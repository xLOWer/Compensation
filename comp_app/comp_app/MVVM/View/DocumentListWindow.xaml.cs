using comp_app.MVVM.ViewModel;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Grid.LookUp;
using System;
using System.Windows;
using System.Windows.Threading;
namespace comp_app.MVVM.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class DocumentListWindow : Window
    {
        private DocumentListViewModel _conext { get; set; }

        public DocumentListWindow()
        {
            _conext = new DocumentListViewModel();
            this.DataContext = _conext;
            DevExpress.Xpf.Core.ThemeManager.SetThemeName(this, "VS2017Light");
            InitializeComponent();
        }

        private void view_RowUpdated(object sender, RowEventArgs e)
        {
            var s = (TableView)sender;
            var r = e.Row;
            var d = _conext.SelectedDocument;
            _conext.RowUpdated(sender, e);
        }

        private void view_RowCanceled(object sender, RowEventArgs e)
        {

        }

        private void view_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            var d = _conext.SelectedDocument;
        }
    }
}
