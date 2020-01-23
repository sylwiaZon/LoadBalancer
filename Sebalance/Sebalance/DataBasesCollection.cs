using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Sebalance
{
    [ConfigurationCollection(typeof(DataBaseElement))]
    public sealed class DataBasesCollection : ConfigurationElementCollection
    {
        public DataBasesCollection()
        {
            DataBaseElement dataBaseElement =
               (DataBaseElement)CreateNewElement();
            Add(dataBaseElement);
        }
        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.AddRemoveClearMap;
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new DataBaseElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((DataBaseElement)element).Name;
        }

        public DataBaseElement this[int index]
        {
            get
            {
                return (DataBaseElement)BaseGet(index);
            }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }

                BaseAdd(index, value);
            }
        }

        new public DataBaseElement this[string name]
        {
            get
            {
                return (DataBaseElement)BaseGet(name);
            }
        }

        public int IndexOf(DataBaseElement sessionFactory)
        {
            return BaseIndexOf(sessionFactory);
        }

        public void Add(DataBaseElement sessionFactory)
        {
            BaseAdd(sessionFactory);
        }

        protected override void BaseAdd(ConfigurationElement element)
        {
            BaseAdd(element, false);
        }

        public void Remove(DataBaseElement sessionFactory)
        {
            if (BaseIndexOf(sessionFactory) >= 0)
            {
                BaseRemove(sessionFactory.Name);
            }
        }

        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }

        public void Remove(string name)
        {
            BaseRemove(name);
        }

        public void Clear()
        {
            BaseClear();
        }
    }
}

