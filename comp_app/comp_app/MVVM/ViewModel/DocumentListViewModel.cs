using comp_app.MVVM.Model;
using comp_app.MVVM.View;
using comp_app.MVVM.ViewModel.Common;
using comp_app.Services;
using comp_app.Services.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace comp_app.MVVM.ViewModel
{
    public class DocumentListViewModel : ViewModelBase<Document>, IListViewModel<Document>
    {
        public DocumentListViewModel()
        {
            DataRepository.Documents = new GenericRepository<Document>(DataGenerator(50));
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

        public CommandService EditCommand => new CommandService(Edit);
        public CommandService AddNewCommand => new CommandService(AddNew);

        public override void Delete(object o = null)
        {
            DataRepository.Documents.Remove(SelectedDocument, (s) =>
            {
                // код удаления
                return false;
            });
            
        }

        public override void Refresh(object o = null)
        {        }

        public void Edit(object o = null)
        {            
            DocumentSingleWindow single = new DocumentSingleWindow(SelectedDocument);
            single.Activate();
            single.Show();
        }

        public void AddNew(object o = null)
        {
            DocumentSingleWindow single = new DocumentSingleWindow(new Document());
            single.Activate();
            single.Show();
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
