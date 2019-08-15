using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace comp_app.MVVM.Model.Common
{
    public class OracleTableAttribute : Attribute
    {
        public OracleTableAttribute(string _SchemeName = "abt", string _TableName = "dual")
        {
            this.SchemeName = _SchemeName;
            this.TableName = _TableName;
        }
        public string SchemeName { get; set; }
        public string TableName { get; set; }
    }
}
