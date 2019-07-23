using comp_app.MVVM.Model;
using comp_app.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace comp_app.MVVM.ViewModel
{
    class DocumentSingleViewModel : INotifyPropertyChanged
    {        
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string info)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }

        private Document document;
        public Document Document
        {
            get
            {
                return document;
            }
            set
            {
                document = value;
                NotifyPropertyChanged("Document");
            }
        }

        public DocumentSingleViewModel(Document _document)
        {
            Document = _document;

            DataRepository.Statuses = new List<Status>
            {
                new Status(){Id="0", Name="Неоплачено" },
                new Status(){Id="1", Name="Оплачено" },
                new Status(){Id="2", Name="Брак" },
                new Status(){Id="3", Name="Неизвестно" },
            };

            DataRepository.Providers = new List<Provider>
            {
                new Provider(){ Id="0", Name="Шварцкопф" },
                new Provider(){ Id="1", Name="Колгейт" },
                new Provider(){ Id="2", Name="Джонсон-с" },
                new Provider(){ Id="3", Name="ЭсСиДжонсон" },
                new Provider(){ Id="4", Name="Бенкизер" },
                new Provider(){ Id="5", Name="Хайджин" },
                new Provider(){ Id="6", Name="СХЗ" },
            };

            DataRepository.Companies = new List<Company>
            {
                new Company(){Id="0", Name="ВИП" },
                new Company(){Id="1", Name="Вламур" },
                new Company(){Id="2", Name="ВИРЭЙ-ТМ" },
                new Company(){Id="3", Name="Владист" },
            };

            DataRepository.Items = new List<Item>
            {
                new Item(){Id="0", Name="Транспорт" },
                new Item(){Id="1", Name="Промо" },
                new Item(){Id="2", Name="Зарплата" },
                new Item(){Id="3", Name="Ретро" },
            };

            DataRepository.Invoices = new List<Invoice>
            {
                new Invoice(){Id="0", Number="6" },
                new Invoice(){Id="1", Number="1" },
                new Invoice(){Id="2", Number="2" },
                new Invoice(){Id="3", Number="3" },
                new Invoice(){Id="3", Number="4" },
                new Invoice(){Id="3", Number="5" },
            };

            DataRepository.PaymentMethods = new List<PaymentMethod>
            {
                new PaymentMethod(){Id="0", Name="6" },
                new PaymentMethod(){Id="1", Name="1" },
                new PaymentMethod(){Id="2", Name="2" },
            };
        }
    }
}
