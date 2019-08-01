using comp_app.MVVM.Model;
using comp_app.MVVM.ViewModel.Common;
using comp_app.Services;
using comp_app.Services.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace comp_app.MVVM.ViewModel
{
    public class DocumentSingleViewModel : ViewModelBase<Document>, ISingleViewModel<Document>
    {
        public DocumentSingleViewModel(Document _document)
        {
            Document = _document;

            DataRepository.Statuses = new GenericRepository<Status>(new List<Status>
            {
                new Status(){Id="0", Name="Неоплачено" },
                new Status(){Id="1", Name="Оплачено" },
                new Status(){Id="2", Name="Брак" },
                new Status(){Id="3", Name="Неизвестно" },
            });
            DataRepository.Providers = new GenericRepository<Provider>(new List<Provider>
            {
                new Provider(){ Id="0", Name="Шварцкопф" },
                new Provider(){ Id="1", Name="Колгейт" },
                new Provider(){ Id="2", Name="Джонсон-с" },
                new Provider(){ Id="3", Name="ЭсСиДжонсон" },
                new Provider(){ Id="4", Name="Бенкизер" },
                new Provider(){ Id="5", Name="Хайджин" },
                new Provider(){ Id="6", Name="СХЗ" },
            });
            DataRepository.Companies = new GenericRepository<Company>(new List<Company>
            {
                new Company(){Id="0", Name="ВИП" },
                new Company(){Id="1", Name="Вламур" },
                new Company(){Id="2", Name="ВИРЭЙ-ТМ" },
                new Company(){Id="3", Name="Владист" },
            });
            DataRepository.Items = new GenericRepository<Item>(new List<Item>
            {
                new Item(){Id="0", Name="Транспорт" },
                new Item(){Id="1", Name="Промо" },
                new Item(){Id="2", Name="Зарплата" },
                new Item(){Id="3", Name="Ретро" },
            });
            DataRepository.Invoices = new GenericRepository<Invoice>(new List<Invoice>
            {
                new Invoice(){Id="0", Number="документ 6" },
                new Invoice(){Id="1", Number="документ 1" },
                new Invoice(){Id="2", Number="документ 2" },
                new Invoice(){Id="3", Number="документ 3" },
                new Invoice(){Id="4", Number="документ 4" },
                new Invoice(){Id="5", Number="документ 5" },
            });
            DataRepository.PaymentMethods = new GenericRepository<PaymentMethod>(new List<PaymentMethod>
            {
                new PaymentMethod(){Id="0", Name="вз" },
                new PaymentMethod(){Id="1", Name="на р/с" }
            });
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
        
        public CommandService SaveCommand => new CommandService(Save);
        public CommandService CancelCommand => new CommandService(Cancel);
        
        public void Save(object o = null)
        {        }

        public void Cancel(object o = null)
        { }
    }
}
