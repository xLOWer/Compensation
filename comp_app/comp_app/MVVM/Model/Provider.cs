using comp_app.MVVM.Model.Common;
using static comp_app.Services.DbService;

namespace comp_app.MVVM.Model
{
    /// <summary>
    /// Поставщик
    /// </summary>
    [DbTableName("COMP_PROVIDER")]
    public class Provider : IRef
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
