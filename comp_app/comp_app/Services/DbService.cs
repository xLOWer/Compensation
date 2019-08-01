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

namespace comp_app.Services
{
    internal static partial class DbService
    {
        /// <summary>
        /// Запускает массив команд с возможностью задавать параметры
        /// </summary>
        /// <param name="commands">Команды</param>
        internal static void ExecuteCommand(List<OracleCommand> commands)
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
        /// <summary>
        /// Запускает одиночный запрос
        /// </summary>
        /// <param name="Sql">Запрос</param>
        /// <returns>DataTable с резльтатом запроса</returns>
        internal static DataTable Select(string Sql)
        {
            Utilites.Logger.Log($"[ORCL] {MethodBase.GetCurrentMethod().DeclaringType} {MethodBase.GetCurrentMethod().Name}");
            DataTable DataGridItems = ObjToDataTable(typeof(string));
            using (OracleCommand command = new OracleCommand())
            {
                command.Connection = Connection.conn;
                Connection.Check();
                OracleDataAdapter adapter = new OracleDataAdapter(Sql, Connection.conn);
                OracleCommandBuilder builder = new OracleCommandBuilder(adapter);
                DataGridItems.Clear();
                adapter.Fill(DataGridItems);
                
            }            
            return DataGridItems;
        }

        internal static void Insert(string Sql)
        {
            Utilites.Logger.Log($"[ORCL] {MethodBase.GetCurrentMethod().DeclaringType} {MethodBase.GetCurrentMethod().Name}");
            using (OracleCommand command = new OracleCommand(Sql))
            {
                command.Connection = Connection.conn;
                Connection.Check();
                command.ExecuteNonQuery();
                
            }
        }

        internal static void Insert(List<string> Sqls)
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

        internal static DataTable ObjToDataTable(Type type)
        {
            var dt = new DataTable();
            foreach (var info in type.GetProperties())
                dt.Columns.Add(info.Name);
            dt.AcceptChanges();
            return dt;
        }
        
        internal static class Sqls
        {
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

            internal static string GET_FAILED_DETAILS(string SENDER_ILN) =>
                $"SELECT * FROM {(AppSettings.AppConfig.Schema + ".")}EDI_GET_FAILED_DETAILS WHERE SENDER_ILN={SENDER_ILN}";

            internal static string Remove(string Id, Document o = null) => "delete documents where id=1";
            internal static string Remove(string Id, Company o = null) => "delete companies where id=1";

            internal static string Create(Document o) => "insert into documents() values()";
            internal static string Create(Company o) => "insert into companies() values()";

            internal static string Update(Document o, string Id) => "update documents set 1=1 where id=1";
            internal static string Update(Company o, string Id) => "update companies set 1=1 where id=1";       
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
                    !String.IsNullOrEmpty(AppSettings.AppConfig.DbSID) ||
                    !String.IsNullOrEmpty(AppSettings.AppConfig.DbPort) ||
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

    internal static partial class DbService<TModel>
    {
        internal static List<TModel> DocumentSelect(string Sql)
        {
            Utilites.Logger.Log($"[ORCL] {MethodBase.GetCurrentMethod().DeclaringType} {MethodBase.GetCurrentMethod().Name}");
            Utilites.Logger.Log("\t\t"+Sql);
            List<TModel> Documents = new List<TModel>();
            DataTable DataGridItems = ObjToDataTable(typeof(TModel));
            using (OracleCommand command = new OracleCommand())
            {
                command.Connection = DbService.Connection.conn;
                DbService.Connection.Check();
                OracleDataAdapter adapter = new OracleDataAdapter(Sql, DbService.Connection.conn);
                DataGridItems.Clear();
                adapter.Fill(DataGridItems);
            }

            Documents = ToListof(DataGridItems).ToList();
            return Documents;
        }
        /*
        internal static TModel ObjectToClass(object[] obj)
        {
            const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;
            TModel inst = Activator.CreateInstance<TModel>();
            var objectProperties = typeof(TModel).GetProperties(flags);
            int c = obj.Count();
            for (int i = 0; i < c; ++i)            
                objectProperties[i].SetValue(inst, obj[i].ToString(), null);  
            return inst;
        }*/

        internal static List<TModel> ToListof(DataTable dt)
        {
            const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;
            var columnNames = dt.Columns.Cast<DataColumn>()
                .Select( c => c.ColumnName )
                .ToList();
            var objectProperties = typeof( TModel ).GetProperties( flags );
            var targetList = dt.AsEnumerable().Select( dataRow =>
            {
                var instanceOfT = Activator.CreateInstance<TModel>();

                foreach (var properties in objectProperties.Where( properties => columnNames.Contains( properties.Name ) && dataRow[properties.Name] != DBNull.Value ))
                {
                    properties.SetValue( instanceOfT, dataRow[properties.Name], null );
                }
                return instanceOfT;
            } ).ToList();

            return targetList;
        }


        internal static DataTable ObjToDataTable(Type type)
        {
            var dt = new DataTable();
            foreach (var info in type.GetProperties())
                dt.Columns.Add( info.Name );
            dt.AcceptChanges();
            return dt;
        }

        internal static List<TModel> DocumentSelect(List<string> Sqls)
        {
            Utilites.Logger.Log($"[ORCL] {MethodBase.GetCurrentMethod().DeclaringType} {MethodBase.GetCurrentMethod().Name}");
            Utilites.Logger.Log("\t\tSqls.Count=" + Sqls.Count.ToString());
            List<TModel> Documents = new List<TModel>();
            DataTable DataGridItems = ObjToDataTable(typeof(TModel));
            using (OracleCommand command = new OracleCommand())
            {
                foreach (var Sql in Sqls)
                {
                    command.Connection = DbService.Connection.conn;
                    DbService.Connection.Check();
                    OracleDataAdapter adapter = new OracleDataAdapter(Sql, DbService.Connection.conn);
                    OracleCommandBuilder builder = new OracleCommandBuilder(adapter);
                    DataGridItems.Clear();
                    adapter.Fill(DataGridItems);
                }
            }

            Documents = ToListof(DataGridItems).ToList();
            return Documents;
        }


    }

}
