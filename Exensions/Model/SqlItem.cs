using System;
using System.Collections.Generic;
using System.Text;

namespace Extensions.Model
{
    public class SqlItem
    {
        public string Sql { get; set; } = "";
        public object Params { get; set; } = "";
        public SqlItem()
        {
            
        }
    }
}
