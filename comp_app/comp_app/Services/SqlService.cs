using comp_app.AppSettings;
using System;

namespace comp_app.Services
{
    internal static class SqlService
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
            $"SELECT * FROM {(AppConfig.Schema+".")}EDI_GET_FAILED_DETAILS WHERE SENDER_ILN={SENDER_ILN}";

    }

}