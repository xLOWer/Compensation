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
        public DocumentListWindow()
        {
            _context = new DocumentListViewModel();
            this.DataContext = _context;
            DevExpress.Xpf.Core.ThemeManager.SetThemeName(this, "VS2017Light");
            InitializeComponent();
        }

        private DocumentListViewModel _context { get; set; }
    }
}
