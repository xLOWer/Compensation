using comp_app.MVVM.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace comp_app
{
    public partial class AuthWindow : Window
    {
        public AuthWindow()
        {
            InitializeComponent();
            DataContext = new AuthViewModel(ref authWindow);
        }
    }
}
