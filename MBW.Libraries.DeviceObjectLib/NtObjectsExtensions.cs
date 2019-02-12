using System.Collections.Generic;
using MBW.Libraries.DeviceObjectLib.ObjectTypes;

namespace MBW.Libraries.DeviceObjectLib
{
    public static class NtObjectsExtensions
    {
        public static IEnumerable<PhysicalDriveIdentifier> GetPhysicalDrives(this NtObjects ntObjects)
        {
            foreach (NtObjectBase objectBase in ntObjects.ListDirectory(StaticStrings.GlobalRoot, filterType: WellKnownType.SymbolicLink))
            {
                PhysicalDriveIdentifier obj = objectBase as PhysicalDriveIdentifier;
                if (obj == null)
                    continue;

                yield return obj;
            }
        }

        public static IEnumerable<HarddiskVolumeIdentifier> GetHarddiskVolumes(this NtObjects ntObjects)
        {
            foreach (NtObjectBase objectBase in ntObjects.ListDirectory(StaticStrings.DeviceRoot, filterType: WellKnownType.Device))
            {
                HarddiskVolumeIdentifier obj = objectBase as HarddiskVolumeIdentifier;
                if (obj == null)
                    continue;

                yield return obj;
            }
        }

        public static IEnumerable<HarddiskPartitionIdentifier> GetHarddiskPartitions(this NtObjects ntObjects)
        {
            // For multiple disks, we can look at the root of the global directory
            IEnumerable<NtObjectBase> objects = ntObjects.ListDirectory(StaticStrings.GlobalRoot, filterType: WellKnownType.SymbolicLink);

            foreach (NtObjectBase @base in objects)
            {
                if (!ntObjects.HarddiskPartitionRegex.IsMatch(@base.Name))
                    continue;

                if (@base is HarddiskPartitionIdentifier item)
                    yield return item;
            }
        }

        public static IEnumerable<HarddiskPartitionIdentifier> GetHarddiskPartitions(this NtObjects ntObjects, PhysicalDriveIdentifier physicalDrive)
        {
            // For a single disk, we can look at the specific directory
            IEnumerable<NtObjectBase> objects = ntObjects.ListDirectory(physicalDrive.ObjectFolderAddress, filterType: WellKnownType.SymbolicLink);

            foreach (NtObjectBase @base in objects)
            {
                if (!ntObjects.PartitionRegex.IsMatch(@base.Name))
                    continue;

                if (@base is HarddiskPartitionIdentifier item)
                    yield return item;
            }
        }
    }
}