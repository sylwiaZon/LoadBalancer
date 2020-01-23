using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Sebalance
{
    public class OpenSessionInViewSection: ConfigurationSection
    {
        [ConfigurationProperty("sessionFactories", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(DataBasesCollection),
            AddItemName = "sessionFactory",
            ClearItemsName = "clearFactories")]
        public DataBasesCollection DataBaseCollection
        {
            get
            {
                DataBasesCollection dataBasesCollection =
                    (DataBasesCollection)base["dataBasesFactories"];
                return dataBasesCollection;
            }
        }
    }
}
