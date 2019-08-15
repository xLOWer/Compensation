using comp_app.MVVM.Model;
using comp_app.MVVM.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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
            Set(DbService.GetEntityList<Document>());
            //Set(DbService.GetEntityList<Company>());
            //Set(DbService.GetEntityList<Status>());
            //Set(DbService.GetEntityList<Provider>());
            //Set(DbService.GetEntityList<PaymentMethod>());
            //Set(DbService.GetEntityList<Item>());
            //Set(DbService.GetEntityList<Invoice>());
        }

        public static void LazyLoad<T>() where T : IRef
        {
            List<T> list = Get<T>();
            var tp = typeof(T);
            var prs = tp.GetProperties(BindingFlags.NonPublic | BindingFlags.Instance);
            var prop = GetProp<T>();
            foreach (var v in list)
            {
                foreach (var pr in prs)
                {
                    var i = Get(pr.PropertyType);

                    //var k = n.Where(x => x.ID == v.ID);
                    //prop.SetValue(v, .Where(x => x.ID == v.ID), null);
                }
            }
            Set(list);
        }



        public static T FirstOrDefault<T>(Func<T, bool> predicate)
        {
            var prop = GetProp<T>();
            return ((List<T>)prop.GetValue(prop, null)).FirstOrDefault(predicate);
        }

        public static int Count<T>()
        {
            var prop = GetProp<T>();
            return ((List<T>)prop.GetValue(prop, null)).Count;
        }

        public static bool Any<T>(Func<T, bool> predicate)
        {
            var prop = GetProp<T>();
            return ((List<T>)prop.GetValue(prop, null)).Any(predicate);
        }

        public static List<T> Where<T>(Func<T, bool> predicate)
        {
            var prop = GetProp<T>();
            return (List<T>)((List<T>)prop.GetValue(prop, null)).Where(predicate);
        }

        public static int Max<T>(Func<T, int> selector)
        {
            var prop = GetProp<T>();
            return ((List<T>)prop.GetValue(prop, null)).Max(selector);
        }
        public static int? Max<T>(Func<T, int?> selector)
        {
            var prop = GetProp<T>();
            return ((List<T>)prop.GetValue(prop, null)).Max(selector);
        }
        public static decimal Max<T>(Func<T, decimal> selector)
        {
            var prop = GetProp<T>();
            return ((List<T>)prop.GetValue(prop, null)).Max(selector);
        }
        public static decimal? Max<T>(Func<T, decimal?> selector)
        {
            var prop = GetProp<T>();
            return ((List<T>)prop.GetValue(prop, null)).Max(selector);
        }
        public static double Max<T>(Func<T, double> selector)
        {
            var prop = GetProp<T>();
            return ((List<T>)prop.GetValue(prop, null)).Max(selector);
        }
        public static double? Max<T>(Func<T, double?> selector)
        {
            var prop = GetProp<T>();
            return ((List<T>)prop.GetValue(prop, null)).Max(selector);
        }
        public static long Max<T>(Func<T, long> selector)
        {
            var prop = GetProp<T>();
            return ((List<T>)prop.GetValue(prop, null)).Max(selector);
        }
        public static long? Max<T>(Func<T, long?> selector)
        {
            var prop = GetProp<T>();
            return ((List<T>)prop.GetValue(prop, null)).Max(selector);
        }

        public static int RemoveAll<T>(Predicate<T> match)
        {
            var prop = GetProp<T>();
            return ((List<T>)prop.GetValue(prop, null)).RemoveAll(match);
        }

        public static bool Remove<T>(T item)
        {
            if (item == null) return false;
            var prop = GetProp<T>();
            return ((List<T>)prop.GetValue(prop, null)).Remove(item);
        }

        public static void AddRange<T>(List<T> items)
        {
            if (items == null) return;

            var prop = GetProp<T>();
            ((List<T>)prop.GetValue(prop, null)).AddRange(items);
        }

        public static void Add<T>(T item)
        {
            if (item == null) return;
            var prop = GetProp<T>();
            ((List<T>)prop.GetValue(prop, null)).Add(item);
        }

        public static List<T> Get<T>()
        {
            var prop = GetProp<T>();
            return (List<T>)prop.GetValue(prop, null);
        }

        public static List<T> Get<T>(Type t) where T : IRef
        {
            var prop = GetProp(t);
            return (List<T>)prop.GetValue(prop, null);
        }

        public static void Set<T>(List<T> value)
        {
            if (value == null) return;
            var prop = GetProp<T>();
            prop.SetValue(prop, value, null);            
        }
        
        private static PropertyInfo GetProp<T>()
        {
            PropertyInfo[] fields = typeof(DataRepository).GetProperties();
            PropertyInfo field = fields.FirstOrDefault(x => x.PropertyType.GetGenericArguments().Contains(typeof(T)));
            return field;
        }

        private static PropertyInfo GetProp(Type t)
        {
            PropertyInfo[] fields = typeof(DataRepository).GetProperties();
            PropertyInfo field = fields.FirstOrDefault(x => x.PropertyType.GetGenericArguments().Contains(t));
            return field;
        }

        public static void Refresh<T>()
        {
            Set(DbService.GetEntityList<T>());
            var prop = GetProp<T>();
        }
    }
}
