using NHibernate.Util;
using Sebalance;
using System;
using System.Collections;
using System.Collections.Generic;

namespace FurnitureShop
{
    class Program
    {

        static void Main(string[] args)
        {
            NHibernateExample NHibernate = new NHibernateExample();

        }
    }


    class NHibernateExample
    {
        private int Counter = 0;
        private int Counter2 = 0;

        public ArrayList Tables;

        private LoadBalancer LoadBalancer = new LoadBalancer();

        public NHibernateExample()
        {
            Configuration();
            NextAction();
        }

        public void Configuration()
        {
            IStrategy s = new RoundRobin();
            var sessionFactory1 = new NHibernateSessionFactory("Server=104.211.12.200; Port=3306; Database = furnitureShop; Uid = sebalance; Password = sebalance; ", new List<Type> { typeof(Table) });
            var sessionFactory2 = new NHibernateSessionFactory("Server=104.211.12.200; Port=3307; Database = furnitureShop; Uid = sebalance; Password = sebalance; ", new List<Type> { typeof(Table) });
            var sessionFactory3 = new NHibernateSessionFactory("Server=104.211.12.200; Port=3308; Database = furnitureShop; Uid = sebalance; Password = sebalance; ", new List<Type> { typeof(Table) });
            LoadBalancer.SetDatabases(new List<DataBase>
            {
                new DataBase(sessionFactory1.SessionFactory),
                new DataBase(sessionFactory2.SessionFactory),
                new DataBase(sessionFactory3.SessionFactory)
            });

            LoadBalancer.SetStrategy(s);
            PrepareTableData();
            Console.WriteLine();
        }

        public void PrepareTableData()
        {
            Table tab1 = new Table()
            {
                Color = "Różowy",
                Material = "skóra"
            };

            Table tab2 = new Table()
            {
                Color = "Niebieski",
                Material = "aksamit"
            };

            Table tab3 = new Table()
            {
                Color = "Zielony",
                Material = "drewno"
            };

            Table tab4 = new Table()
            {
                Color = "Purpurowy",
                Material = "metal"
            };

            Table tab5 = new Table()
            {
                Color = "Magenta",
                Material = "sztruks"
            };

            Table tab6 = new Table()
            {
                Color = "Łososiowy",
                Material = "łuska"
            };

            Table tab7 = new Table()
            {
                Color = "Czerwony",
                Material = "wiśnia"
            };

            Table tab8 = new Table()
            {
                Color = "Czarny",
                Material = "lateks"
            };

            Table tab9 = new Table()
            {
                Color = "Brązowy",
                Material = "olcha"
            };

            Table tab10 = new Table()
            {
                Color = "Kolorowy",
                Material = "dąb"
            };

            Tables = new ArrayList();
            Tables.Add(tab1);
            Tables.Add(tab2);
            Tables.Add(tab3);
            Tables.Add(tab4);
            Tables.Add(tab5);
            Tables.Add(tab6);
            Tables.Add(tab7);
            Tables.Add(tab8);
            Tables.Add(tab9);
            Tables.Add(tab10);
            LoadBalancer.Save(Tables[Counter]);
            Counter++;
            LoadBalancer.Save(Tables[Counter]);
            Counter++;
            LoadBalancer.Save(Tables[Counter]);
            Counter++;
            LoadBalancer.Save(Tables[Counter]);
            Counter++;
            LoadBalancer.Save(Tables[Counter]);
            Counter++;
        }


        public void DeleteData()
        {

            LoadBalancer.Delete(Tables[Counter2]);
            Counter2++;
            Console.WriteLine("Rezultat operacji: Dane zostały usunięte z tabeli Tables");
            Console.WriteLine();
            NextAction();
        }


        public void AddData()
        {
            LoadBalancer.Save(Tables[Counter]);
            Counter++;
            Console.WriteLine("Rezultat operacji: Dane zostały poprawnie dodane do tabeli Tables");
            Console.WriteLine();
            NextAction();
        }


        public void UpdateData(String color, String material, int id)
        {
            Table tab = (Table)Tables[id - 1];
            tab.Color = color;
            tab.Material = material;
            LoadBalancer.Save(tab);
            Console.WriteLine("Rezultat operacji: Dane zostały zmienione w tabeli Tables");
            Console.WriteLine();
            NextAction();
        }

        public void GetAllData()
        {
            LoadBalancer.Query<Table>().ForEach(t =>
            {
                Console.WriteLine($"Stół {t.TableId} MATERIAŁ: {t.Material} KOLOR: {t.Color} ");
            });
            Console.WriteLine();
            NextAction();

        }

        public void NextAction()
        {
            Console.WriteLine("Wybierz następne działanie, które chcesz wykonać: ");
            Console.WriteLine("1: Skasowanie danych z tabeli Car");
            Console.WriteLine("2: Dodanie danych do tabeli Car");
            Console.WriteLine("3: Wyświetlenie wszystkich danych z tabeli Car");
            Console.WriteLine("4: Zaktualizowanie danych z tabeli Car");
            Console.WriteLine("5: Opuścić aplikację: ");
            Console.Write("Twój wybór: ");
            int actionId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();
            switch (actionId)
            {
                case 1:
                    DeleteData();
                    break;
                case 2:
                    AddData();
                    break;
                case 3:
                    GetAllData();
                    break;
                case 4:
                    UpdateData("CZARNY", "CZARNY MATERIAŁ", 0);
                    break;
                case 5:
                    System.Environment.Exit(0);
                    break;
                default:
                    break;
            }
            Console.ReadKey();
        }
    }
}