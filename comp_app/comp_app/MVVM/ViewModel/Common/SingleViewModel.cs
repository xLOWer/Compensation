using comp_app.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace comp_app.MVVM.ViewModel.Common
{
    public class SingleViewModel<TEntity, TView, TListView> : 
        ViewModelBase<TView>, 
        ISingleViewModel<TEntity, TView, TListView>
    {
        private TEntity _Item;
        public TEntity Item { get => _Item; set { _Item = value; NotifyPropertyChanged("Item"); } }

        public CommandService SaveCommand => new CommandService(Save);
        public CommandService CancelCommand => new CommandService(Cancel);
        public CommandService DeleteCommand => new CommandService(Delete);
        public CommandService RefreshCommand => new CommandService(Refresh);

        public virtual void Save(object o = null)
        {
            
        }

        public virtual void Cancel(object o = null)
        {
            TabService.CloseTab(View);
        }

        public virtual void Delete(object o = null)
        {

        }

        public virtual void Refresh(object o = null)
        {

        }



    }
}
