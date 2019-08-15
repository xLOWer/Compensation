namespace comp_app.MVVM.Model.Common
{
    public interface IRef
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        long ID { get; set; }
        /// <summary>
        /// Наименование
        /// </summary>
        string NAME { get; set; }
    }
}
