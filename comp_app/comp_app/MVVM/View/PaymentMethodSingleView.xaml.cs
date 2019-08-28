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
    /// Логика взаимодействия для PaymentMethodSingleView.xaml
    /// </summary>
    public partial class PaymentMethodSingleView : Page
    {
        public PaymentMethodSingleView(PaymentMethod _PaymentMethod)
        {
            InitializeComponent();
            this.DataContext = new PaymentMethodSingleViewModel(_PaymentMethod, ref PaymentMethod_SingleView);
        }        
    }
}
