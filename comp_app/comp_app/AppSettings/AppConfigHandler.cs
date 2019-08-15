using comp_app.Services;
using System;
using System.IO;

namespace comp_app.AppSettings
{
    public static class AppConfigHandler
    {
        private static string directoryPath = ""/*Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)*/;
        private static string dirName = "CompApp";
        private static string fileName = "CompAppConf.xcnf";
        private static string fullPath => Path.GetFullPath($"{fileName}");
        public static string LogPath => Path.GetFullPath($"{dirName}");
        
        public static void ConfigureOracle()
        {
            Utilites.Logger.Log("[OracleConnectionService]");
            try { DbService.Connection.Configure(); }
            catch (Exception ex) { Utilites.Error(ex); }
            Utilites.Logger.Log("\t\tClientVersion " + DbService.Connection.conn.ClientVersion);
            Utilites.Logger.Log("\t\tServer " + DbService.Connection.conn.Server);
        }

        public static void Load()
        {
            if (!Directory.Exists($"{directoryPath}\\{dirName}"))
                Directory.CreateDirectory($"{directoryPath}\\{dirName}");

            if (!File.Exists(fullPath))
                Create();
            Read();

            Utilites.Logger.Log($"\t\tconf_dir {directoryPath}\\{dirName}");
        }

        public static void Save() => Create();
        public static void Create() => JsonService.Write(new MVVM.Model.Common.AppConfig(), fullPath);

        public static void Read()
        {
            Utilites.Logger.Log("[APPCONFIG]");
            var newLoadedConfig = JsonService.Read<MVVM.Model.Common.AppConfig>(fullPath) ?? new MVVM.Model.Common.AppConfig();

            AppConfig.DbUserName = !string.IsNullOrEmpty(newLoadedConfig.DbUserName) ? newLoadedConfig.DbUserName : AppConfig.DbUserName;
            AppConfig.DbUserPassword = !string.IsNullOrEmpty(newLoadedConfig.DbUserPassword) ? newLoadedConfig.DbUserPassword : AppConfig.DbUserPassword;
            AppConfig.DbHost = !string.IsNullOrEmpty(newLoadedConfig.DbHost) ? newLoadedConfig.DbHost : AppConfig.DbHost;
            AppConfig.DbPort = !string.IsNullOrEmpty(newLoadedConfig.DbPort) ? newLoadedConfig.DbPort : AppConfig.DbPort;
            AppConfig.DbSID = !string.IsNullOrEmpty(newLoadedConfig.DbSID) ? newLoadedConfig.DbSID : AppConfig.DbSID;
            
            AppConfig.EnableLogging = newLoadedConfig.EnableLogging ?? AppConfig.EnableLogging;

            AppConfig.Schema = !string.IsNullOrEmpty(newLoadedConfig.Schema) ? newLoadedConfig.Schema : AppConfig.Schema;

            Utilites.Logger.Log("\t\tDbUserName " + AppConfig.DbUserName);
            Utilites.Logger.Log("\t\tDbHost " + AppConfig.DbHost);
        }

        


    }
}
