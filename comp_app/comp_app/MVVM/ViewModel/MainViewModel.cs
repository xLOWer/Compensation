﻿using comp_app.MVVM.View;
using comp_app.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace comp_app.MVVM.ViewModel.Common
{
    public class MainViewModel : ViewModelBase<MainWindow>
    {
        public MainViewModel(ref MainWindow _view)
        {
            try
            {
                View = _view;
                TabService.NewTab(new DocumentListWindow(), "Документы");
            }
            catch (Exception ex) { Utilites.Error(ex); }
        }

        public CommandService CloseCommand => new CommandService(Close);

        public virtual void Close(object o = null)
        {
            View.Close();
            AuthWindow auth = new AuthWindow();
        }

    }
}