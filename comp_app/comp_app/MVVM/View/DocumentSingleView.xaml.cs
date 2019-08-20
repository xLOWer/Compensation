using comp_app.MVVM.Model;
using comp_app.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace comp_app.MVVM.View
{
    /// <summary>
    /// Логика взаимодействия для DocumentSingleView.xaml
    /// </summary>
    public partial class DocumentSingleView : Page
    {
        public DocumentSingleView(Document _document)
        {
            InitializeComponent();
            this.DataContext = new DocumentSingleViewModel(_document, ref docView);
        }        
    }
}
