using comp_app.MVVM.Model.Common;
using static comp_app.Services.DbService;

namespace comp_app.MVVM.Model
{
    /// <summary>
    /// Ссылка на СФ
    /// </summary>
    [DbTableName("COMP_INVOICE")]
    public class Invoice : IRef
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
        /// <summary>
        /// Номер
        /// </summary>
        public string NUMBER { get; set; }
    }
}
