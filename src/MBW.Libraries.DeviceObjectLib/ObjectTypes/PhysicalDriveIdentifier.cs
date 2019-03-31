namespace MBW.Libraries.DeviceObjectLib.ObjectTypes
{
    public class PhysicalDriveIdentifier : NtObjectBase
    {
        public int Identifier { get; }

        public PhysicalDriveIdentifier(string typeName, string parent, string name, int identifier)
            : base(typeName, parent, name)
        {
            Identifier = identifier;
        }

        /// <summary>
        /// Can be used in File.Open() scenarios.
        /// Typically \\?\GLOBAL\PhysicalDriveX
        /// </summary>
        public string FileAddress => $@"\\?\GLOBAL\PhysicalDrive{Identifier}";

        /// <summary>
        /// Can be used to work with the NT Object Tree, represents the directory in which the harddisk resides 
        /// Typically \Device\HarddiskX
        /// </summary>
        public string ObjectFolderAddress => $@"\Device\Harddisk{Identifier}";

        /// <summary>
        /// Can be used to work with the NT Object Tree, represents the harddisks device
        /// Typically \Device\HarddiskX\DRX
        /// </summary>
        public string DeviceAddress => $@"\Device\Harddisk{Identifier}\DR{Identifier}";

        public override string ToString()
        {
            return $"PhysicalDrive{Identifier}";
        }
    }
}