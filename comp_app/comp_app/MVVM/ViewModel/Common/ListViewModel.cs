using comp_app.MVVM.Model.Common;
using comp_app.MVVM.View;
using comp_app.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace comp_app.MVVM.ViewModel.Common
{
    public class ListViewModel<TEntity, TView, TSingleView> :
        ViewModelBase<TView>, 
        IListViewModel<TEntity, TSingleView, TView> where TView : new()
        where TEntity : IRef, new()
    {
        public ListViewModel(ref TView _view)
        {
            View = _view;
            Refresh();
        }
        private List<TEntity> _Items = new List<TEntity>();
        public List<TEntity> Items { get => _Items; set { _Items = value; NotifyPropertyChanged("Items"); } }

        private List<TEntity> _SelectedItems = new List<TEntity>();
        public List<TEntity> SelectedItems { get => _SelectedItems; set { _SelectedItems = value; NotifyPropertyChanged("SelectedItems"); } }

        public CommandService ItemsSourceChangedCommand => new CommandService(ItemsSourceChanged);
        public CommandService SelectedItemChangedCommand => new CommandService(SelectedItemChanged);
        public CommandService EditCommand => new CommandService(Edit);
        public CommandService AddNewCommand => new CommandService(AddNew);
        public CommandService DeleteCommand => new CommandService(Delete);
        public CommandService RefreshCommand => new CommandService(Refresh);

        public virtual void SelectedItemChanged(object o = null) => NotifyPropertyChanged("SelectedItems");        
        public virtual void ItemsSourceChanged(object o = null) => NotifyPropertyChanged("Items");
        
        public virtual void Edit(object o = null)
        {                      
            if (SelectedItems != null)
                if (SelectedItems.Count > 0)
                    foreach (var item in SelectedItems)
                    {
                        var cts = typeof(TSingleView).GetConstructors();
                        var ct = cts.FirstOrDefault();
                        var ivnct = ct.Invoke(new object[] { item });

                        TabService.NewTab((Page)ivnct, $"{item.ID}");
                    }
        }

        public virtual void AddNew(object o = null)
        {
            var a = typeof(TSingleView).GetConstructors();
                var b = a.FirstOrDefault(x =>x.IsConstructor);
                var c = (Page)b.Invoke(new object[] { null });
            TabService.NewTab(c, $"Новый документ");
        }
        public virtual void Delete(object o = null)
        {
            if (SelectedItems != null)
                if (SelectedItems.Count > 0)
                    foreach (var item in SelectedItems)                    
                        DataRepository.Remove(item);
            Refresh();
        }

        public virtual void Refresh(object o = null)
        {
            DataRepository.Refresh<TEntity>();
            Items = DataRepository.Get<TEntity>();
        }
    }
}
