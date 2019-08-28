using comp_app.MVVM.Model.Common;
using static comp_app.Services.DbService;

namespace comp_app.MVVM.Model
{
    /// <summary>
    /// Статус документа
    /// </summary>
    [DbTableName("COMP_STATUS")]
    public class Status : IRef
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [DbPrimaryKey]
        public long ID { get; set; }
        /// <summary>
        /// Наименование
        /// </summary>
        public string NAME { get; set; }
    }
}
