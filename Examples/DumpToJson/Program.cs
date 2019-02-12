using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using MBW.Libraries.DeviceObjectLib;
using MBW.Libraries.DeviceObjectLib.ObjectTypes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace DumpToJson
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //var pp = NtObjects.Instance.GetSingleObject(@"\Device\Csc");
            //var pp1 = pp.GetSymbolicLinkObject();

            var objects = ListAll()
                //.Where(s => s.FullName.StartsWith(@"\Device"))
                .OrderBy(s => s.FullName)
                .Select(s =>
                {
                    Dictionary<string, object> item = new Dictionary<string, object>();

                    foreach (PropertyInfo property in s.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
                    {
                        if (!property.CanRead)
                            continue;

                        object value = property.GetValue(s);
                        if (value is bool bl && !bl)
                            continue;

                        item[property.Name] = value;
                    }

                    if (s is NtDirectory dir)
                    {
                        try
                        {
                            item["Children"] = dir.ListDirectory().Select(x => x.FullName).ToArray();
                        }
                        catch (Exception)
                        {
                        }
                    }

                    if (s.GetType() != typeof(NtObjectBase))
                        item["Interpreted"] = s.GetType().Name;

                    if (s.IsSymbolicLink)
                    {
                        List<Dictionary<string, object>> symlinks = new List<Dictionary<string, object>>();

                        item["IsSymbolicLink"] = true;
                        item["SymbolicLinkChain"] = symlinks;

                        NtObjectBase linkItem = s;
                        while (linkItem.IsSymbolicLink && linkItem.TryGetSymbolicLinkObject(out NtObjectBase target))
                        {
                            symlinks.Add(new Dictionary<string, object>
                            {
                                { "SymlinkTarget", target.FullName },
                                { "SymlinkTargetType", target.Type}
                            });

                            linkItem = target;
                        }

                    }

                    return item;
                });

            string json = JsonConvert.SerializeObject(objects, Formatting.Indented, new StringEnumConverter());
            Console.WriteLine(json);
        }

        private static IEnumerable<NtObjectBase> ListAll()
        {
            Stack<NtObjectBase> stack = new Stack<NtObjectBase>();

            foreach (NtObjectBase ntObjectBase in NtObjects.Instance.ListDirectory("\\"))
                stack.Push(ntObjectBase);

            while (stack.TryPop(out NtObjectBase item))
            {
                yield return item;

                if (item is NtDirectory dir)
                {
                    try
                    {
                        foreach (NtObjectBase ntObjectBase in dir.ListDirectory())
                            stack.Push(ntObjectBase);
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }
    }
}
