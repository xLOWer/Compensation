using comp_app.MVVM.Model;
using comp_app.MVVM.View;
using comp_app.MVVM.ViewModel.Common;
using comp_app.Services;
using System;
using System.Collections.Generic;

namespace comp_app.MVVM.ViewModel
{
    public class StatusListViewModel : ListViewModel<Status, StatusListView, StatusSingleView>
    {
        public StatusListViewModel(ref StatusListView _view) : base(ref _view)
        {
            DataRepository.Refresh<Status>();
        }     
    }
}
