using System;
namespace Sebalance
{
    public class LoadBalancer
    {
        Strategy strategy;
        int currentDataBase;
        int max;
        public LoadBalancer()
        {
            this.strategy = new RoundRobin();
            this.currentDataBase = 0;
            this.max = 9;
        }
        public LoadBalancer(Strategy s)
        {
            this.strategy = s;
        }
        public void setMaximum(int m)
        {
            this.max = m;
        }
        public void setCurrent(int k)
        {
            this.currentDataBase = k;
        }
        public void setStrategy(Strategy s)
        {
            this.strategy = s;
        }
        public int chooseDatabase()
        {
            this.currentDataBase= this.strategy.getNext(this.currentDataBase, this.max);
            return this.currentDataBase;
        }
    }
}
