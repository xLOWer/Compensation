using comp_app.MVVM.Model.Common;

namespace comp_app.MVVM.Model
{
    /// <summary>
    /// Компания
    /// </summary>
    public class Company : IRef
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public long ID { get; set; }
        /// <summary>
        /// Наименование
        /// </summary>
        public string NAME { get; set; }
    }
}
