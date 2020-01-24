using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Sebalance
{
    public class LoadBalancer
    {
        private  IStrategy Strategy;
        private  int CurrentDataBase = 0;
        private  List<DataBase> AllDataBases;
        static int Max;
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

         private DataBase ChooseDatabase()
        {
            CurrentDataBase = Strategy.GetNext(CurrentDataBase, Max);
            return AllDataBases[CurrentDataBase];
        }


        public  IQueryable<T> Query<T>()
        {
            return ChooseDatabase().GetSession().Query<T>();
        }

        public  void Save<T>(T obj)
        {
            foreach (var db in AllDataBases) {
                db.GetSession().Save(obj);
            }
        }

        public  void Delete<T>(T obj)
        {
            foreach (var db in AllDataBases)
            {
                var session = db.GetSession();
                var trx = session.BeginTransaction();
                session.Delete(obj);
                trx.Commit();
            }
        }
        

        public  void Update<T>(T obj)
        {
            foreach (var db in AllDataBases)
            {
                var session = db.GetSession();
                var trx = session.BeginTransaction();
                session.Update(obj);
                trx.Commit();
            }
        }



    }
}
