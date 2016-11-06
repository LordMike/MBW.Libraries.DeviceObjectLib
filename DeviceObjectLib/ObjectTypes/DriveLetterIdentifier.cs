namespace DeviceObjectLib.ObjectTypes
{
    public class DriveLetterIdentifier : NtObjectBase
    {
        public string DriveLetter { get; }
        
        public DriveLetterIdentifier(string typeName, string parent, string name, string driveLetter)
            : base(typeName, parent, name)
        {
            DriveLetter = driveLetter;
        }

        /// <summary>
        /// Can be used in File.Open() scenarios.
        /// Typically \\?\GLOBAL\X:
        /// </summary>
        public string FileAddress => @"\\?\GLOBAL\" + Name;

        /// <summary>
        /// A drive letter is a symbolic link to a volume.
        /// </summary>
        /// <returns>The volume for the given drive letter</returns>
        public NtObjectBase GetTarget()
        {
            string target = GetSymbolicLinkTarget();

            return NtUtils.GetSingleObject(target);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}