using comp_app.MVVM.ViewModel;
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
            InitializeComponent();
            DevExpress.Xpf.Core.ThemeManager.SetThemeName(this, "VS2017Light");
            this.DataContext = new DocumentListViewModel();
        }

    }
}
