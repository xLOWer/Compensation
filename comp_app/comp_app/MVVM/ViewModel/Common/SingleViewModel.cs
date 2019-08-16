using comp_app.MVVM.Model.Common;
using comp_app.Services;
using DevExpress.Xpf.Core;
using System;
using System.Reflection;
using System.Windows;

namespace comp_app.MVVM.ViewModel.Common
{
    public class SingleViewModel<TEntity, TView, TListView> : 
        ViewModelBase<TView>, 
        ISingleViewModel<TEntity, TView, TListView> where TEntity: IRef
    {
        public SingleViewModel(TEntity item, ref TView _view)
        {
            _OnOpenState = Activator.CreateInstance<TEntity>();
            View = _view;

            // если в item попадает Null, то считается, что окно создаёт новую сущность, 
            // а иначе редактирует существующую
            newFlag = item == null; 

            if (item == null) Item = Activator.CreateInstance<TEntity>();            
            else Item = item;

            Set_OnOpenState();
        }

        /// <summary>
        /// Устанвливает все поля главной сущности в сущность-состояние, 
        /// которая отражает начальное состояние главной сущности чтобы 
        /// узнать, изменялась ли главная сущность.
        /// </summary>
        public void Set_OnOpenState()
        {
            foreach (var pr in typeof(TEntity).GetProperties(BindingFlags.Public | BindingFlags.Instance))
                pr.SetValue(_OnOpenState, pr.GetValue(Item, null), null);
        }

        /// <summary>
        /// Флаг, показывающий является ли сущность новой созданной или открытой существующей.
        /// Необходимо для понимания способа сохранения.
        /// </summary>
        public bool newFlag { get; set; } = true;

        /// <summary>
        /// Путём сравнения всех свойств определяет одинаковы ли главная сущность и сущность-состояние. 
        /// </summary>
        /// <returns>Возвращает true, если после открытия или сохранения главная сущность изменялась</returns>
        public bool IsEquals()
        {
            foreach(var pr in typeof(TEntity).GetProperties(BindingFlags.Public | BindingFlags.Instance))            
                if (!pr.GetValue(Item, null).Equals(pr.GetValue(_OnOpenState, null)))
                    return false;
            return true;
        }

        /// <summary>
        /// Сущность-состояние.
        /// Отражает начальное состояние главной сущности после открытия или последнего сохранения
        /// </summary>
        public TEntity _OnOpenState { get; set; }

        /// <summary>
        /// Главная сущность.
        /// Отражает редактируемый или создающийся объект
        /// </summary>
        public TEntity Item { get => _Item; set { _Item = value; NotifyPropertyChanged("Item"); } }
        private TEntity _Item;

        public CommandService SaveCommand => new CommandService(Save);
        public CommandService CancelCommand => new CommandService(Cancel);
        public CommandService DeleteCommand => new CommandService(Delete);

        public virtual void Save(object o = null)
        {
            var saveResult =
                DXMessageBox.Show("Подтверждаете сохранение изменённых данных?",
                                  "Сохранение",
                                  MessageBoxButton.YesNo,
                                  MessageBoxImage.Question);
            if (saveResult  == MessageBoxResult.Yes)
            {
                if (!IsEquals())
                {
                    if (newFlag) DataRepository.Add(Item);                    
                    else DataRepository.Update(Item);
                    Set_OnOpenState();
                    newFlag = false;
                }                    
            }
        }

        public virtual void Cancel(object o = null)
        {
            if (IsEquals())
            {
                TabService.CloseTab(View);
                return;
            }

            var saveResult = 
                DXMessageBox.Show("Подтверждаете сохранение изменённых данных?", 
                                  "Сохранение",
                                  MessageBoxButton.YesNo,
                                  MessageBoxImage.Question);

            if (saveResult == MessageBoxResult.Yes)
            {
                if (newFlag) DataRepository.Add(Item);
                else DataRepository.Update(Item);
            }
            TabService.CloseTab(View);

        }

        public virtual void Delete(object o = null)
        {
            var saveResult = 
                DXMessageBox.Show("Подтверждаете удаление?", 
                                  "Удаление",
                                  MessageBoxButton.YesNo,
                                  MessageBoxImage.Warning);

            if (saveResult == MessageBoxResult.Yes)
            {
                if (!newFlag) DataRepository.Remove(Item);                
                TabService.CloseTab(View);
            }
        }



    }
}
