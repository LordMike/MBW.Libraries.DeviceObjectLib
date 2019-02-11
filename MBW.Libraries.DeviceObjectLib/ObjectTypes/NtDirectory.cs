using System.Collections.Generic;

namespace DeviceObjectLib.ObjectTypes
{
    public class NtDirectory : NtObjectBase
    {
        public NtDirectory(string typeName, string parent, string name)
            : base(typeName, parent, name)
        {
        }

        public IEnumerable<NtObjectBase> ListDirectory(bool recurse = false, WellKnownType filterType = WellKnownType.Unknown)
        {
            return NtUtils.ListDirectory(FullName, recurse, filterType);
        }
    }
}