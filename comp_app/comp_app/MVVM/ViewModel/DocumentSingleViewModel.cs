using comp_app.MVVM.Model;
using comp_app.MVVM.View;
using comp_app.MVVM.ViewModel.Common;
using comp_app.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace comp_app.MVVM.ViewModel
{
    public class DocumentSingleViewModel : SingleViewModel<Document, DocumentSingleWindow, DocumentListWindow>
    {
        public DocumentSingleViewModel(Document _document, ref DocumentSingleWindow _view) : base(_document, ref _view)
        {
        }
    }
}
