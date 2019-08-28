using comp_app.MVVM.Model.Common;
using static comp_app.Services.DbService;

namespace comp_app.MVVM.Model
{
    /// <summary>
    /// Способ оплаты
    /// </summary>
    [DbTableName("COMP_PAYMENT_METHOD")]
    public class PaymentMethod : IRef
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
