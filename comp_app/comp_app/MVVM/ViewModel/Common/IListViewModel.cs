using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace comp_app.MVVM.ViewModel.Common
{
    public interface IListViewModel<T> : IViewModel<T>
    {
        T SelectedDocument { get; set; }
        void Edit(object o = null);
        void AddNew(object o = null);

    }
}
