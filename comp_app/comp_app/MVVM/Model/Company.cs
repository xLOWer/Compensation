using comp_app.MVVM.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace comp_app.MVVM.Model
{
    public class Company : IRef
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
