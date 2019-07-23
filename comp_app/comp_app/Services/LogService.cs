﻿using comp_app.AppSettings;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace comp_app.Services
{
    public static class LogService
    {
        private static string directoryPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        private static string dirName = "EdiClient";
        private static string fileName = "EdiClientLog.txt";
        private static string fullPath => Path.GetFullPath($"{directoryPath}\\{dirName}\\{fileName}");

        public static string Read()
        {
            var log = "";
            using (var stream = new FileStream(fullPath, FileMode.Open))
            {
                long c = stream.Length;
                byte[] buffer = new byte[c + 1];
                stream.Read(buffer, 0, (int)c);
                log = Encoding.Default.GetString(buffer);
                stream.Close();
            }
            return log;
        }

        public static void Log(string msg)
        {
            if (!AppConfig.EnableLogging ?? false) return;
            if (string.IsNullOrEmpty(msg)) return;
            string message = $"[{DateTime.UtcNow.ToShortDateString()} {DateTime.UtcNow.ToLongTimeString()}] "
                + $"{msg}\r\n";
            int c = message.Count();
            using (var stream = new FileStream(fullPath, FileMode.Append))
            {
                stream.Write(Encoding.Default.GetBytes(message), 0, c);
                stream.Close();
            }
        }
        


    }
}