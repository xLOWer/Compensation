using comp_app.MVVM.Model;
using comp_app.MVVM.View;
using comp_app.MVVM.ViewModel.Common;
using comp_app.Services;
using System;
using System.Collections.Generic;

namespace comp_app.MVVM.ViewModel
{
    public class PaymentMethodListViewModel : ListViewModel<PaymentMethod, PaymentMethodListView, PaymentMethodSingleView>
    {
        public PaymentMethodListViewModel(ref PaymentMethodListView _view) : base(ref _view)
        {
            DataRepository.Refresh<PaymentMethod>();
        }     
    }
}
