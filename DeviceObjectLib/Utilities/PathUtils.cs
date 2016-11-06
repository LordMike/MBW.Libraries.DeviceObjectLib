namespace DeviceObjectLib.Utilities
{
    internal class PathUtils
    {
        public static string Combine(string a, string b)
        {
            if (a.EndsWith("\\"))
                return a + b;

            return a + "\\" + b;
        }
    }
}