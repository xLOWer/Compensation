using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Markup;

namespace comp_app
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            CultureInfo culture = CultureInfo.CreateSpecificCulture("ru-RU");

            Thread.CurrentThread.CurrentUICulture = culture;
            Thread.CurrentThread.CurrentCulture = culture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = culture;

            Thread.CurrentThread.CurrentCulture.NumberFormat.NumberGroupSeparator = " ";
            
            //Thread.CurrentThread.CurrentCulture.DateTimeFormat.LongDatePattern= "dd MMMM yyyy";
            //Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = "dd MMMM yyyy";
            //Thread.CurrentThread.CurrentCulture.DateTimeFormat.YearMonthPattern = "MMMM yyyy";
            //Thread.CurrentThread.CurrentCulture.DateTimeFormat.MonthDayPattern = "dd MMMM";
        }
    }
}
