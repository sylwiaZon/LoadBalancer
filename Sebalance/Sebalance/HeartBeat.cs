using System;
using System.Collections.Generic;
using System.Linq;

namespace Sebalance
{
     class HeartBeat : ISubject

    {
        List<Object> Unreachable;
        List<DataBase> DataBases;
        public HeartBeat()
        {
            DataBases = new List<DataBase>();
            Unreachable = new List<object>();
        }

        void ISubject.SendHeartBeat()
        {
            //tutaj obsluga bledow i dodanie do listy Unreachable
            //wolanie for each jesli trzeba
            DataBases = DataBases.Where(db =>!db.IsSwitchedOff()).ToList();
            Notify();
        }
        public void Subscribe(DataBase observer)
        {
            DataBases.Add(observer);
        }

        public void Unsubscribe(DataBase observer)
        {
            DataBases.Remove(observer);
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
