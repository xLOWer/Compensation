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
        public string HasOriginal { get; set; } // Оригинал
        public string HasScan { get; set; } // Скан
        public string DocumentDateTime { get; set; }// Дата документа 
        public string CompensationDate { get; set; }// Дата компенсации
    }
}