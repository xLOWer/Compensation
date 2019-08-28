using comp_app.AppSettings;
using comp_app.MVVM.Model;
using comp_app.MVVM.Model.Common;
using comp_app.MVVM.View;
using comp_app.MVVM.ViewModel;
using comp_app.Services;
using Devart.Data.Oracle;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows;

namespace comp_app
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            AppConfigHandler.Load();
            AppConfigHandler.ConfigureOracle();

            DataRepository.LoadAll();
            //var item = new Document();

            //DataRepository.Refresh<Document>();
            //var doc = new Document() { NAME="name123", COMPANY_ID=3 }.WithNewId();
            //DataRepository.Add(doc);

            //var doc = DataRepository.Get<Document>().FirstOrDefault(x=>x.ID == 2);
            //doc.STATUS_ID = 2;
            //var doc2 = doc;
            //DataRepository.Update(doc2);

            CultureInfo culture = CultureInfo.CreateSpecificCulture("ru-RU");
            Thread.CurrentThread.CurrentUICulture = culture;
            Thread.CurrentThread.CurrentCulture = culture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = culture;

            Thread.CurrentThread.CurrentCulture.NumberFormat.NumberGroupSeparator = " ";
        }

    }
}
