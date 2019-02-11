using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using MBW.Libraries.DeviceObjectLib.Objects;
using MBW.Libraries.DeviceObjectLib.ObjectTypes;
using MBW.Libraries.DeviceObjectLib.Utilities;

namespace MBW.Libraries.DeviceObjectLib
{
    public static class NtUtils
    {
        private static readonly Regex HarddiskVolumeRegex = new Regex(@"^HarddiskVolume([0-9]+)$", RegexOptions.Compiled);
        private static readonly Regex PhysicalDriveRegex = new Regex(@"^PhysicalDrive([0-9]+)$", RegexOptions.Compiled);
        private static readonly Regex PartitionRegex = new Regex(@"^Partition([0-9]+)$", RegexOptions.Compiled);
        private static readonly Regex HarddiskPartitionRegex = new Regex(@"^Harddisk([0-9]+)Partition([0-9]+)$", RegexOptions.Compiled);
        private static readonly Regex HarddiskRegex = new Regex(@"Harddisk([0-9]+)$", RegexOptions.Compiled);
        private static readonly Regex DriveLetterRegex = new Regex(@"([A-Z]+):$", RegexOptions.Compiled);

        public static IEnumerable<NtObjectBase> ListDirectory(string path, bool recurse = false, WellKnownType filterType = WellKnownType.Unknown)
        {
            OBJECT_ATTRIBUTES objectAttributes = new OBJECT_ATTRIBUTES(path, 0);
            IntPtr directoryHandleUnsafe;
            NtStatus retVal = Win32.NtOpenDirectoryObject(out directoryHandleUnsafe, DirectoryAccessEnum.DIRECTORY_QUERY, ref objectAttributes);

            if (retVal != NtStatus.STATUS_SUCCESS)
                throw new Exception("NtOpenDirectoryObject Error code: " + retVal + ", parentPath: " + path);

            SafeFileHandle directoryHandle = new SafeFileHandle(directoryHandleUnsafe);

            using (directoryHandle)
            {
                uint singleDirInfo = (uint)Marshal.SizeOf<OBJECT_DIRECTORY_INFORMATION>();
                using (UnmanagedMemory mem = new UnmanagedMemory((int)(256 * singleDirInfo)))
                {
                    bool restart = true;
                    NtStatus status;
                    uint context = 0;

                    do
                    {
                        uint returnLength;
                        status = Win32.NtQueryDirectoryObject(directoryHandle.Handle, mem.Handle, mem.Bytes, false, restart, ref context, out returnLength);
                        restart = false;

                        IntPtr ptr = mem.Handle;

                        while (true)
                        {
                            OBJECT_DIRECTORY_INFORMATION dir = Marshal.PtrToStructure<OBJECT_DIRECTORY_INFORMATION>(ptr);
                            ptr = new IntPtr(ptr.ToInt64() + (int)singleDirInfo);

                            if (dir.Name.Length == 0)
                                break;

                            string typeName = dir.TypeName.ToString();
                            string name = dir.Name.ToString();
                            WellKnownType wellKnownType = StaticStrings.ToWellKnownType(typeName);

                            if (filterType != WellKnownType.Unknown && filterType != wellKnownType)
                                continue;

                            if (wellKnownType == WellKnownType.Directory)
                            {
                                NtDirectory directory = new NtDirectory(typeName, path, name);
                                yield return directory;

                                if (recurse)
                                {
                                    foreach (NtObjectBase subObject in ListDirectory(directory.FullName, true, filterType))
                                        yield return subObject;
                                }

                                continue;
                            }

                            yield return new NtObjectBase(typeName, path, name);
                        }
                    } while (status == NtStatus.STATUS_MORE_ENTRIES);
                }
            }
        }

        public static IEnumerable<NtObjectBase> ListDirectoryObjects(string path, bool recurse = false, WellKnownType filterType = WellKnownType.Unknown)
        {
            foreach (NtObjectBase @object in ListDirectory(path, recurse, filterType))
            {
                yield return ConvertToSpecificType(@object);
            }
        }

        internal static NtObjectBase ConvertToSpecificType(NtObjectBase @object)
        {
            if (@object is NtDirectory)
            {
                return @object;
            }

            Match match;
            if (@object.Type == WellKnownType.SymbolicLink)
            {
                if ((match = PhysicalDriveRegex.Match(@object.Name)).Success)
                {
                    int id = int.Parse(match.Groups[1].Value);
                    return new PhysicalDriveIdentifier(@object.TypeName, @object.Parent, @object.Name, id);
                }

                if ((match = PartitionRegex.Match(@object.Name)).Success)
                {
                    int partitionIdx = int.Parse(match.Groups[1].Value);

                    match = HarddiskRegex.Match(@object.Parent);
                    if (match.Success)
                    {
                        int harddiskIdx = int.Parse(match.Groups[1].Value);
                        return new HarddiskPartitionIdentifier(@object.TypeName, @object.Parent, @object.Name, harddiskIdx, partitionIdx);
                    }
                }

                if ((match = HarddiskPartitionRegex.Match(@object.Name)).Success)
                {
                    int harddiskIdx = int.Parse(match.Groups[1].Value);
                    int partitionIdx = int.Parse(match.Groups[2].Value);

                    return new HarddiskPartitionIdentifier(@object.TypeName, @object.Parent, @object.Name, harddiskIdx, partitionIdx);
                }

                if ((match = DriveLetterRegex.Match(@object.Name)).Success)
                {
                    string letter = match.Groups[1].Value;

                    return new DriveLetterIdentifier(@object.TypeName, @object.Parent, @object.Name, letter);
                }
            }

            if (@object.Type == WellKnownType.Device && (match = HarddiskVolumeRegex.Match(@object.Name)).Success)
            {
                int id = int.Parse(match.Groups[1].Value);
                return new HarddiskVolumeIdentifier(@object.TypeName, @object.Parent, @object.Name, id);
            }

            return @object;
        }

        public static NtObjectBase GetSingleObject(string path)
        {
            // TODO: some clever way of not enumerating everything?
            string[] splits = path.Split('\\');
            string parent = string.Join("\\", splits, 0, splits.Length - 1);
            string name = splits[splits.Length - 1];

            foreach (NtObjectBase o in ListDirectoryObjects(parent))
            {
                if (o.Name == name)
                    return o;
            }

            return null;
        }

        public static IEnumerable<PhysicalDriveIdentifier> GetPhysicalDrives()
        {
            foreach (NtObjectBase objectBase in ListDirectoryObjects(StaticStrings.GlobalRoot, filterType: WellKnownType.SymbolicLink))
            {
                PhysicalDriveIdentifier obj = objectBase as PhysicalDriveIdentifier;
                if (obj == null)
                    continue;

                yield return obj;
            }
        }

        public static IEnumerable<HarddiskVolumeIdentifier> GetHarddiskVolumes()
        {
            foreach (NtObjectBase objectBase in ListDirectoryObjects(StaticStrings.DeviceRoot, filterType: WellKnownType.Device))
            {
                HarddiskVolumeIdentifier obj = objectBase as HarddiskVolumeIdentifier;
                if (obj == null)
                    continue;

                yield return obj;
            }
        }

        public static IEnumerable<HarddiskPartitionIdentifier> GetHarddiskPartitions()
        {
            // For multiple disks, we can look at the root of the global directory
            IEnumerable<NtObjectBase> objects = ListDirectory(StaticStrings.GlobalRoot, filterType: WellKnownType.SymbolicLink);

            foreach (NtObjectBase @base in objects)
            {
                if (!HarddiskPartitionRegex.IsMatch(@base.Name))
                    continue;

                HarddiskPartitionIdentifier item = ConvertToSpecificType(@base) as HarddiskPartitionIdentifier;

                if (item != null)
                    yield return item;
            }
        }

        public static IEnumerable<HarddiskPartitionIdentifier> GetHarddiskPartitions(PhysicalDriveIdentifier physicalDrive)
        {
            // For a single disk, we can look at the specific directory
            IEnumerable<NtObjectBase> objects = ListDirectory(physicalDrive.ObjectFolderAddress, filterType: WellKnownType.SymbolicLink);

            foreach (NtObjectBase @base in objects)
            {
                if (!PartitionRegex.IsMatch(@base.Name))
                    continue;

                HarddiskPartitionIdentifier item = ConvertToSpecificType(@base) as HarddiskPartitionIdentifier;

                if (item != null)
                    yield return item;
            }
        }
    }
}
