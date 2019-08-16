﻿using System;
using System.Data;
using System.Linq;
using System.Reflection;
using Devart.Data.Oracle;
using System.Collections.Generic;
using comp_app.AppSettings;
using System.Windows;
using DevExpress.Xpf.Core;
using comp_app.MVVM.Model.Common;
using comp_app.MVVM.Model;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace comp_app.Services
{
    internal static partial class DbService
    {
        /// <summary>
        /// Запускает массив команд с возможностью задавать параметры
        /// </summary>
        /// <param name="commands">Команды</param>
        internal static void ExecuteCommands(List<OracleCommand> commands)
        {
            Utilites.Logger.Log($"[ORCL] {MethodBase.GetCurrentMethod().DeclaringType} {MethodBase.GetCurrentMethod().Name}");
            Utilites.Logger.Log("\t\tcount=" + commands.Count.ToString());
            foreach (var command in commands)
            {
                command.Connection = Connection.conn;
                Connection.Check();
                command.ExecuteNonQuery();
            }
        }
        
        internal static void ExecuteCommand(OracleCommand command)
        {
            Utilites.Logger.Log($"[ORCL] {MethodBase.GetCurrentMethod().DeclaringType} {MethodBase.GetCurrentMethod().Name}");
            using (command)
            {
                Connection.Check();
                command.Connection = Connection.conn;
                var res = command.ExecuteNonQuery();                
            }
        }

        internal static void ExecuteQuery(string Sql)
        {
            Utilites.Logger.Log($"[ORCL] {MethodBase.GetCurrentMethod().DeclaringType} {MethodBase.GetCurrentMethod().Name}");
            using (OracleCommand command = new OracleCommand(Sql))
            {
                command.Connection = Connection.conn;
                Connection.Check();
                command.ExecuteNonQuery();
                
            }
        }

        internal static void ExecuteQueries(List<string> Sqls)
        {
            Utilites.Logger.Log($"[ORCL] {MethodBase.GetCurrentMethod().DeclaringType} {MethodBase.GetCurrentMethod().Name}");
            int c = Sqls.Count();
            for (int i = 1; i <= c; ++i)
            {
                using (OracleCommand command = new OracleCommand(Sqls[i - 1]))
                {
                    command.Connection = Connection.conn;
                    Connection.Check();
                    command.ExecuteNonQuery();
                    
                }
            }
        }

        public static void InsertNewEntity<TEntity>(TEntity obj) where TEntity : IRef
        {
            var f = typeof(TEntity).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var entityOraAttr = (OracleTableAttribute)typeof(TEntity).GetCustomAttributes(typeof(OracleTableAttribute), false).FirstOrDefault();
            string values = "";
            string names = "";
            int i = 0;
            foreach (var prop in f)
            {
                names += (i > 0 ? ", " : "") + prop.Name;
                values += (i++ > 0 ? ", " : "") + JsonConvert.SerializeObject(prop.GetValue(obj, null));
            }
            var sql = $"insert into {entityOraAttr.SchemeName}.{entityOraAttr.TableName}({names})values({values.Replace("\"", "'")})";
            ExecuteQuery(sql);
        }

        public static void UpdateEntity<TEntity>(TEntity obj) where TEntity : IRef
        {
            var f = typeof(TEntity).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var entityOraAttr = (OracleTableAttribute)typeof(TEntity).GetCustomAttributes(typeof(OracleTableAttribute), false).FirstOrDefault();
            string updates = "";
            int i = 0;
            foreach (var prop in f)
            {
                updates += (i++ > 0 ? ", " : "") + prop.Name + "=" +JsonConvert.SerializeObject(prop.GetValue(obj, null));
            }
            var sql = $"update {entityOraAttr.SchemeName}.{entityOraAttr.TableName} SET {updates.Replace("\"", "'")} WHERE ID={obj.ID}";
            ExecuteQuery(sql);
        }

        public static void DeleteEntity<TEntity>(string id) where TEntity : IRef
        {
            var f = typeof(TEntity).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var entityOraAttr = (OracleTableAttribute)typeof(TEntity).GetCustomAttributes(typeof(OracleTableAttribute), false).FirstOrDefault();
            var sql = $"delete from {entityOraAttr.SchemeName}.{entityOraAttr.TableName} where ID={id}";
            ExecuteQuery(sql);
        }

        public static List<T> SelectEntity<T>()
        {
            List<T> list = new List<T>();
            var entityOraAttr = (OracleTableAttribute)typeof(T).GetCustomAttributes(typeof(OracleTableAttribute), false).FirstOrDefault();
            Connection.Check();
            var reader = new OracleCommand($"SELECT * FROM {entityOraAttr.SchemeName}.{entityOraAttr.TableName}", Connection.conn).ExecuteReader();
            int count = reader.FieldCount;
            var p0 = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            while (reader.Read())
            {
                var ins = Activator.CreateInstance<T>();
                for (int i = 0; i < count; i++)
                {
                    var p = p0.FirstOrDefault(x => x.Name.ToUpper() == reader.GetName(i).ToUpper());
                    p.SetValue(ins, Convert.ChangeType(Convert.ChangeType(reader.GetValue(i), p.PropertyType), p.PropertyType), null);
                }
                list.Add(ins);
            }
            return list;
        }
               

        internal static string SelectSingleValue(string Sql)
        {
            Utilites.Logger.Log($"[ORCL] {MethodBase.GetCurrentMethod().DeclaringType} {MethodBase.GetCurrentMethod().Name}");
            OracleDataReader reader;
            string retVal = "";
            using (OracleCommand command = new OracleCommand())
            {
                command.Connection = Connection.conn;
                command.CommandType = CommandType.Text;
                command.CommandText = Sql;
                Connection.conn.Open();
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    retVal = reader[0].ToString();
                }
                Connection.conn.Close();
            }
            return retVal;
        }
        




        internal static class Sqls
        {
            //internal static string SelectJsonString(string SchemeName, string TableName) => $"SELECT {SchemeName}.get_json('select * from {SchemeName}.{TableName}') json FROM dual";

            internal static string DatesBetween(DateTime Date)
                => $" BETWEEN {OracleDateFormat(Date)} AND {OracleDateFormat(Date)} ";

            internal static string DatesBetween(DateTime DateFrom, DateTime DateTo)
                => $" BETWEEN {OracleDateFormat(DateFrom)} AND {OracleDateFormat(DateTo)} ";

            internal static string OracleDateFormat(DateTime Date) => $" TO_DATE('{Date.Day}/{Date.Month}/{Date.Year}','DD/MM/YYYY') ";

            internal static string Sql_DateRange(string tableName, string fieldName, string sign, DateTime date1)
                => $"{tableName}.{fieldName} = {OracleDateFormat(date1)}\n";

            internal static string Sql_DateRange(string shortTableName, string fieldName, string sign, DateTime date1, DateTime date2)
                => $"{shortTableName}.{fieldName} {DatesBetween(date1, date2)}\n";

            internal static string ToOracleDate(DateTime Date)
            {
                var day = Date.Day < 10 ? $"0{Date.Day}" : Date.Day.ToString();
                var mouth = Date.Month < 10 ? $"0{Date.Month}" : Date.Month.ToString();
                return $"{day}.{mouth}. {Date.Year} {Date.Hour}:{Date.Minute}:{Date.Second}";
            }
        }






        public static class Connection
        {
            internal static OracleConnection conn { get; set; }
            public static string Timeout => conn?.ConnectionTimeout.ToString() ?? "ошибка";

            internal static void Configure()
            {
                if (!string.IsNullOrEmpty(AppSettings.AppConfig.connString) ||
                    !String.IsNullOrEmpty(AppSettings.AppConfig.DbUserName) ||
                    !String.IsNullOrEmpty(AppSettings.AppConfig.DbUserPassword) ||
                    //!String.IsNullOrEmpty(AppSettings.AppConfig.DbSID) ||
                    //!String.IsNullOrEmpty(AppSettings.AppConfig.DbPort) ||
                    !String.IsNullOrEmpty(AppSettings.AppConfig.DbHost))
                {
                    conn = new OracleConnection(AppSettings.AppConfig.connString);
                }
                else
                    DXMessageBox.Show("Соединение с базой не создано. Не верные параметры в строке соединения");
            }

            internal static void Open() => conn.Open();
            internal static void Close() => conn.Close();

            internal static void Check()
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();
            }

        }


    }


}
