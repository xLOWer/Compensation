using comp_app.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace comp_app.Services
{
    public static class DataRepository
    {
        internal static List<Document> Documents { get; set; }
        internal static List<Company> Companies { get; set; }
        internal static List<Status> Statuses { get; set; }
        internal static List<Provider> Providers { get; set; }
        internal static List<PaymentMethod> PaymentMethods { get; set; }
        internal static List<Item> Items { get; set; }
        internal static List<Invoice> Invoices { get; set; }
    }
}
