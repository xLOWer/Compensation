using comp_app.MVVM.Model.Common;

namespace comp_app.MVVM.Model
{
    /// <summary>
    /// Ссылка на СФ
    /// </summary>
    public class Invoice : IRef
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
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
