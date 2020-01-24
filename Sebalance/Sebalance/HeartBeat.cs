using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Sebalance
{
    public class HeartBeat

    {
        static List<DataBase> Unreachable;
        static List<DataBase> DataBases;
        public static HeartBeat Instance = new HeartBeat();

        public static HeartBeat GetInstance()
        {
            return Instance;
        }

        private HeartBeat()
        {
            DataBases = new List<DataBase>();
            Unreachable = new List<DataBase>();
        }
        public void ThreadHeartBeat()
        {
            while (true)
            {
                Thread.Sleep(5000);
                SendHeartBeat();
            }
        }
        public void SendHeartBeat()
        {
            Unreachable = new List<DataBase>();
            foreach(DataBase db in DataBases)
            {
                try
                {
                    _ = db.GetSession().CreateSQLQuery("SELECT null").UniqueResult();
                }
                catch (Exception)
                {
                   
                    Unreachable.Add(db);
                }
            }

            //tutaj obsluga bledow i dodanie do listy Unreachable
            //wolanie for each jesli trzeba
            DataBases = DataBases.Where(db =>!db.IsSwitchedOff()).ToList();
            Notify();
        }
        public void Subscribe(DataBase observer)
        {
            DataBases.Add(observer);
        }


        public void Notify()
        {
            foreach (DataBase db in DataBases)
            {
                    db.Update(Unreachable);

            }
        }

    
    }
}
