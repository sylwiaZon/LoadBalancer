using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Sebalance
{
    public class DataBaseElement : ConfigurationElement
    {
        private DataBase dataBase;
        public DataBaseElement() { }
        public DataBaseElement()
        {

        }
        public object Name { get; internal set; }
    }
}
