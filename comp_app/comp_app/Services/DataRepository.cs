using comp_app.MVVM.Model;
using comp_app.MVVM.Model.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace comp_app.Services
{
    public static class DataRepository
    {

        public static List<Document> Documents { get; set; } = new List<Document>();
        public static List<Company> Companies { get; set; } = new List<Company>();
        public static List<Status> Statuses { get; set; } = new List<Status>();
        public static List<Provider> Providers { get; set; } = new List<Provider>();
        public static List<PaymentMethod> PaymentMethods { get; set; } = new List<PaymentMethod>();
        public static List<Item> Items { get; set; } = new List<Item>();
        public static List<Invoice> Invoices { get; set; } = new List<Invoice>();


        public static void LoadAll()
        {
            //DbService.Connection.Check();
            //TestDbLoadingSpeed("GetGenericList", ()=> { Set(DbService.GetGenericList<Document>()); }, 100);
            //DbService.Connection.Check();
            //TestDbLoadingSpeed("GetEntityList", () => { Set(DbService.GetEntityList<Document>()); }, 100);

            Set(DbService.SelectEntity<Document>());
            Set(DbService.SelectEntity<Status>());
            //Set(DbService.SelectEntity<Company>());
            //Set(DbService.SelectEntity<Provider>());
            //Set(DbService.SelectEntity<PaymentMethod>());
            //Set(DbService.SelectEntity<Item>());
            //Set(DbService.SelectEntity<Invoice>());
        }

        public static void Set<T>(List<T> value)
        {
            if (value == null) return;
            var prop = GetProp<T>();
            prop.SetValue(prop, value, null);
        }

        public static List<T> Get<T>()
        {
            var prop = GetProp<T>();
            return (List<T>)prop.GetValue(prop, null);
        }


        private static PropertyInfo GetProp<T>()
        {
            PropertyInfo[] fields = typeof(DataRepository).GetProperties();
            PropertyInfo field = fields.FirstOrDefault(x => x.PropertyType.GetGenericArguments().Contains(typeof(T)));
            return field;
        }
        
        public static void Refresh<T>()
        {
            Set(DbService.SelectEntity<T>());
            var prop = GetProp<T>();
        }

        public static void Remove<T>(T item) where T : IRef
        {
            if (item == null) return;
            if (item.ID == null) return;
            DbService.DeleteEntity<T>(item.ID.ToString());
        }

        public static void Add<T>(T item) where T : IRef
        {
            if (item == null) return;
            DbService.InsertNewEntity<T>(item);
            Refresh<T>();
        }

        public static void Update<T>(T item) where T : IRef
        {
            if (item == null) return;
            DbService.UpdateEntity<T>(item);
        }


    }
}
