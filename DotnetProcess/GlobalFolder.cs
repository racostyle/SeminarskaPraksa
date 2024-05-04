namespace SeminarskaPraksa.DotnetProcess
{
    internal static class GlobalFolder
    {
        private static readonly string _location;
        public static string Location => _location;

        static GlobalFolder()
        {
            _location = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SeminarskaPraksa");
            Directory.CreateDirectory(_location);
        }
    }
}
