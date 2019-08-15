using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace comp_app.MVVM.ViewModel.Common
{
    public interface IViewModel<TView> : INotifyPropertyChanged
    {
        TView View { get; set; }
        void NotifyPropertyChanged(string PropName);
    }
}
