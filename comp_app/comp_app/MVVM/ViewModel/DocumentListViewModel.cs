using comp_app.MVVM.Model;
using comp_app.MVVM.View;
using comp_app.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace comp_app.MVVM.ViewModel
{
    public class DocumentListViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string info)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }
        private Document selectedDocument;
        public Document SelectedDocument {
            get
            {
                return selectedDocument;
            }
            set
            {
                selectedDocument = value;
                NotifyPropertyChanged("SelectedDocument");
            }
        }
        //object sender, DevExpress.Xpf.Grid.RowEventArgs e

        public CommandService DeleteCommand => new CommandService(Delete);
        public CommandService RefreshCommand => new CommandService(Refresh);
        public CommandService EditCommand => new CommandService(Edit);
        public CommandService AddNewCommand => new CommandService(AddNew);
        public CommandService SaveCommand => new CommandService(Save);
        public CommandService CancelCommand => new CommandService(Cancel);


        public void Save(object o = null)
        {
            var d = SelectedDocument;
        }

        public void Cancel(object o = null)
        {
        }

        public void Delete(object o = null)
        {

        }
        public void Refresh(object o = null)
        {

        }

        public void Edit(object o = null)
        {
        }

        public void AddNew(object o = null)
        {
        }


        public DocumentListViewModel()
        {
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

            DataRepository.Documents = new List<Document>
            {              

            new Document()
                {
                    Id = "0",
                    StatusId ="0",
                    ClearanceMounth = "01/01/2019",
                    AccurualMounth =  "01/01/2019",
                    ProviderId = "0",
                    CompanyId = "0",
                    ItemId = "0",
                    Comments = "транспорт",
                    FactSum = "1093829,81",
                    PlanSum = "1093829,81",
                    CompensationDate = "",
                    InvoiceId = "",
                    DocumentDateTime = "",
                    PaymentMethodId = "",
                    HasOriginal = "0",
                    HasScan = "0"
                },
                new Document()
                {
                    Id = "1",
                    StatusId ="1",
                    ClearanceMounth ="01/01/2019",
                    AccurualMounth = "01/01/2019",
                    ProviderId = "0",
                    CompanyId = "0",
                    ItemId = "1",
                    Comments = "BMC",
                    FactSum = "2103308,49",
                    PlanSum = "2103308,49",
                    CompensationDate = new DateTime(2019, 04, 02).ToString(),
                    InvoiceId = "0",
                    DocumentDateTime = new DateTime(2019, 01, 31).ToString(),
                    PaymentMethodId = "0",
                    HasOriginal = "1",
                    HasScan = "0"
                },
                new Document()
                {
                    Id = "2",
                    StatusId ="1",
                    ClearanceMounth ="01/01/2019",
                    AccurualMounth = "01/01/2019",
                    ProviderId = "2",
                    CompanyId = "1",
                    ItemId = "2",
                    Comments = "BMC",
                    FactSum = "53588,56",
                    PlanSum = "53588,56",
                    CompensationDate = new DateTime(2019, 04, 02).ToString(),
                    InvoiceId = "0",
                    DocumentDateTime = new DateTime(2019, 01, 31).ToString(),
                    PaymentMethodId = "0",
                    HasOriginal = "1",
                    HasScan = "0"
                },
                new Document()
                {
                    Id = "3",
                    StatusId ="1",
                    ClearanceMounth ="01/02/2019",
                    AccurualMounth = "01/02/2019",
                    ProviderId = "0",
                    CompanyId = "0",
                    ItemId = "3",
                    Comments = "пермия BMC Прилож4А",
                    FactSum = "1128555,83",
                    PlanSum = "53588,56",
                    CompensationDate = new DateTime(2019, 04, 02).ToString(),
                    InvoiceId = "",
                    DocumentDateTime = "",
                    PaymentMethodId = "0",
                    HasOriginal = "1",
                    HasScan = "0"
                },
            };
        }
    }
}
