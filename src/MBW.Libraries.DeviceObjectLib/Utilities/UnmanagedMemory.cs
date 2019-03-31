using System;
using System.Runtime.InteropServices;

namespace MBW.Libraries.DeviceObjectLib.Utilities
{
    internal class UnmanagedMemory : IDisposable
    {
        public int Bytes { get; }

        public IntPtr Handle { get; private set; }

        public UnmanagedMemory(int bytes)
        {
            Bytes = bytes;
            Handle = Marshal.AllocHGlobal(bytes);
        }

        public UnmanagedMemory(byte[] data)
        {
            Handle = Marshal.AllocHGlobal(data.Length);
            Bytes = data.Length;
            Marshal.Copy(data, 0, Handle, data.Length);
        }

        public void Dispose()
        {
            if (Handle != IntPtr.Zero)
                Marshal.FreeHGlobal(Handle);

            Handle = IntPtr.Zero;
        }

        public void Clear()
        {
            for (int i = 0; i < Bytes; i++)
                Marshal.WriteByte(Handle, i, 0);
        }
    }
}