using comp_app.MVVM.ViewModel;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Grid.LookUp;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
namespace comp_app.MVVM.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class PaymentMethodListView : Page
    {
        public PaymentMethodListView()
        {
            InitializeComponent();
            this.DataContext = new PaymentMethodListViewModel(ref PaymentMethod_ListView);
        }
    }
}
