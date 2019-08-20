using comp_app.MVVM.Model.Common;
using static comp_app.Services.DbService;

namespace comp_app.MVVM.Model
{
    /// <summary>
    /// Статус документа
    /// </summary>
    [OracleTable(TableName = "COMP_STATUS")]
    public class Status : IRef
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [OracleType(typeof(long))]
        public long ID { get; set; }
        /// <summary>
        /// Наименование
        /// </summary>
        [OracleType(typeof(string))]
        public string NAME { get; set; }
    }
}
