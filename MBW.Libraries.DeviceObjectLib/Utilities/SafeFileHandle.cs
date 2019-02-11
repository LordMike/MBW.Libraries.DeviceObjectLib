using System;

namespace DeviceObjectLib.Utilities
{
    internal class SafeFileHandle : IDisposable
    {
        public IntPtr Handle { get; set; }

        public SafeFileHandle(IntPtr handle = default(IntPtr))
        {
            Handle = handle;
        }

        public void Dispose()
        {
            if (Handle != IntPtr.Zero)
                Win32.CloseHandle(Handle);

            Handle = IntPtr.Zero;
        }
    }
}