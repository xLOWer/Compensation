using System;
using System.Collections.Generic;

namespace comp_app.MVVM.Model
{
    public class Document
    {
        public Document()
        {

        }

        public string Id { get; set; }
        public string StatusId { get; set; } // статус
        public string ClearanceMounth { get; set; } // Месяц оформления
        public string AccurualMounth { get; set; } // Месяц начислений
        public string ProviderId { get; set; } // Поставщик
        public string CompanyId { get; set; } // Компания
        public string ItemId { get; set; } // Статья
        public string Comments { get; set; } // Примечания
        public string FactSum { get; set; } // Факт
        public string PlanSum { get; set; } // План
        public string InvoiceId { get; set; } // № СФ
        public string PaymentMethodId { get; set; } // Способ оплаты
        
        public DateTime CompensationDate => string.IsNullOrEmpty(_CompensationDate)?DateTime.MinValue:DateTime.Parse(_CompensationDate);// Дата документа
        public DateTime DocumentDateTime => string.IsNullOrEmpty(_DocumentDateTime) ? DateTime.MinValue : DateTime.Parse(_DocumentDateTime); // Дата компенсации
        public bool HasOriginal => true_list.Contains(_HasOriginal); // Оригинал
        public bool HasScan => true_list.Contains(_HasScan); // Скан


        /**/public string _HasOriginal { get; set; } 
        /**/public string _HasScan { get; set; } 
        /**/public string _DocumentDateTime { get; set; }
        /**/public string _CompensationDate { get; set; }



        private readonly List<string> true_list = new List<string>() { "1", "true", "TRUE", "True", "T", "t" };


    }
}