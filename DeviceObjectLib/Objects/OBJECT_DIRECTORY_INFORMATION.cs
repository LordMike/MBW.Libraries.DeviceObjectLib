using System.Runtime.InteropServices;

namespace DeviceObjectLib.Objects
{
    [StructLayout(LayoutKind.Sequential)]
    // ReSharper disable once InconsistentNaming
    internal struct OBJECT_DIRECTORY_INFORMATION
    {
        public UNICODE_STRING Name;
        public UNICODE_STRING TypeName;
    }
}