using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sebalance
{
    public class RequestsStorageUoW
    {
        public class Command
        {
            public String Type { set; get; }
            public Object Obj { set; get; }
            public Command() { }
            public Command(String type, Object obj)
            {
                Type = type;
                Obj = obj;
            }
            
        }
        public bool Downloaded = false; 
        public Dictionary<int, Command> Storage { get; internal set; }

        public RequestsStorageUoW(Dictionary<int, Command> Storage)
        {
            this.Storage = Storage;
        }

        public RequestsStorageUoW()
        {
            this.Storage = new Dictionary<int, Command>();
        }
        
        public void Add(int index, Command request)
        {
            Storage.Add(index, request);
        }

        public void Reset()
        {
            Storage.Clear();
        }

        public ArrayList GetRequests()
        {
            ArrayList requests = new ArrayList();
            for(int i = 0; i< Storage.Keys.Last(); i++)
            {
                Command value = new Command();
                Storage.TryGetValue(i, out value);
                requests.Add(value);
            }
            return requests;
        }
    }
}
