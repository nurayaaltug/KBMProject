using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace KisiselBakimProje.Data
{
    public class SQLConfigs
    {
        public string Value { get; }
        public SQLConfigs(string value) => Value = value;

        internal void Open()
        {
            throw new NotImplementedException();
        }
    }
}
