using comp_app.MVVM.Model;
using comp_app.MVVM.View;
using comp_app.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace comp_app.MVVM.ViewModel
{
    public class DocumentListViewModel : INotifyPropertyChanged
    {
        public DocumentListViewModel()
        {
            DataRepository.Documents = DataGenerator(20);
        }

        public event PropertyChangedEventHandler PropertyChanged;
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


        protected void NotifyPropertyChanged(string info)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }

        public void RowUpdated(object sender, DevExpress.Xpf.Grid.RowEventArgs e)
        {        }

        public void Delete(object o = null)
        {        }

        public void Refresh(object o = null)
        {        }

        public void Edit(object o = null)
        {            
            DocumentSingleWindow single = new DocumentSingleWindow(SelectedDocument);
            single.Show();
        }

        public void AddNew(object o = null)
        {        }

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
