using System;
namespace Sebalance
{
    public class RoundRobin : Strategy
    {
        public int getNext(int current, int max)
        {
            int next = current + 1;
            if (next > max)
            {
                next = 0;
            }
            return next;
           
        }
    }
}
