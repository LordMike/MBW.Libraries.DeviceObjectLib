using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DeviceObjectLib;
using DeviceObjectLib.ObjectTypes;

namespace DemoApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //SetupHelper.SetupFileSystems();

            var curr = NtUtils.ListDirectory("\\", true).ToList();

            var objects = NtUtils.ListDirectoryObjects("\\", true).ToList();

            //foreach (var ntObjectBase in objects.Where(s => s.IsSymbolicLink))
            //{
            //    Console.WriteLine(ntObjectBase.FullName + " -> " + ntObjectBase.GetSymbolicLinkTarget());
            //}

            foreach (var ntObjectBase in objects.OfType<DriveLetterIdentifier>())
            {
                var objectBase = ntObjectBase.GetTarget();
                Console.WriteLine(ntObjectBase.DriveLetter + " -> " + ntObjectBase.FileAddress + " -> " + objectBase);
            }

            //List<HarddiskIdentifier> harddisks = NtUtils.GetPhysicalDrives().ToList();
            //List<HarddiskPartitionIdentifier> partitions = NtUtils.GetHarddiskPartitions().ToList();
            List<HarddiskVolumeIdentifier> volumes = NtUtils.GetHarddiskVolumes().ToList();


            foreach (HarddiskVolumeIdentifier volume in volumes)
            {
                Console.WriteLine(volume.FileAddress);

                using (FileStream file = File.Open(volume.FileAddress, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    //long length = 0;
                    //try
                    //{
                    //    using (VolumeDeviceWrapper wrapper = new VolumeDeviceWrapper(file.SafeFileHandle))
                    //    {
                    //        VOLUME_DISK_EXTENTS extents = wrapper.VolumeGetVolumeDiskExtents();

                    //        length = extents.Extents.Sum(s => s.ExtentLength - s.StartingOffset);
                    //    }
                    //}
                    //catch (Exception ex)
                    //{
                    //    Console.WriteLine($" ERROR: {ex.Message}");
                    //}

                    //try
                    //{
                    //    byte[] data = new byte[2 * 1024 * 1024];
                    //    int read = file.Read(data, 0, data.Length);
                    //    Console.WriteLine($" - Read: {read:N0}, actual length: {length:N0}");

                    //    FileSystemInfo[] fs;
                    //    using (MemoryStream ms = new MemoryStream(data))
                    //    using (FakeLengthStream fakeStream = new FakeLengthStream(ms, length))
                    //    {
                    //        fs = FileSystemManager.DetectFileSystems(fakeStream);

                    //        foreach (FileSystemInfo info in fs)
                    //        {
                    //            Console.WriteLine($" - {info.Name}, {info.Description}, {info}");
                    //        }
                    //    }

                    //    if (fs.Length == 1)
                    //    {
                    //        using (FakeLengthStream fakeStream = new FakeLengthStream(file, length))
                    //        using (DiscFileSystem disc = fs[0].Open(fakeStream))
                    //        {
                    //            string[] dirs = disc.GetDirectories("");

                    //            foreach (string dir in dirs)
                    //            {
                    //                Console.WriteLine($" - {volume.FileAddress}\\{dir}");
                    //            }
                    //        }
                    //    }


                    //}
                    //catch (Exception ex)
                    //{
                    //    Console.WriteLine($" ERROR: {ex.Message}");
                    //}

                    //using (FileStream handle = new FileStream(file.SafeFileHandle, FileAccess.Read, 1024, false))
                    //{
                    //    var fakeStream = new FakeLengthStream(handle, 1 * 1024 * 1024);

                    //    try
                    //    {
                    //        fakeStream.Seek(0, SeekOrigin.Begin);
                    //        FileSystemInfo[] fs = FileSystemManager.DetectFileSystems(fakeStream);

                    //        foreach (FileSystemInfo info in fs)
                    //        {
                    //            Console.WriteLine($" - {info.Name}, {info.Description}, {info}");
                    //        }
                    //    }
                    //    catch (Exception ex)
                    //    {
                    //        Console.WriteLine($" ERROR: {ex.Message}");
                    //    }

                    //}
                }


            }
        }
    }

}
