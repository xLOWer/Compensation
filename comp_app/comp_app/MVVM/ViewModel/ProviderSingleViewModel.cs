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
    public class ProviderSingleViewModel : SingleViewModel<Provider, ProviderSingleView, ProviderListView>
    {
        public ProviderSingleViewModel(Provider _item, ref ProviderSingleView _view) : base(_item, ref _view)
        {
        }
    }
}
