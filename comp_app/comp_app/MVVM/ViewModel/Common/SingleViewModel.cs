using comp_app.MVVM.Model.Common;
using comp_app.Services;
using DevExpress.Xpf.Core;
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
        ISingleViewModel<TEntity, TView, TListView> where TEntity: IRef
    {
        public SingleViewModel(TEntity item, ref TView _view)
        {
            View = _view;
            newFlag = item == null;

            if (item == null)            
                Item = Activator.CreateInstance<TEntity>();            
            else            
                Item = item;
            
            _OnOpenState = Item;
        }

        public bool newFlag { get; set; } = true;
        public bool IsEdited { get => !Item.Equals(_OnOpenState); } 
        public TEntity _OnOpenState { get; set; }

        private TEntity _Item;
        public TEntity Item { get => _Item; set { _Item = value; NotifyPropertyChanged("Item"); } }

        public CommandService SaveCommand => new CommandService(Save);
        public CommandService CancelCommand => new CommandService(Cancel);
        public CommandService DeleteCommand => new CommandService(Delete);

        public virtual void Save(object o = null)
        {
            if(DXMessageBox.Show("Подтверждаете сохранение изменённых данных?", "Сохранение", System.Windows.MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) 
                == System.Windows.MessageBoxResult.Yes)
            {
                if (newFlag)
                {
                    DataRepository.Add(Item);
                }
                else
                {
                    DataRepository.Update(Item);
                }
            }

        }

        public virtual void Cancel(object o = null)
        {
            if (DXMessageBox.Show("Закрыть текущую вкладку?", "Закрыть", System.Windows.MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question)
                == System.Windows.MessageBoxResult.Yes)
            {

            }
                TabService.CloseTab(View);
        }

        public virtual void Delete(object o = null)
        {

        }



    }
}
