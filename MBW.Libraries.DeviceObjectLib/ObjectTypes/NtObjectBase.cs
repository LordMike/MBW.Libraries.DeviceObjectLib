using System;
using MBW.Libraries.DeviceObjectLib.Objects;
using MBW.Libraries.DeviceObjectLib.Utilities;

namespace MBW.Libraries.DeviceObjectLib.ObjectTypes
{
    public class NtObjectBase
    {
        public string Parent { get; }
        public string TypeName { get; }
        public string Name { get; }

        public WellKnownType Type { get; }

        public string FullName => PathUtils.Combine(Parent, Name);

        public bool IsSymbolicLink => Type == WellKnownType.SymbolicLink;

        public NtObjectBase(string typeName, string parent, string name)
        {
            Parent = parent;
            TypeName = typeName;
            Name = name;

            Type = StaticStrings.ToWellKnownType(typeName);
        }

        public string GetSymbolicLinkTarget()
        {
            if (Type != WellKnownType.SymbolicLink)
                throw new InvalidOperationException("Not a symbolic link");

            OBJECT_ATTRIBUTES attrib = new OBJECT_ATTRIBUTES(FullName, 0);

            IntPtr handleUnsafe;
            NtStatus retVal = Win32.NtOpenSymbolicLinkObject(out handleUnsafe, DirectoryAccessEnum.DIRECTORY_QUERY, ref attrib);

            if (retVal != NtStatus.STATUS_SUCCESS)
                throw new Exception("NtOpenSymbolicLinkObject, return: " + retVal);

            using (SafeFileHandle handle = new SafeFileHandle(handleUnsafe))
            {
                int retLength;
                UNICODE_STRING target = new UNICODE_STRING();
                NtStatus retVal2 = Win32.NtQuerySymbolicLinkObject(handle.Handle, ref target, out retLength);

                if (retVal2 == NtStatus.STATUS_BUFFER_TOO_SMALL)
                {
                    target = new UNICODE_STRING(retLength);

                    retVal2 = Win32.NtQuerySymbolicLinkObject(handle.Handle, ref target, out retLength);

                    if (retVal2 != NtStatus.STATUS_SUCCESS)
                        throw new Exception("Unable to get target: " + retVal2);
                }

                return target.ToString();
            }
        }

        public override string ToString()
        {
            return $"{TypeName}: {Name}";
        }
    }
}