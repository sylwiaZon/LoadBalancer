using System;
namespace Sebalance
{
    public class LoadBalancer
    {
        IStrategy Strategy;
        int CurrentDataBase;
        int Max;
        public LoadBalancer()
        {
            Strategy = new RoundRobin();
            CurrentDataBase = 0;
            Max = 9;
        }
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
        public int ChooseDatabase()
        {
            CurrentDataBase= Strategy.GetNext(CurrentDataBase, Max);
            return CurrentDataBase;
        }
    }
}
