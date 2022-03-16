using System.IO;

namespace BigBoxPCPSelector.Helper
{
    public sealed class DirectoryInfoHelper
    {
        private static readonly DirectoryInfoHelper instance = new DirectoryInfoHelper();

        // path to the big box application directory
        private string applicationPath;
        public string ApplicationPath
        {
            get
            {
                if (string.IsNullOrWhiteSpace(applicationPath))
                {
                    applicationPath = Directory.GetCurrentDirectory();
                }

                return applicationPath;
            }
        }

        private string pluginFolder;
        public string PluginFolder
        {
            get
            {
                if (string.IsNullOrWhiteSpace(pluginFolder))
                {
                    pluginFolder = $"{ApplicationPath}\\Plugins\\BigBoxPCPSelector";
                }
                return pluginFolder;
            }
        }

        private string logFile;
        public string LogFile
        {
            get
            {
                if (string.IsNullOrWhiteSpace(logFile))
                {
                    logFile = $"{PluginFolder}\\log.txt";
                }
                return logFile;
            }
        }

        private string settingsFile;
        public string SettingsFile
        {
            get
            {
                if (string.IsNullOrWhiteSpace(settingsFile))
                {
                    settingsFile = $"{PluginFolder}\\settings.json";
                }
                return settingsFile;
            }
        }

        public static void CreateFolders()
        {
            CreateFolder(Instance.PluginFolder);
        }

        public static void CreateFolder(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public static void CreateFileIfNotExists(string filePath)
        {
            if (!File.Exists(filePath))
            {
                CreateDirectoryIfNotExists(filePath);

                File.WriteAllText(filePath, "");
            }
        }

        public static void CreateDirectoryIfNotExists(string filePath)
        {
            string folderPath = Path.GetDirectoryName(filePath);

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
        }

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static DirectoryInfoHelper()
        {
        }

        private DirectoryInfoHelper()
        {

        }

        public static DirectoryInfoHelper Instance
        {
            get
            {
                return instance;
            }
        }
    }
}
