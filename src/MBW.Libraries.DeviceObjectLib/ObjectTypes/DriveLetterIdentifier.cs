namespace MBW.Libraries.DeviceObjectLib.ObjectTypes
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
        public string FileAddress => $@"\\?\GLOBAL\{Name}";

        public override string ToString()
        {
            return Name;
        }
    }
}