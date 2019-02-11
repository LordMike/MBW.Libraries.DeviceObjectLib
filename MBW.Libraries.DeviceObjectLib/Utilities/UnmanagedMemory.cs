using System;
using System.Runtime.InteropServices;

namespace MBW.Libraries.DeviceObjectLib.Utilities
{
    internal class UnmanagedMemory : IDisposable
    {
        private IntPtr _handle;

        public int Bytes { get; }

        public IntPtr Handle => _handle;

        public UnmanagedMemory(int bytes)
        {
            Bytes = bytes;
            _handle = Marshal.AllocHGlobal(bytes);
        }

        public UnmanagedMemory(byte[] data)
        {
            _handle = Marshal.AllocHGlobal(data.Length);
            Bytes = data.Length;
            Marshal.Copy(data, 0, Handle, data.Length);
        }

        public void Dispose()
        {
            if (_handle != IntPtr.Zero)
                Marshal.FreeHGlobal(_handle);

            _handle = IntPtr.Zero;
        }
    }
}