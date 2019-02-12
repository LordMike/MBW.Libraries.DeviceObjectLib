using System;
using MBW.Libraries.DeviceObjectLib.Objects;
using MBW.Libraries.DeviceObjectLib.Utilities;
using Microsoft.Win32.SafeHandles;

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
            if (!TryGetSymbolicLinkTarget(out string res, out NtStatus retCode))
                ErrorHelper.Throw(retCode, "Unable to find Symbolic Link Target");

            return res;
        }

        public bool TryGetSymbolicLinkTarget(out string result)
        {
            return TryGetSymbolicLinkTarget(out result, out NtStatus _);
        }

        public bool TryGetSymbolicLinkTarget(out string result, out NtStatus returnCode)
        {
            if (Type != WellKnownType.SymbolicLink)
                throw new InvalidOperationException($"Not a symbolic link, was {Type}");

            OBJECT_ATTRIBUTES attrib = new OBJECT_ATTRIBUTES(FullName, 0);
            returnCode = Win32.NtOpenSymbolicLinkObject(out SafeFileHandle handle, DirectoryAccessEnum.DIRECTORY_QUERY, ref attrib);

            using (handle)
            {
                UNICODE_STRING target = new UNICODE_STRING();
                returnCode = Win32.NtQuerySymbolicLinkObject(handle, ref target, out int retLength);

                if (returnCode == NtStatus.STATUS_SUCCESS)
                {
                    result = target.ToString();
                    return returnCode == NtStatus.STATUS_SUCCESS;
                }

                if (returnCode != NtStatus.STATUS_BUFFER_TOO_SMALL)
                {
                    result = null;
                    return false;
                }

                target = new UNICODE_STRING((ushort)retLength);
                returnCode = Win32.NtQuerySymbolicLinkObject(handle, ref target, out retLength);

                if (returnCode != NtStatus.STATUS_SUCCESS)
                {
                    result = null;
                    return false;
                }

                result = target.ToString();

                if (result.Contains(";"))
                    result = result.Substring(0, result.IndexOf(';'));

                if (result.EndsWith("\\"))
                    result = result.Substring(0, result.Length - 1);

                return true;
            }
        }

        public NtObjectBase GetSymbolicLinkObject()
        {
            return NtObjects.Instance.GetSingleObject(GetSymbolicLinkTarget());
        }

        public bool TryGetSymbolicLinkObject(out NtObjectBase result)
        {
            return TryGetSymbolicLinkObject(out result, out _);
        }

        public bool TryGetSymbolicLinkObject(out NtObjectBase result, out NtStatus returnCode)
        {
            result = null;

            if (!TryGetSymbolicLinkTarget(out var target, out returnCode))
                return false;

            try
            {
                result = NtObjects.Instance.GetSingleObject(target);
            }
            catch (Exception)
            {
            }

            return result != null;
        }

        public override string ToString()
        {
            return $"{TypeName}: {Name}";
        }
    }
}