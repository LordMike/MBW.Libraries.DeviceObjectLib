using System;
using System.Collections.Generic;
using System.Linq;
using MBW.Libraries.DeviceObjectLib;
using MBW.Libraries.DeviceObjectLib.ObjectTypes;

namespace EnumerateDisks
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IEnumerable<PhysicalDriveIdentifier> disks = NtObjects.Instance.GetPhysicalDrives().OrderBy(s => s.Identifier);

            foreach (PhysicalDriveIdentifier disk in disks)
            {
                Console.WriteLine($"DISK {disk.Identifier}, type: {disk.Type}: {disk.DeviceAddress}");

                foreach (HarddiskPartitionIdentifier partition in NtObjects.Instance.GetHarddiskPartitions(disk)
                    .OrderBy(s => s.Partition))
                {
                    Console.WriteLine($" - PART {partition.Harddisk} / {partition.Partition}, type: {partition.Type}: {partition.DeviceAddress}");

                    if (partition.TryGetVolume(out HarddiskVolumeIdentifier volume))
                        Console.WriteLine($"   => VOLUME {volume.Identifier}, type: {volume.Type}: {volume.DeviceAddress}");
                }

            }

            Console.WriteLine();

            IEnumerable<HarddiskVolumeIdentifier> volumes = NtObjects.Instance.GetHarddiskVolumes().OrderBy(s => s.Identifier);

            foreach (HarddiskVolumeIdentifier volume in volumes)
                Console.WriteLine($"VOL {volume.Identifier}, type: {volume.Type}: {volume.DeviceAddress}, file: {volume.FileAddress}");

            Console.WriteLine();

            IEnumerable<HarddiskPartitionIdentifier> partitions = NtObjects.Instance.GetHarddiskPartitions().OrderBy(s => s.Harddisk).ThenBy(s => s.Partition);

            foreach (HarddiskPartitionIdentifier partition in partitions)
            {
                Console.WriteLine($"PART {partition.Harddisk} / {partition.Partition}, type: {partition.Type}: {partition.DeviceAddress}");
            }
        }
    }
}
