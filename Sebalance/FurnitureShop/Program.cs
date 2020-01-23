using Sebalance;
using System;
using System.Collections.Generic;

namespace FurnitureShop
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");


            
            IStrategy s = new RandomStrategy();
            IStrategy s2 = new RoundRobin();
            var sessionFactory1 = new NHibernateSessionFactory("", "");
            var sessionFactory2 = new NHibernateSessionFactory("", "");
            LoadBalancer.SetDatabases(new List<DataBase>
            {
                new DataBase(sessionFactory1.SessionFactory),
                new DataBase(sessionFactory2.SessionFactory)
            });
            LoadBalancer.SetStrategy(s);

            var db = LoadBalancer.ChooseDatabase();
            var session = db.GetSession();
           // session.
           //for (int i = 0; i < 15; i++)
           // {
           //     Console.WriteLine(lb.ChooseDatabase());
           // }
           // lb.SetStrategy(s2);
           // Console.WriteLine("randomix");
           // for (int i = 0; i < 15; i++)
           // {
           //     Console.WriteLine(lb.ChooseDatabase());
           // }
            
        }
    }
}
