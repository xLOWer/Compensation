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

        public CommandService DeleteCommand => new CommandService(Delete);
        public CommandService RefreshCommand => new CommandService(Refresh);
        public CommandService EditCommand => new CommandService(Edit);
        public CommandService AddNewCommand => new CommandService(AddNew);

        public virtual void Delete(object o = null)
        {

        }

        public virtual void Refresh(object o = null)
        {

        }

        public virtual void Edit(object o = null)
        {
            var single = new DocumentSingleWindow(SelectedDocument);
        }

        public virtual void AddNew(object o = null)
        {
            var single = new DocumentSingleWindow(new Document());
        }



        public DocumentListViewModel()
        {
            DataRepository.Documents = new List<Document>
            {
                new Document()
                {
                    Id = "0",
                    StatusId ="0",
                    ClearanceMounth = new DateTime(2019, 01, 19).Month.ToString(),
                    AccurualMounth =  new DateTime(2019, 01, 19).Month.ToString(),
                    ProviderId = "0",
                    CompanyId = "0",
                    ItemId = "0",
                    Comments = "транспорт",
                    FactSum = "1093829,81",
                    PlanSum = "1093829,81",
                    _CompensationDate = "",
                    InvoiceId = "",
                    _DocumentDateTime = "",
                    PaymentMethodId = "",
                    _HasOriginal = "0",
                    _HasScan = "0"
                },
                new Document()
                {
                    Id = "1",
                    StatusId ="1",
                    ClearanceMounth =new DateTime(2019, 01, 19).Month.ToString(),
                    AccurualMounth = new DateTime(2019, 01, 19).Month.ToString(),
                    ProviderId = "0",
                    CompanyId = "0",
                    ItemId = "1",
                    Comments = "BMC",
                    FactSum = "2103308,49",
                    PlanSum = "2103308,49",
                    _CompensationDate = new DateTime(2019, 04, 02).ToString(),
                    InvoiceId = "0",
                    _DocumentDateTime = new DateTime(2019, 01, 31).ToString(),
                    PaymentMethodId = "0",
                    _HasOriginal = "1",
                    _HasScan = "0"
                },
                new Document()
                {
                    Id = "2",
                    StatusId ="1",
                    ClearanceMounth =new DateTime(2019, 01, 19).Month.ToString(),
                    AccurualMounth = new DateTime(2019, 01, 19).Month.ToString(),
                    ProviderId = "2",
                    CompanyId = "1",
                    ItemId = "2",
                    Comments = "BMC",
                    FactSum = "53588,56",
                    PlanSum = "53588,56",
                    _CompensationDate = new DateTime(2019, 04, 02).ToString(),
                    InvoiceId = "0",
                    _DocumentDateTime = new DateTime(2019, 01, 31).ToString(),
                    PaymentMethodId = "0",
                    _HasOriginal = "1",
                    _HasScan = "0"
                },
                new Document()
                {
                    Id = "3",
                    StatusId ="1",
                    ClearanceMounth =new DateTime(2019, 01, 19).Month.ToString(),
                    AccurualMounth = new DateTime(2019, 01, 19).Month.ToString(),
                    ProviderId = "0",
                    CompanyId = "0",
                    ItemId = "3",
                    Comments = "пермия BMC Прилож4А",
                    FactSum = "1128555,83",
                    PlanSum = "53588,56",
                    _CompensationDate = new DateTime(2019, 04, 02).ToString(),
                    InvoiceId = "",
                    _DocumentDateTime = "",
                    PaymentMethodId = "0",
                    _HasOriginal = "1",
                    _HasScan = "0"
                },
            };
        }
    }
}
