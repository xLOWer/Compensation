using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace comp_app.MVVM.ViewModel.Common
{
    public interface IViewModel<T> : INotifyPropertyChanged
    {
        void Delete(object o = null);
        void Refresh(object o = null);
    }
}
