using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace comp_app.MVVM.Model.Common
{
    public class OracleTableAttribute : Attribute
    {
        public OracleTableAttribute(string _SchemeName = null, string _TableName = null)
        {
            this.SchemeName = _SchemeName ?? comp_app.AppSettings.AppConfig.Schema;
            this.TableName = _TableName;
        }
        public string SchemeName { get; set; }
        public string TableName { get; set; }
    }
}
