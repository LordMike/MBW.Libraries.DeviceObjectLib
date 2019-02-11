using System;
using System.Runtime.InteropServices;
using MBW.Libraries.DeviceObjectLib.Objects;

// ReSharper disable InconsistentNaming

namespace MBW.Libraries.DeviceObjectLib.Utilities
{
    internal static class Win32
    {
        [DllImport("ntdll.dll")]
        public static extern NtStatus NtOpenDirectoryObject(
            out IntPtr DirectoryHandle,
            DirectoryAccessEnum DesiredAccess,
            ref OBJECT_ATTRIBUTES ObjectAttributes);

        [DllImport("ntdll.dll")]
        public static extern NtStatus NtQueryDirectoryObject(
            IntPtr DirectoryHandle,
            IntPtr Buffer,
            int Length,
            bool ReturnSingleEntry,
            bool RestartScan,
            ref uint Context,
            out uint ReturnLength);

        [DllImport("ntdll.dll")]
        public static extern NtStatus NtOpenSymbolicLinkObject(
            out IntPtr LinkHandle,
            DirectoryAccessEnum DesiredAccess,
            ref OBJECT_ATTRIBUTES ObjectAttributes);

        [DllImport("ntdll.dll")]
        public static extern NtStatus NtQuerySymbolicLinkObject(
            IntPtr LinkHandle,
            ref UNICODE_STRING LinkTarget,
            out int ReturnedLength);

        [DllImport("Kernel32.dll")]
        public static extern int CloseHandle(IntPtr ptr);
    }
}