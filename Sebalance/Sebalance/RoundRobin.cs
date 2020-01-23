using System;
namespace Sebalance
{
    public class RoundRobin : IStrategy
    {
        public int GetNext(int current, int max)
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
