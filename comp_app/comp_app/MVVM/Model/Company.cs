using comp_app.MVVM.Model.Common;
using static comp_app.Services.DbService;

namespace comp_app.MVVM.Model
{
    /// <summary>
    /// Компания
    /// </summary>
    [DbTableName("COMP_COMPANY")]
    public class Company : IRef
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
