using comp_app.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    }
}
