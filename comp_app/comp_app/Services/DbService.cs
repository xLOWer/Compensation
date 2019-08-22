using System;
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
        private const BindingFlags PublicInstance = BindingFlags.Public | BindingFlags.Instance;
        private static T GetAttrib<T>(Type attrOf) => (T)attrOf.GetCustomAttributes(typeof(T), false).FirstOrDefault();

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

        public static void InsertNewEntity<T>(T obj) where T : IRef
        {
            var tProperties = typeof(T).GetProperties(PublicInstance);
            var schemeAttr = GetAttrib<DbSchemeAttribute>(typeof(T));
            var scheme = schemeAttr == null ? "" : $"{schemeAttr.SchemeName}.";
            var table = GetAttrib<DbTableNameAttribute>(typeof(T))?.TableName ?? typeof(T).Name;
            string values = "";
            string names = "";
            int i = 0;
            foreach (var prop in tProperties)
            {
                names += (i > 0 ? ", " : "") + prop.Name;
                values += (i++ > 0 ? ", " : "") + OracleConvert.DbOutcomingTypeConterter(obj, prop);
            }
            var sql = $"insert into {scheme}{table}({names})values({values.Replace("\"", "'")})";
            //ExecuteQuery(sql);
        }               

        public static void UpdateEntity<T>(T obj)
        {
            var tProperties = typeof(T).GetProperties(PublicInstance);
            var schemeAttr = GetAttrib<DbSchemeAttribute>(typeof(T));
            var scheme = schemeAttr == null ? "" : $"{schemeAttr.SchemeName}.";
            var table = GetAttrib<DbTableNameAttribute>(typeof(T))?.TableName ?? typeof(T).Name;
            var fieldNameAttr = GetAttrib<DbPrimaryKeyAttribute>(typeof(T));
            string updates = "";
            var idField = "id";
            var idFieldValue = "";
            int i = 0;
            foreach (var prop in tProperties)
            {
                var nameAttr = GetAttrib<DbFieldNameAttribute>(prop.PropertyType);
                var propName = nameAttr == null ? prop.Name : nameAttr.FieldName;

                if (prop.GetCustomAttributes(typeof(DbPrimaryKeyAttribute), false) != null)
                {
                    idField = propName;
                    idFieldValue = prop.GetValue(obj, null).ToString();
                }
                else
                {
                    updates += (i++ > 0 ? ", " : "") + propName + "=" + OracleConvert.DbOutcomingTypeConterter(obj, prop);
                }                
            }
            
            var sql = $"update {scheme}{table} SET {updates.Replace("\"", "'")} WHERE {idField}={idFieldValue}";
            //ExecuteQuery(sql);
        }

        public static void DeleteEntity<T>(T obj) where T : IRef
        {
            var schemeAttr = GetAttrib<DbSchemeAttribute>(typeof(T));
            var scheme = schemeAttr == null ? "" : $"{schemeAttr.SchemeName}.";
            var table = GetAttrib<DbTableNameAttribute>(typeof(T))?.TableName ?? typeof(T).Name;
            var primaryKey = typeof(T).GetProperties(PublicInstance).FirstOrDefault(x => GetAttrib<DbPrimaryKeyAttribute>(x.PropertyType) != null);
            var idField = primaryKey == null ? "id" : primaryKey.Name;
            var idFieldValue = primaryKey.GetValue(obj, null).ToString() ?? throw new NullReferenceException("При удалении объекта в базе не указан его идентификатор");
            var sql = $"delete from {scheme}{table} where {idField}={idFieldValue}";
            //ExecuteQuery(sql);
        }

        public static void DeleteEntity<T>(string id) where T : IRef
        {
            if(string.IsNullOrEmpty(id)) throw new ArgumentNullException("При удалении объекта в базе не указан его идентификатор или указан пустой");
            var tableAttr = GetAttrib<DbTableNameAttribute>(typeof(T));
            var schemeAttr = GetAttrib<DbSchemeAttribute>(typeof(T));
            var scheme = $"{schemeAttr?.SchemeName}." ?? "";
            var table = tableAttr?.TableName ?? typeof(T).Name;
            var primaryKey = typeof(T).GetProperties(PublicInstance).FirstOrDefault(x => GetAttrib<DbPrimaryKeyAttribute>(x.PropertyType) != null);
            var idField = primaryKey == null ? "id" : primaryKey.Name;
            var sql = $"delete from {scheme}{table} where {idField}={id}";
            //ExecuteQuery(sql);
        }

        public static List<T> SelectEntity<T>()
        {
            List<T> list = new List<T>();
            var tableAttr = GetAttrib<DbTableNameAttribute>(typeof(T));
            var schemeAttr = GetAttrib<DbSchemeAttribute>(typeof(T));
            var scheme = schemeAttr == null ? "" : $"{schemeAttr.SchemeName}.";
            var table = tableAttr?.TableName ?? typeof(T).Name;

            Connection.Check();
            var reader = new OracleCommand($"SELECT * FROM {scheme}{table}", Connection.conn).ExecuteReader();
            int count = reader.FieldCount;
            var p0 = typeof(T).GetProperties(PublicInstance);

            while (reader.Read())
            {
                var ins = Activator.CreateInstance<T>();
                for (int i = 0; i < count; i++)
                {
                    var p = p0.FirstOrDefault(x => x.Name.ToUpper() == reader.GetName(i).ToUpper());
                    var FieldType = reader.GetFieldType(i);
                    var val = OracleConvert.DbIncomingTypeConterter(reader.GetValue(i), p.PropertyType, FieldType);
                    p.SetValue(ins, val, null);
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
        




        internal static class OracleConvert
        {
            //internal static string SelectJsonString(string SchemeName, string TableName) => $"SELECT {SchemeName}.get_json('select * from {SchemeName}.{TableName}') json FROM dual";

            internal static string ToOracleDatesBetweenString(DateTime Date)
                => $" BETWEEN {ToOracleVarcharDateString(Date)} AND {ToOracleVarcharDateString(Date)} ";

            internal static string ToOracleDatesBetweenString(DateTime DateFrom, DateTime DateTo)
                => $" BETWEEN {ToOracleVarcharDateString(DateFrom)} AND {ToOracleVarcharDateString(DateTo)} ";

            internal static string ToOracleVarcharDateString(DateTime Date) => $" TO_DATE('{Date.Day}/{Date.Month}/{Date.Year}','DD/MM/YYYY') ";

            internal static string ToOracleVarcharDateTimeString(DateTime Date) => $" TO_DATE('{ToOracleDateTimeString(Date)}','DD.MM.YYYY HH24:mi:ss') ";

            internal static string ToOracleFieldDatesBetweenString(string shortTableName, string fieldName, string sign, DateTime date1, DateTime date2)
                => $"{shortTableName}.{fieldName} {ToOracleDatesBetweenString(date1, date2)}\n";

            /// <summary>
            /// Преобразовывает System.DateTime в строку даты-время Oracle
            /// </summary>
            /// <param name="Date"></param>
            /// <returns>Возвращает строку в формате DD.MM.YYYY HH24:mi:ss</returns>
            internal static string ToOracleDateTimeString(DateTime Date)
            {
                var day = Date.Day < 10 ? $"0{Date.Day}" : Date.Day.ToString();
                var mouth = Date.Month < 10 ? $"0{Date.Month}" : Date.Month.ToString();
                return $"{day}.{mouth}.{Date.Year} {Date.Hour}:{Date.Minute}:{Date.Second}";
            }


            internal static DateTime ToDateTime(string OracleDateString, string format)
            {
                try
                {
                    return DateTime.ParseExact(OracleDateString, format, null);
                }
                catch (Exception ex)
                {
                    return DateTime.Now;
                }
            }

            internal static DateTime ToDateTime(string OracleDateString)
            {
                try
                {
                    return DateTime.ParseExact(OracleDateString, "d.M.yyyy H:mm:ss", null);
                }
                catch (Exception ex)
                {
                    return DateTime.Now;
                }
            }

            internal static string DbOutcomingTypeConterter<T>(T obj, PropertyInfo prop)
            {
                object propVal = prop.GetValue(obj, null);

                switch (Type.GetTypeCode(prop.PropertyType))
                {
                    case TypeCode.Int64 | TypeCode.Decimal | TypeCode.Int32 | TypeCode.Single | TypeCode.Double | TypeCode.Int16 | TypeCode.UInt64 | TypeCode.UInt32 | TypeCode.UInt16:
                        return $"{propVal.ToString()}";
                        
                    case TypeCode.DateTime:
                        return ToOracleVarcharDateTimeString((DateTime)propVal);
                        
                    case TypeCode.Boolean:
                        return $"\"{((new string[] { "TRUE", "1", "T", "Y", "YES" }).Any(x => x == propVal.ToString().ToUpper()) ? "1" : "0")}\"";
                        
                    default:
                        return $"\"{propVal.ToString()}\"";
                }
                
            }

            internal static object DbIncomingTypeConterter(object obj, Type PropType, Type type)
            {
                switch (Type.GetTypeCode(PropType))
                {
                    case TypeCode.DateTime:
                        break;
                    case TypeCode.Boolean:
                        var tempStr = Convert.ChangeType(obj, typeof(string)).ToString().ToUpper();
                        return (new string[] { "TRUE", "1", "T", "Y", "YES" }).Any(x => x == tempStr);
                    default:
                        return Convert.ChangeType(obj, PropType);
                }


                return obj;
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

        public class DbTableNameAttribute : Attribute
        {
            public DbTableNameAttribute(string _TableName = null)
            {
                this.TableName = _TableName;
            }
            public string TableName { get; set; }
        }

        public class DbSchemeAttribute : Attribute
        {
            public DbSchemeAttribute(string _SchemeName = null)
            {
                this.SchemeName = _SchemeName ?? comp_app.AppSettings.AppConfig.Schema;
            }
            public string SchemeName { get; set; }
        }

        public class DbFieldNameAttribute : Attribute
        {
            public DbFieldNameAttribute(string _fieldName = null)
            {
                this.FieldName = _fieldName;
            }
            public string FieldName { get; set; }
        }

        public class DbPrimaryKeyAttribute : Attribute { }

    }


}
