using System;
using System.Collections.Generic;
using MBW.Libraries.DeviceObjectLib;
using MBW.Libraries.DeviceObjectLib.Exceptions;
using MBW.Libraries.DeviceObjectLib.ObjectTypes;

namespace ListAllObjects
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IEnumerable<NtObjectBase> objects = NtObjects.Instance.ListDirectory("\\");

            foreach (NtObjectBase @base in objects)
            {
                Iterate(0, @base);
            }
        }

        private static void Iterate(int indent, NtObjectBase item)
        {
            void Write(string str)
            {
                for (int i = 0; i < indent; i++)
                    Console.Write(' ');

                Console.WriteLine(str);
            }

            Write($"{item.Type}: {item.FullName}");

            if (item is PhysicalDriveIdentifier physicalDriveIdentifier)
            {
                Write($"  PhysicalDrive{physicalDriveIdentifier.Identifier} {physicalDriveIdentifier.FileAddress}");
            }
            if (item is HarddiskVolumeIdentifier volumeIdentifier)
            {
                Write($"  Volume{volumeIdentifier.Identifier} {volumeIdentifier.FileAddress}");
            }
            if (item is HarddiskPartitionIdentifier partitionIdentifier)
            {
                Write($"  Partition{partitionIdentifier.Partition}, PhysicalDrive{partitionIdentifier.Harddisk} {partitionIdentifier.FileAddress}");
            }

            if (item.IsSymbolicLink)
            {
                if (item.TryGetSymbolicLinkTarget(out string target))
                    Write($"  => {target}");
                else
                    Write("  => UNABLE TO LOCATE");
            }

            if (item is NtDirectory asDir)
            {
                try
                {
                    foreach (NtObjectBase @base in asDir.ListDirectory())
                    {
                        Iterate(indent + 2, @base);
                    }
                }
                catch (NtObjectException e)
                {
                    Write("  UNABLE TO ENUMERATE DIRECTORY, " + e.StatusCode);
                }

                Console.WriteLine();
            }
        }
    }
}
