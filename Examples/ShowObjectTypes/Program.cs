using System;
using System.Collections.Generic;
using System.Linq;
using DeviceObjectLib;
using DeviceObjectLib.ObjectTypes;

namespace ShowObjectTypes
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IEnumerable<PhysicalDriveIdentifier> disks = NtUtils.GetPhysicalDrives().OrderBy(s => s.Identifier);

            foreach (PhysicalDriveIdentifier disk in disks)
            {
                Console.WriteLine($"DISK {disk.Identifier}, type: {disk.Type}: {disk.DeviceAddress}");

                foreach (HarddiskPartitionIdentifier partition in NtUtils.GetHarddiskPartitions(disk).OrderBy(s => s.Partition))
                    Console.WriteLine($" - PART {partition.Harddisk} / {partition.Partition}, type: {partition.Type}: {partition.DeviceAddress}");
            }

            Console.WriteLine();

            IEnumerable<HarddiskVolumeIdentifier> volumes = NtUtils.GetHarddiskVolumes().OrderBy(s => s.Identifier);

            foreach (HarddiskVolumeIdentifier volume in volumes)
                Console.WriteLine($"VOL {volume.Identifier}, type: {volume.Type}: {volume.DeviceAddress}, file: {volume.FileAddress}");

            Console.WriteLine();

            IEnumerable<HarddiskPartitionIdentifier> partitions = NtUtils.GetHarddiskPartitions().OrderBy(s => s.Harddisk).ThenBy(s => s.Partition);

            foreach (HarddiskPartitionIdentifier partition in partitions)
            {
                Console.WriteLine($"PART {partition.Harddisk} / {partition.Partition}, type: {partition.Type}: {partition.DeviceAddress}");
            }
        }
    }
}
