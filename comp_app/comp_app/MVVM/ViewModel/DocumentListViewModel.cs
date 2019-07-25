using comp_app.MVVM.Model;
using comp_app.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;

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

        public void RowUpdated(object sender, DevExpress.Xpf.Grid.RowEventArgs e)
        {

        }

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
            DataRepository.Documents = DataGenerator(1000);

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
                new Invoice(){Id="0", Number="документ 6" },
                new Invoice(){Id="1", Number="документ 1" },
                new Invoice(){Id="2", Number="документ 2" },
                new Invoice(){Id="3", Number="документ 3" },
                new Invoice(){Id="4", Number="документ 4" },
                new Invoice(){Id="5", Number="документ 5" },
            };

            DataRepository.PaymentMethods = new List<PaymentMethod>
            {
                new PaymentMethod(){Id="0", Name="вз" },
                new PaymentMethod(){Id="1", Name="на р/с" }
            };         


        }

        private List<Document> DataGenerator(int n)
        {
            Random r = new Random();            
            var list = new List<Document>();

            for (int i = 0; i <= n; ++i)
            {
                DateTime date = DateTime.Now;
                try
                {
                    date = DateTime.Parse($"{r.Next(10, 30).ToString()}.0{r.Next(1, 9).ToString()}.201{r.Next(3, 9).ToString()}");
                }
                catch (Exception ex) { }
                var sum = (r.NextDouble() * 1000).ToString();
                list.Add(new Document()
                {
                    Id = i.ToString(),
                    StatusId = r.Next(0, 3).ToString(),
                    ClearanceMounth = date.ToString(),
                    AccurualMounth = date.ToString(),
                    ProviderId = r.Next(0, 6).ToString(),
                    CompanyId = r.Next(0, 3).ToString(),
                    ItemId = r.Next(0, 3).ToString(),
                    Comments = "Comments",
                    FactSum = sum,
                    PlanSum = sum,
                    CompensationDate = date.ToString(),
                    InvoiceId = r.Next(0, 5).ToString(),
                    DocumentDateTime = date.ToString(),
                    PaymentMethodId = (r.Next(1, 2)-1).ToString(),
                    HasOriginal = r.Next(0, 3) == 0 ? "False" : "True",
                    HasScan = r.Next(0, 3) == 0 ? "False" : "True"
                });
            }
            return list;
        }
        


    }
}
