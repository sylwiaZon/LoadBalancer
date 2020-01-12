using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Sebalance
{
    public class DataBaseElement : ConfigurationElement
    {
        public object Name { get; internal set; }
    }
}
