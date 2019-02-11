namespace DeviceObjectLib.ObjectTypes
{
    public class HarddiskVolumeIdentifier : NtObjectBase
    {
        public int Identifier { get; }

        public HarddiskVolumeIdentifier(string typeName, string parent, string name, int identifier)
            : base(typeName, parent, name)
        {
            Identifier = identifier;
        }

        /// <summary>
        /// Can be used in File.Open() scenarios.
        /// Typically \\?\GLOBAL\HarddiskVolumeX
        /// </summary>
        public string FileAddress => @"\\?\GLOBAL\HarddiskVolume" + Identifier;

        /// <summary>
        /// Can be used to work with the NT Object Tree
        /// Typically \Device\HarddiskVolumeX
        /// </summary>
        public string DeviceAddress => @"\Device\HarddiskVolume" + Identifier;

        public override string ToString()
        {
            return $"HarddiskVolume{Identifier}";
        }
    }
}