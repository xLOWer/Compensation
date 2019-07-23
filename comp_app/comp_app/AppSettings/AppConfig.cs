namespace comp_app.AppSettings
{
    public static class AppConfig
    {
        public const string  AppVersion = "0.0.1.0";
        public static string DbUserName { get; set; } // имя пользователя
        public static string DbUserPassword { get; set; } // пароль пользователя
        public static string DbPort { get; set; } = "1521"; // порт подключения  
        public static string DbSID { get; set; } // имя сервиса 
        public static string DbHost { get; set; } // ip/хост базы 
        
        public static bool?  EnableLogging { get; set; } = false; // включено ли логирование (выключено по-умолч.)

        public static string Schema { get; set; } = "HPCSERVICE";
        public static string connString => $"Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = {DbHost})(PORT = {DbPort}))(CONNECT_DATA = "
                               + $"(SERVER = DEDICATED)(SERVICE_NAME = {DbSID})));Password={DbUserPassword};User ID={DbUserName}";

        public static bool?  EnableProxy { get; set; } = false; // включен ли трафик через прокси
        public static string ProxyUserName { get; set; } // имя пользователя для прокси
        public static string ProxyUserPassword { get; set; } // пароль пользователя прокси
        
    }
}
