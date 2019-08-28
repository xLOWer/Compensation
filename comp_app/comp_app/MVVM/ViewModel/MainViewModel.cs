using comp_app.MVVM.View;
using comp_app.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace comp_app.MVVM.ViewModel.Common
{
    public class MainViewModel : ViewModelBase<MainWindow>
    {
        public MainViewModel(ref MainWindow _view)
        {
            try
            {
                View = _view;
                DocumentsOpenCommand.Execute(null);
            }
            catch (Exception ex) { Utilites.Error(ex); }
        }

        public CommandService CloseCommand => new CommandService(Close);
        public CommandService StatusesOpenCommand => new CommandService(o => { TabService.NewTab(new StatusListView(), "Статусы"); });
        public CommandService DocumentsOpenCommand => new CommandService(o => { TabService.NewTab(new DocumentListView(), "Документы"); });
        public CommandService ItemsOpenCommand => new CommandService(o => { TabService.NewTab(new ItemListView(), "Статьи бюджета"); });
        public CommandService PaymentMethodsOpenCommand => new CommandService(o => { TabService.NewTab(new PaymentMethodListView(), "Способы оплаты"); });
        public CommandService ProvidersOpenCommand => new CommandService(o => { TabService.NewTab(new ProviderListView(), "Провайдеры"); });

        public virtual void Close(object o = null)
        {
            View.Close();
            AuthWindow auth = new AuthWindow();
        }
    }
}
