namespace comp_app.MVVM.Model.Common
{
    public class AppConfig
    {
        public AppConfig()
        {
            DbUserName = AppSettings.AppConfig.DbUserName;
            DbUserPassword = AppSettings.AppConfig.DbUserPassword;
            Schema = AppSettings.AppConfig.Schema;
            DbHost = AppSettings.AppConfig.DbHost;
            DbPort = AppSettings.AppConfig.DbPort;
            DbSID = AppSettings.AppConfig.DbSID;
                       
            EnableLogging = AppSettings.AppConfig.EnableLogging;

            EnableProxy = AppSettings.AppConfig.EnableProxy;
            ProxyUserName = AppSettings.AppConfig.ProxyUserName;
            ProxyUserPassword = AppSettings.AppConfig.ProxyUserPassword;
        }

        public string DbUserName { get; set; }
        public string DbUserPassword { get; set; }
        public string DbPort { get; set; }
        public string DbSID { get; set; } 
        public string DbHost { get; set; }
                
        public bool? EnableLogging { get; set; } 

        public string Schema { get; set; }

        public bool? EnableProxy { get; set; }
        public string ProxyUserName { get; set; }
        public string ProxyUserPassword { get; set; }
    }
}
