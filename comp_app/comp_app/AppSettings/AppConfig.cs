namespace comp_app.AppSettings
{
    public static class AppConfig
    {
        public const string  AppVersion = "0.0.1.1";

        public static string DbUserName { get; set; } = "developer3"; // имя пользователя
        public static string DbUserPassword { get; set; } = "shishl0Dm"; // пароль пользователя
        public static string DbPort { get; set; } = "1521"; // порт подключения  
        public static string DbSID { get; set; } = "orcl.vladivostok.wera"; // имя сервиса 
        public static string DbHost { get; set; } = "192.168.2.13"; // ip/хост базы 

        public static string Schema { get; set; } = "DEVELOPER3";
        public static string connString => $"Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = {DbHost})(PORT = {DbPort}))(CONNECT_DATA = "
                               + $"(SERVER = DEDICATED)(SERVICE_NAME = {DbSID})));Password={DbUserPassword};User ID={DbUserName}";

        public static bool? EnableLogging { get; set; } = false; // включено ли логирование (выключено по-умолч.)    
        

        /*виртуальные свойства, не идущие в файл*/
        public static string ThemeName { get; set; } = "VS2017Light"; //VS2017Light  MetropolisDark      
    }
}
