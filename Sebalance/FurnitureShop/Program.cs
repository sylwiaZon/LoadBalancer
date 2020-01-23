using Sebalance;
using System;

namespace FurnitureShop
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");


            
            IStrategy s = new RandomStrategy();
            IStrategy s2 = new RoundRobin();
            LoadBalancer lb = new LoadBalancer();
           for (int i = 0; i < 15; i++)
            {
                Console.WriteLine(lb.ChooseDatabase());
            }
            lb.SetStrategy(s2);
            Console.WriteLine("randomix");
            for (int i = 0; i < 15; i++)
            {
                Console.WriteLine(lb.ChooseDatabase());
            }
            
        }
    }
}
