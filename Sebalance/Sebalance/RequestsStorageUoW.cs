using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sebalance
{
    class RequestsStorageUoW
    {
        public Dictionary<int, string> Storage { get; internal set; }

        public RequestsStorageUoW(Dictionary<int, string> Storage)
        {
            this.Storage = Storage;
        }

        public RequestsStorageUoW()
        {
            this.Storage = new Dictionary<int, string>();
        }
        
        public void Add(int index, string request)
        {
            Storage.Add(index, request);
        }

        public void Reset()
        {
            Storage.Clear();
        }

        public ArrayList GetRequestsFrom(int from)
        {
            ArrayList requests = new ArrayList();
            for(int i = from+1; i< Storage.Keys.Last(); i++)
            {
                string value = "";
                Storage.TryGetValue(i, out value);
                requests.Add(value);
            }
            return requests;
        }
    }
}
