using MBW.Libraries.DeviceObjectLib.Exceptions;

namespace MBW.Libraries.DeviceObjectLib.Utilities
{
    internal static class ErrorHelper
    {
        public static void ThrowIfNotSuccess(NtStatus retVal, string message)
        {
            if (retVal != NtStatus.STATUS_SUCCESS)
                throw new NtObjectException(retVal, message);
        }

        public static void Throw(NtStatus retVal, string message)
        {
            throw new NtObjectException(retVal, message);
        }
    }
}