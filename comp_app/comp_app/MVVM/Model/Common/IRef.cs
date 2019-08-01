using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace comp_app.MVVM.Model.Common
{
    public interface IRef
    {
        string Id { get; set; }
        string Name { get; set; }
    }
}
