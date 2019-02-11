using System;
using System.Collections.Generic;
using MBW.Libraries.DeviceObjectLib;
using MBW.Libraries.DeviceObjectLib.ObjectTypes;

namespace ListAllObjectsAndLinks
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IEnumerable<NtObjectBase> objects = NtUtils.ListDirectoryObjects("\\", true);

            foreach (NtObjectBase @base in objects)
            {
                if (!@base.IsSymbolicLink)
                {
                    Console.WriteLine($"NOTLINK {@base.Type}: {@base.FullName}");
                }
                else
                {
                    string target = @base.GetSymbolicLinkTarget();
                    Console.WriteLine($"LINK {@base.Type}: {@base.FullName} -> {target}");
                }
            }
        }
    }
}
