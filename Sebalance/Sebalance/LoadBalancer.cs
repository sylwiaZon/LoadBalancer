using System;
using System.Collections.Generic;
using System.Linq;

namespace Sebalance
{
    public class LoadBalancer
    {
        private static IStrategy Strategy;
        private static int CurrentDataBase = 0;
        private static List<DataBase> AllDataBases;
        static int Max;

        private LoadBalancer() { }
        
        public static void SetStrategy(IStrategy s)
        {
            Strategy = s;
        }

        public static void SetDatabases(List<DataBase> dbs) {
            AllDataBases = dbs;
            Max = dbs.Count;
        }

        static private DataBase ChooseDatabase()
        {
            CurrentDataBase = Strategy.GetNext(CurrentDataBase, Max);
            return AllDataBases[CurrentDataBase];
        }


        public static IQueryable<T> Query<T>()
        {
            return ChooseDatabase().GetSession().Query<T>();
        }

        public static void Save<T>(T obj)
        {
            foreach (var db in AllDataBases) {
                db.GetSession().Save(obj);
            }
        }

        public static void Delete<T>(T obj)
        {
            foreach (var db in AllDataBases)
            {
                var session = db.GetSession();
                var trx = session.BeginTransaction();
                session.Delete(obj);
                trx.Commit();
            }
        }
        

        public static void Update<T>(T obj)
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
