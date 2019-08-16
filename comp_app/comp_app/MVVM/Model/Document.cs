using comp_app.MVVM.Model.Common;
using comp_app.Services;
using System;
using System.Linq;

namespace comp_app.MVVM.Model
{
    /// <summary>
    /// Документ
    /// </summary>
    [OracleTable(TableName = "COMP_DOCUMENT")]
    public class Document : IRef
    {
        public Document WithNewId()
        {
            ID = DataRepository.Get<Document>().Max(x => x.ID) + 1;
            return this;
        }

        public Document()
        {
            Status = new Status();
            Provider = new Provider();
            Company = new Company();
            Item = new Item();
            Invoice = new Invoice();
            PaymentMethod = new PaymentMethod();

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
            HAS_ORIGINAL = "0";
            HAS_SCAN = "0";
            DOCUMENT_DATE_TIME = DbService.OracleConvert.ToOracleDateTimeString(DateTime.Now);
            COMPENSATION_DATE = DbService.OracleConvert.ToOracleDateTimeString(DateTime.Now);

        }

        /// <summary>
        /// Идентификатор
        /// </summary>
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
        public string HAS_ORIGINAL
        {
            get => 
                new string[] { "1", "TRUE", "YES", "Y", "T" }
                .Any(x => x == __HAS_ORIGINAL.ToUpper()) 
                // если в хранимом свойстве лежит нечто похожее на правду
                ? "1" //возвращаем 1 как строку
                : "0"; // иначе 0 как строку

            set
            {
                __HAS_ORIGINAL = 
                    new string[] { "1", "TRUE", "YES", "Y", "T" }.Any(x => x == value.ToUpper()) 
                    ? "1" 
                    : "0";
            }
        }
        private string __HAS_ORIGINAL;

        /// <summary>
        /// Имеется ли скан
        /// </summary>
        public string HAS_SCAN
        {
            get => 
                new string[] { "1", "TRUE", "YES", "Y", "T" }
                .Any(x => x == __HAS_SCAN.ToUpper())
                // если в хранимом свойстве лежит нечто похожее на правду
                ? "1" //возвращаем 1 как строку
                : "0"; // иначе 0 как строку

            set
            {
                __HAS_SCAN = 
                    new string[] { "1", "TRUE", "YES", "Y", "T" }.Any(x => x == value.ToUpper())
                    ? "1" 
                    : "0";
            }
        }
        private string __HAS_SCAN;

        /// <summary>
        /// Дата документа 
        /// </summary>
        public string DOCUMENT_DATE_TIME { get; set; }

        /// <summary>
        /// Дата компенсации
        /// </summary>
        public string COMPENSATION_DATE { get; set; }


        /*   вспомогательные элементы для удобства работы в c#   */
        internal Status Status { get; set; }
        internal Provider Provider { get; set; }
        internal Company Company { get; set; }
        internal Item Item { get; set; }
        internal Invoice Invoice { get; set; }
        internal PaymentMethod PaymentMethod { get; set; }
    }
}