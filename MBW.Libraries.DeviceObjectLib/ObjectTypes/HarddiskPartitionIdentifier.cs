namespace MBW.Libraries.DeviceObjectLib.ObjectTypes
{
    public class HarddiskPartitionIdentifier : NtObjectBase
    {
        public int Harddisk { get; }
        public int Partition { get; }

        public HarddiskPartitionIdentifier(string typeName, string parent, string name, int harddisk, int partition)
            : base(typeName, parent, name)
        {
            Harddisk = harddisk;
            Partition = partition;
        }

        /// <summary>
        /// Can be used in File.Open() scenarios.
        /// Typically \\?\GLOBAL\HarddiskXPartitionY
        /// </summary>
        public string FileAddress => $@"\\?\GLOBAL\Harddisk{Harddisk}Partition{Partition}";

        /// <summary>
        /// Can be used to work with the NT Object Tree
        /// Typically \Device\HarddiskXPartitionY
        /// </summary>
        public string DeviceAddress => $@"\Device\Harddisk{Harddisk}Partition{Partition}";

        public override string ToString()
        {
            return $"Harddisk{Harddisk}Partition{Partition}";
        }
    }
}