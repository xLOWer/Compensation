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
    public class StatusSingleViewModel : SingleViewModel<Status, StatusSingleView, StatusListView>
    {
        public StatusSingleViewModel(Status _item, ref StatusSingleView _view) : base(_item, ref _view)
        {
        }
    }
}
