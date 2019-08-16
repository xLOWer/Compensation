using comp_app.AppSettings;
using comp_app.MVVM.View;
using comp_app.MVVM.ViewModel.Common;
using comp_app.Services;
using Devart.Data.Oracle;
using System;
using System.ComponentModel;
using System.Linq;

namespace comp_app.MVVM.ViewModel
{
    class AuthViewModel : ViewModelBase<AuthWindow>, INotifyPropertyChanged
    {
        public AuthViewModel(ref AuthWindow _view)
        {
            LoadConfig();
            View = _view;
            Login();
        }

        public CommandService LoginCommand => new CommandService(Login);
        public virtual void Login(object o = null)
        {
            string result = null;
            try
            {
                result = TestConnection();

            }
            catch (Exception ex) { Utilites.Error(ex); }

            if (!string.IsNullOrEmpty(result)) Configure();
        }

        private string TestConnection()
        {
            try
            {
                SaveConfig();
                AppConfigHandler.ConfigureOracle();
                return DbService.SelectSingleValue("select * from dual");
            }
            catch (Exception ex)
            {
                Utilites.Error(ex);
                return "";
            }
        }

        private void Configure()
        {                 
            try
            {
                MainWindow mainWindow = new MainWindow();
                View.Activate();
                View.Close();
                mainWindow.Activate();
                mainWindow.Show();
            }
            catch (Exception ex) { Utilites.Error(ex); }
        }

        private void LoadConfig()
        {
            AppConfigHandler.Load();

            UserName = AppConfig.DbUserName;
            Password = AppConfig.DbUserPassword;
            DbHost = AppConfig.DbHost;
            DbPort = AppConfig.DbPort;
            DbSid = AppConfig.DbSID;
        }

        private void SaveConfig()
        {
            AppConfig.DbUserName = UserName;
            AppConfig.DbUserPassword = Password;
            AppConfig.DbHost = DbHost;
            AppConfig.DbPort = DbPort;
            AppConfig.DbSID = DbSid;

            AppConfigHandler.Save();
        }




        private string _UserName;
        public string UserName
        {
            get => _UserName;
            set { _UserName = value; NotifyPropertyChanged("UserName"); }
        }

        private string _Password;
        public string Password
        {
            get => _Password;
            set { _Password = value; NotifyPropertyChanged("Password"); }
        }

        private string _DbHost;
        public string DbHost
        {
            get => _DbHost;
            set { _DbHost = value; NotifyPropertyChanged("DbHost"); }
        }

        private string _DbPort;
        public string DbPort
        {
            get => _DbPort;
            set { _DbPort = value; NotifyPropertyChanged("DbPort"); }
        }

        private string _DbSid;
        public string DbSid
        {
            get => _DbSid;
            set { _DbSid = value; NotifyPropertyChanged("DbSid"); }
        }


    }
}
