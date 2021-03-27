DeviceObjectLib [![Generic Build](https://github.com/LordMike/MBW.Libraries.DeviceObjectLib/actions/workflows/dotnet.yml/badge.svg)](https://github.com/LordMike/MBW.Libraries.DeviceObjectLib/actions/workflows/dotnet.yml) [![Nuget](https://img.shields.io/nuget/v/MBW.Libraries.DeviceObjectLib)](https://www.nuget.org/packages/MBW.Libraries.DeviceObjectLib)
==================

A C# Library to work with the Win32 Windows Object Tree

### Reasons

The Windows Object Tree is available and documented, but not very covered in C#. I've implemented methods necessary to explore the object tree and get useful information from it. It is f.ex. now very simple to get a list of all volumes available to Windows.

#### Notes

Some parts of the object tree require Admin rights.

### High level

The main class to use is `DeviceObjectLib.NtUtils`, which provides static methods. The two main methods are `ListObjects` and `ListDirectoryObjects`:

* `ListObjects` - Lists objects, but makes no attempt to interpret them
* `ListDirectoryObjects` - Lists objects, and attempts to interpret them as special types

Objects interpreted by this library include (See more in the `ObjectTypes` namespace):

* `NtDirectory` - a directory in the object tree
* `HarddiskPartitionIdentifier` - e.g. `\Device\Harddisk0Partition2`
* `HarddiskVolumeIdentifier` - e.g. `\Device\HarddiskVolume3`

These objects can typically also be listed using the methods on `NtUtils`.

The objects also typically provide some convenience features, such as calculating the paths of the object in question, so they can be opened using `File.Open`.

#### Example: List all objects

This example lists all objects in the tree, recursively, and prints out their paths and types.

    using System;
    using System.Collections.Generic;
    using DeviceObjectLib;
    using DeviceObjectLib.ObjectTypes;
    
    namespace ListAllObjects
    {
        public class Program
        {
            public static void Main(string[] args)
            {
                IEnumerable<NtObjectBase> objects = NtUtils.ListDirectory("\\", true);
    
                foreach (NtObjectBase @base in objects)
                    Console.WriteLine($"{@base.Type}: {@base.FullName}");
            }
        }
    }

See more examples in the `Examples` folder.
