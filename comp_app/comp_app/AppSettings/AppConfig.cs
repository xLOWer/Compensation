namespace comp_app.AppSettings
{
    public static class AppConfig
    {
        public const string  AppVersion = "0.0.1.1";

        public static string DbUserName { get; set; } = ""; // имя пользователя
        public static string DbUserPassword { get; set; } = ""; // пароль пользователя
        public static string DbPort { get; set; } = ""; // порт подключения  
        public static string DbSID { get; set; } = ""; // имя сервиса 
        public static string DbHost { get; set; } = ""; // ip/хост базы 

        public static string Schema { get; set; } = "DEVELOPER3";
        public static string connString => $"Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = {DbHost})(PORT = {DbPort}))(CONNECT_DATA = "
                               + $"(SERVER = DEDICATED)(SERVICE_NAME = {DbSID})));Password={DbUserPassword};User ID={DbUserName}";

        public static bool? EnableLogging { get; set; } = false; // включено ли логирование (выключено по-умолч.)    
        

        /*виртуальные свойства, не идущие в файл*/
        public static string ThemeName { get; set; } = "VS2017Light"; //VS2017Light  MetropolisDark      
    }
}
