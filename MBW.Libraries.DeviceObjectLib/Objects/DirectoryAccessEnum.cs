using System;

namespace MBW.Libraries.DeviceObjectLib.Objects
{
    [Flags]
    internal enum DirectoryAccessEnum
    {
        /// <summary>
        /// Query access to the directory object.
        /// </summary>
        DIRECTORY_QUERY = 0x0001,

        /// <summary>
        /// Name-lookup access to the directory object.
        /// </summary>
        DIRECTORY_TRAVERSE = 0x0002,

        /// <summary>
        /// Name-creation access to the directory object.
        /// </summary>
        DIRECTORY_CREATE_OBJECT = 0x0004,

        /// <summary>
        /// Subdirectory-creation access to the directory object.
        /// </summary>
        DIRECTORY_CREATE_SUBDIRECTORY = 0x0008,

        ///// <summary>
        ///// All of the preceding rights plus STANDARD_RIGHTS_REQUIRED.
        ///// </summary>
        //DIRECTORY_ALL_ACCESS = STANDARD_RIGHTS_REQUIRED | 0xF
    }
}