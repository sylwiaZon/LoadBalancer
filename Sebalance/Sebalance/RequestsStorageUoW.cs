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

            internal Command Clone()
            {
                Command com = new Command(this.Type, this.Obj);
                return com;
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
       

        public Dictionary<int, Command> DeepCopy()
        {
            Dictionary<int, Command> other =  new  Dictionary<int, Command>(Storage.Count, Storage.Comparer);
            foreach (KeyValuePair<int, Command> entry in Storage)
            {
                other.Add(entry.Key, (Command)entry.Value.Clone());
            }
            
            return other;
        }
         
        public void SetDownloaded(bool v)
        {
            Downloaded = v;
        }
        public void Add(int index, Command request)
        {
            Storage.Add(index, request);
        }
        
        public void Reset()
        {
            Storage.Clear();
        }

        public Dictionary<int, Command> GetRequests()
        {
            return Storage;
        }
    }
}
