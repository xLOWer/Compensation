﻿namespace comp_app.MVVM.Model.Common
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

        public string TraderUserName { get; set; }
        public string TraderUserPassword { get; set; }

        public int? EdiTimeout { get; set; } 
        public string EdiUser { get; set; }
        public string EdiPassword { get; set; } 
        public string EdiGLN { get; set; } 
        public string EdiEmail { get; set; }
        public string EdiUrl { get; set; } 

        public bool? EnableAutoHandler { get; set; }
        public int? AutoHandlerPeriod { get; set; } 

        public bool? EnableLogging { get; set; } 

        public string Schema { get; set; }

        public bool? EnableProxy { get; set; }
        public string ProxyUserName { get; set; }
        public string ProxyUserPassword { get; set; }
    }
}
