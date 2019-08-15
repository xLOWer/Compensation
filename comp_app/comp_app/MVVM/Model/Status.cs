using comp_app.MVVM.Model.Common;

namespace comp_app.MVVM.Model
{
    /// <summary>
    /// Статус документа
    /// </summary>
    public class Status : IRef
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
