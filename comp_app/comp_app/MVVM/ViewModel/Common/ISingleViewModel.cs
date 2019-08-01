using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace comp_app.MVVM.ViewModel.Common
{
    public interface ISingleViewModel<T> : IViewModel<T>
    {
        T Document { get; set; }
        void Save(object o = null);
        void Cancel(object o = null);
    }
}
