using System;
using System.Collections.Generic;

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

        static public DataBase ChooseDatabase()
        {
            CurrentDataBase = Strategy.GetNext(CurrentDataBase, Max);
            return AllDataBases[CurrentDataBase];
        }
    }
}
