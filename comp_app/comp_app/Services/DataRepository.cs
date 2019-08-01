using comp_app.MVVM.Model;
using comp_app.MVVM.Model.Common;
using System;
using System.Reflection;
using System.Collections.Generic;
using comp_app.Services.Common;

namespace comp_app.Services
{
    public static class DataRepository
    {
        public static GenericRepository<Document> Documents { get; set; }
        public static GenericRepository<Company> Companies { get; set; }
        public static GenericRepository<Status> Statuses { get; set; }
        public static GenericRepository<Provider> Providers { get; set; }
        public static GenericRepository<PaymentMethod> PaymentMethods { get; set; }
        public static GenericRepository<Item> Items { get; set; }
        public static GenericRepository<Invoice> Invoices { get; set; }
    }
}
