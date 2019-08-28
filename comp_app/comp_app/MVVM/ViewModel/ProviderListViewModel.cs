using comp_app.MVVM.Model;
using comp_app.MVVM.View;
using comp_app.MVVM.ViewModel.Common;
using comp_app.Services;
using System;
using System.Collections.Generic;

namespace comp_app.MVVM.ViewModel
{
    public class ProviderListViewModel : ListViewModel<Provider, ProviderListView, ProviderSingleView>
    {
        public ProviderListViewModel(ref ProviderListView _view) : base(ref _view)
        {
            DataRepository.Refresh<Provider>();
        }     
    }
}
