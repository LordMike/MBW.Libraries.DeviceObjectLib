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
