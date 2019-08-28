using comp_app.MVVM.Model;
using comp_app.MVVM.View;
using comp_app.MVVM.ViewModel.Common;
using comp_app.Services;
using System;
using System.Collections.Generic;

namespace comp_app.MVVM.ViewModel
{
    public class ItemListViewModel : ListViewModel<Item, ItemListView, ItemSingleView>
    {
        public ItemListViewModel(ref ItemListView _view) : base(ref _view)
        {
            DataRepository.Refresh<Status>();
        }     
    }
}
