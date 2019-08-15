using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace comp_app.MVVM.ViewModel.Common
{
    public interface IListViewModel<TEntity, TSingleView, TListView> : 
        IViewModel<TListView>
    {
        List<TEntity> Items { get; set; }
        List<TEntity> SelectedItems { get; set; }

        void Edit(object o = null);
        void AddNew(object o = null);
        void Delete(object o = null);
        void Refresh(object o = null);
    }
}
