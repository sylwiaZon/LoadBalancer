using System;
namespace Sebalance
{
    public class RandomStrategy : IStrategy
    {

        public int GetNext(int current, int max)
        {
            Random random = new Random();
            return random.Next(0, max);
        }
    }
}
