using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace comp_app.MVVM.ViewModel.Common
{
    public class ViewModelBase<TView> :
        IViewModel<TView>
    {
        private TView _View;
        public TView View { get => _View; set { _View = value; NotifyPropertyChanged("View"); } }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string info)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }
    }
}
