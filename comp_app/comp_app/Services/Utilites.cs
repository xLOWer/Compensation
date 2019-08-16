using comp_app.AppSettings;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Windows;

namespace comp_app.Services
{
    public static class Utilites
    {
        internal static void Error(Exception ex)
        {
            Logger.Log($"===============================");
            var msg = $"Message: {ex.Message}\n\nSource: {ex.Source}\n\n{GetInnerExceptionMessage(ex)}\n\nTargetSite: {ex?.TargetSite}\n\n{ex.InnerException?.Message}\n\nStackTrace: {ex.StackTrace}";                
            Logger.Log(msg);
            Logger.Log($"===============================");
            DevExpress.Xpf.Core.DXMessageBox.Show( msg, "ОШИБКА", MessageBoxButton.OK, MessageBoxImage.Error );
        }

        internal static void Error(string text)
        {
            Logger.Log($"===============================");
            Logger.Log($"[ОШИБКА] {text}");
            Logger.Log($"===============================");
            DevExpress.Xpf.Core.DXMessageBox.Show( $"[ОШИБКА] {text}", "ОШИБКА", MessageBoxButton.OK, MessageBoxImage.Error );
        }

        private static string GetInnerExceptionMessage(Exception ex)
            => ex.InnerException != null ? ex.Message + GetInnerExceptionMessage( ex.InnerException ) : $"\ninner: {ex.Message}\n{ex.Data}\n{ex.Source}\n{ex.TargetSite}\n===========\n";
        
        public static string Time { get; set; }

        public static class Logger
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

        //private const string key = "fYjQuN";

        public static string XorAndCompress(string input)
        {
            //var xor_text = xorIt(input, key); // {"DbUserName":null,"DbUserPassword":null,"DbPort":"1521","DbSID":null,"DbHost":null,"EnableLogging":false,"Schema":"HPCSERVICE","EnableProxy":false,"ProxyUserName":null,"ProxyUserPassword":null}
            var compress_text = Compress(input); //
            return compress_text; // 
        }

        public static string DecompressAndUnxor(string input)
        {
            var decompress_text = Decompress(input);//
            //var unxor_text = xorIt(decompress_text, key);//
            return decompress_text;//
        }

        public static void TestDbLoadingSpeed(string name, Action act, int count)
        {
            List<double> ret = new List<double>();
            Stopwatch watch = new Stopwatch();
            int i = 0;
            for (; i < count; i++)
            {
                watch.Reset();
                watch.Start();

                try
                {
                    act.Invoke();
                }
                catch (Exception ex)
                {
                    watch.Stop();
                    Utilites.Error(ex);
                }
                finally
                {
                    watch.Stop();
                    ret.Add((double)watch.ElapsedMilliseconds / 1000);
                }
            }
            MessageBox.Show(
                $@"{name}
число запусков: {count}
суммарное время: {ret.Sum(x => x)}
максимальное время: {ret.Max(x => x)}
минимальное время: {ret.Min(x => x)}
среднее время: {ret.Average(x => x)}");
        }


        public static string xorIt(string text, string key)
        {
            var result = new StringBuilder();

            for (int c = 0; c < text.Length; c++)
            {
                char character = text[c];
                uint charCode = (uint)character;
                int keyPosition = c % key.Length;
                char keyChar = key[keyPosition];
                uint keyCode = (uint)keyChar;
                uint combinedCode = charCode ^ keyCode;
                char combinedChar = (char)combinedCode;
                result.Append(combinedChar);
            }
            return result.ToString();
        }

        public static string Compress(string uncompressedString)
        {
            byte[] compressedBytes;
            using (var uncompressedStream = new MemoryStream(Encoding.UTF8.GetBytes(uncompressedString)))
            {
                using (var compressedStream = new MemoryStream())
                {
                    using (var compressorStream = new DeflateStream(compressedStream, CompressionMode.Compress, true))
                    {
                        uncompressedStream.CopyTo(compressorStream);
                    }
                    compressedBytes = compressedStream.ToArray();
                }
            }

            return Convert.ToBase64String(compressedBytes);
        }
        
        public static string Decompress(string compressedString)
        {
            byte[] decompressedBytes;
            var compressedStream = new MemoryStream(Convert.FromBase64String(compressedString));
            using (var decompressorStream = new DeflateStream(compressedStream, CompressionMode.Decompress))
            {
                using (var decompressedStream = new MemoryStream())
                {
                    decompressorStream.CopyTo(decompressedStream);
                    decompressedBytes = decompressedStream.ToArray();
                }
            }
            return Encoding.UTF8.GetString(decompressedBytes);
        }
    }
}