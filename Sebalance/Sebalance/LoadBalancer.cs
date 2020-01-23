using System;
using System.Collections.Generic;

namespace Sebalance
{
    public class LoadBalancer
    {
        IStrategy Strategy;
        int CurrentDataBase;
        static List<DataBase> AllDataBases;
        int Max;
        
        public LoadBalancer(IStrategy s)
        {
            Strategy = s;
        }
        public void SetMaximum(int m)
        {
            Max = m;
        }
        public void SetCurrent(int k)
        {
            CurrentDataBase = k;
        }
        public void SetStrategy(IStrategy s)
        {
            Strategy = s;
        }
        public DataBase ChooseDatabase()
        {
            CurrentDataBase = Strategy.GetNext(CurrentDataBase, Max);
            return AllDataBases[CurrentDataBase];
        }
    }
}
