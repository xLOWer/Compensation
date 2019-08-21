using comp_app.MVVM.Model.Common;
using comp_app.Services;
using System;
using System.Linq;
using static comp_app.Services.DbService;

namespace comp_app.MVVM.Model
{
    /// <summary>
    /// Документ
    /// </summary>
    [DbTableName("COMP_DOCUMENT")]
    public class Document : IRef
    {
        public Document WithNewId()
        {
            ID = DataRepository.Get<Document>().Max(x => x.ID) + 1;
            return this;
        }

        public Document()
        {
            ID = 0;
            NAME = "";
            STATUS_ID = 0;
            CLEARANCE_MOUNTH = 0;
            ACCURUAL_MOUNTH = 0;
            PROVIDER_ID = 0;
            COMPANY_ID = 0;
            ITEM_ID = 0;
            COMMENTS = "";
            FACT_SUM = 0;
            PLAN_SUM = 0;
            INVOICE_ID = 0;
            PAYMENT_METHOD_ID = 0;
            HAS_ORIGINAL = false;
            HAS_SCAN = false;
            DOCUMENT_DATE_TIME = DateTime.Now;
            COMPENSATION_DATE = DateTime.Now;
            // DOCUMENT_DATE_TIME = DbService.OracleConvert.ToOracleDateTimeString(DateTime.Now);
            // COMPENSATION_DATE = DbService.OracleConvert.ToOracleDateTimeString(DateTime.Now);

        }

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
        /// Статус
        /// </summary>
        public long STATUS_ID { get; set; }

        /// <summary>
        /// Месяц оформления
        /// </summary>
        public long CLEARANCE_MOUNTH { get; set; }

        /// <summary>
        /// Месяц начислений
        /// </summary>
        public long ACCURUAL_MOUNTH { get; set; }

        /// <summary>
        /// Поставщик
        /// </summary>
        public long PROVIDER_ID { get; set; }

        /// <summary>
        /// Компания
        /// </summary>
        public long COMPANY_ID { get; set; }

        /// <summary>
        /// Статья
        /// </summary>
        public long ITEM_ID { get; set; }

        /// <summary>
        /// Примечания
        /// </summary>
        public string COMMENTS { get; set; }

        /// <summary>
        /// Факт
        /// </summary>
        public decimal FACT_SUM { get; set; }

        /// <summary>
        /// План
        /// </summary>
        public decimal PLAN_SUM { get; set; }

        /// <summary>
        ///  № СФ
        /// </summary>
        public long INVOICE_ID { get; set; }

        /// <summary>
        /// Способ оплаты
        /// </summary>
        public long PAYMENT_METHOD_ID { get; set; }

        /// <summary>
        /// Имеется ли оригинал
        /// </summary>
        public bool HAS_ORIGINAL { get; set; }

        /// <summary>
        /// Имеется ли скан
        /// </summary>
        public bool HAS_SCAN { get; set; }

        /// <summary>
        /// Дата документа 
        /// </summary>
        public DateTime DOCUMENT_DATE_TIME { get; set; }

        /// <summary>
        /// Дата компенсации
        /// </summary>
        public DateTime COMPENSATION_DATE { get; set; }


        /*   вспомогательные элементы для удобства работы в c#   */
        internal Status Status => DataRepository.Statuses.FirstOrDefault(x => x.ID == STATUS_ID) ?? new Status();
        internal Provider Provider { get; }
        internal Company Company { get; }
        internal Item Item { get; set; }
        internal Invoice Invoice { get; }
        internal PaymentMethod PaymentMethod { get; }
    }
}