using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Sebalance
{
    public class LoadBalancer
    {
        private  IStrategy Strategy;
        private  int CurrentDataBase = -1;
        private  List<DataBase> AllDataBases;
        static int Max;
        private static RequestsStorageUoW Requests = new RequestsStorageUoW();
        private static int RequestsCounter = 0;

        private static HeartBeat HeartBeat = HeartBeat.GetInstance();
        public LoadBalancer()
        {
           
        }
        
        public  void SetStrategy(IStrategy s)
        {
            Strategy = s;
        }

        public  void SetDatabases(List<DataBase> dbs) {
            AllDataBases = dbs;
            Max = dbs.Count;
            Thread heartBeat = new Thread(new ThreadStart(HeartBeat.ThreadHeartBeat));
            heartBeat.Start();
        }
        public bool NoMoreDataBases()
        {
            foreach(DataBase db in AllDataBases)
            {
                if (db.IsSwitchedOn())
                {
                    return true;
                }
            }
            return false;
        }

         private DataBase ChooseDatabase()
        {
            
            CurrentDataBase = Strategy.GetNext(CurrentDataBase, Max);
            return AllDataBases[CurrentDataBase];
        }


        public IQueryable<T> Query<T>()
        {
            try
            {
                while (NoMoreDataBases())
                {
                    DataBase db = ChooseDatabase();
                    if (db.IsAvailable())
                    {
                        Console.WriteLine(String.Format("Select from Data Base {0} ", db.Name));
                        return db.GetSession().Query<T>();
                    }
                    else
                    {
                        db.SetAvailability(false);
                    }
                }
                throw new Exception();
            }
            catch(Exception)
            {
                Console.WriteLine("All databases are dead. Try to restart");
            }
            return null;
        }

        public  void Save<T>(T obj)
        {
            RequestsCounter++;
            foreach (var db in AllDataBases)
            {
                
                    if (db.IsAvailable())
                    {
                    db.Save(obj);
                    }
                    else if (!db.IsAvailable() && db.IsSwitchedOn())
                    {
                    db.AddToUoW(RequestsCounter, "INSERT", obj);
                    }
                
               

            }

        }

        public  void Delete<T>(T obj)
        {
            RequestsCounter++;
            foreach (var db in AllDataBases)
            {
                 if (db.IsAvailable())
                {
                    db.Delete(obj);
                }
                else if (!db.IsAvailable() && db.IsSwitchedOn())
                {
                    db.AddToUoW(RequestsCounter, "DELETE", obj);
                }

            }

 

        }


        public  void Update<T>(T obj)
        {
            RequestsCounter++;
            foreach (var db in AllDataBases)
            {
                
                if (db.IsAvailable())
                {
                    db.Update(obj);
                }
                else if (!db.IsAvailable() && db.IsSwitchedOn())
                {
                    db.AddToUoW(RequestsCounter, "UPDATE", obj);
                }

            }

        }

    }
}
