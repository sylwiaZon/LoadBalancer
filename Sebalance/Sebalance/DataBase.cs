using System;
using System.Collections.Generic;
using NHibernate;

namespace Sebalance
{
    public class DataBase
    {
        private ISessionFactory SessionFactory { get; set; }
        private bool Available { get; set; }
        private bool SwitchedOff { get; set; }
        private int HeartBeatCounter { get; set; }
        private static HeartBeat HeartBeat;
        private RequestsStorageUoW UoW;
        public String Name { get; set; }
        public DataBase(ISessionFactory sessionFactory)
        {
            HeartBeat = HeartBeat.GetInstance();
            HeartBeat.Subscribe(this);
            SessionFactory = sessionFactory;
            Available = true;
            SwitchedOff = false;
            HeartBeatCounter = 0;
            UoW = new RequestsStorageUoW();
        }

    

        public void SetAvailability(bool av)
        {
            Available = av;
        }

        private ISessionFactory GetSessionFactory()
        {
            return SessionFactory;
        }

        internal ISession GetSession()
        {
            ISession session = SessionFactory.OpenSession();
            Name = session.Connection.ConnectionString;
            return session;
        }

        public bool IsAvailable()
        {
            return Available;
        }

        public void SwitchOff()
        {
            SwitchedOff = true;
        }

        public bool IsSwitchedOff()
        {
            return SwitchedOff;
        }
        public bool IsSwitchedOn()
        {
            return !SwitchedOff;
        }

        
        public void AddToUoW(int i,String str, Object obj)
        {
            UoW.Add(i, new RequestsStorageUoW.Command(str, obj));
        }


        public void Update(List<DataBase> unreachable)
        {
           
            if (!SwitchedOff)
            {
                if (unreachable.Find(x => x == this) != null)
                {
                    Console.WriteLine(String.Format("Data Base {0} is not responding",Name));
                    HeartBeatCounter += 1;
                    if (Available)
                    {
                        Available = false;
                    }
                    else
                    {
                        if (HeartBeatCounter == 50)
                        {
                            Console.WriteLine(String.Format("Data Base {0} has been switched off", Name));
                          
                            SwitchedOff = true;
                          
                        }

                    }
                }
                else
                {
                    if (!Available)
                    {
                        Available = true;
                        HeartBeatCounter = 0;
                        SendMissedRequests(UoW.GetRequests());
                    }
                   
                }
            }
          
           
        }
        public void SendMissedRequests(Dictionary<int, RequestsStorageUoW.Command> d)
        {
            bool crashed = false;
            Dictionary<int, RequestsStorageUoW.Command> dCopy= UoW.DeepCopy();
            foreach(KeyValuePair<int, RequestsStorageUoW.Command> elem in d)
            {
                if (!crashed) {
                    switch (elem.Value.Type)
                    {
                        case "INSERT":
                            try
                            {
                                Save(elem.Value.Obj);
                                dCopy.Remove(elem.Key);
                            }
                            catch (Exception)
                            {
                                crashed = true;
                            }
                            break;
                        case "UPDATE":
                            try
                            {
                                Update(elem.Value.Obj);
                                dCopy.Remove(elem.Key);

                            }
                            catch (Exception)
                            {
                                crashed = true;
                            }
                            break;
                        case "DELETE":
                            try
                            {
                                Delete(elem.Value.Obj);
                                dCopy.Remove(elem.Key);
                            }
                            catch (Exception)
                            {
                                crashed = true;
                            }
                            break;
                        default:
                            break;

                    }
                }
                else
                {
                    break;
                }
            }
           
            UoW.Storage = dCopy;
            
        }
        public void Delete<T>(T obj)
        {
            var session = GetSession();
            var trx = session.BeginTransaction();
            session.Delete(obj);
            trx.Commit();
        }
        public void Update<T>(T obj)
        {
            var session = GetSession();
            var trx = session.BeginTransaction();
            session.Update(obj);
            trx.Commit();
        }
        public void Save<T>(T obj)
        {
            GetSession().Save(obj);
        }
    }
}