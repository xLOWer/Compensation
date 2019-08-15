using comp_app.MVVM.Model.Common;

namespace comp_app.MVVM.Model
{
    /// <summary>
    /// Статья бюджета
    /// </summary>
    public class Item : IRef
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
