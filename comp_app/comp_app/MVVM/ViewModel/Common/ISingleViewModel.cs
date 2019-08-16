using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace comp_app.MVVM.ViewModel.Common
{
    public interface ISingleViewModel<TEntity, TSingleView, TListView> : 
        IViewModel<TSingleView>
    {
        TEntity Item { get; set; }

        void Save(object o = null);
        void Cancel(object o = null);
        void Delete(object o = null);
    }
}