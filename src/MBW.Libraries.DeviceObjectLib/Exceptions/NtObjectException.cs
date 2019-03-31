using System;
using MBW.Libraries.DeviceObjectLib.Utilities;

namespace MBW.Libraries.DeviceObjectLib.Exceptions
{
    public class NtObjectException : Exception
    {
        public NtStatus StatusCode { get; }

        public NtObjectException(NtStatus statusCode, string message) : base($"Error performing API call: {statusCode} - {message}")
        {
            StatusCode = statusCode;
        }
    }
}