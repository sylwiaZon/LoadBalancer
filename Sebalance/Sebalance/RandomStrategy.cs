using System;
namespace Sebalance
{
    public class RandomStrategy :Strategy
    {

        public int getNext(int current, int max)
        {
            Random random = new Random();
            return random.Next(0, max);
        }
    }
}
