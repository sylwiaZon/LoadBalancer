using Sebalance;
using System;

namespace FurnitureShop
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");


            
            Strategy s = new RandomStrategy();
            Strategy s2 = new RoundRobin();
            LoadBalancer lb = new LoadBalancer();
           for (int i = 0; i < 15; i++)
            {
                Console.WriteLine(lb.chooseDatabase());
            }
            lb.setStrategy(s2);
            Console.WriteLine("randomix");
            for (int i = 0; i < 15; i++)
            {
                Console.WriteLine(lb.chooseDatabase());
            }
            
        }
    }
}
