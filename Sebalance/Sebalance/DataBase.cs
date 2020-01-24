using System;
using System.Collections.Generic;
using NHibernate;

namespace Sebalance
{
    public class DataBase
    {
        private ISessionFactory SessionFactory { get; set; }
        private bool Available { get; set; }
        private bool SwitchedOff { get; set; }
        private int HeartBeatCounter { get; set; }
        private int LastCommand { get; set; }
        private static HeartBeat HeartBeat;

        public DataBase(ISessionFactory sessionFactory)
        {
            HeartBeat = HeartBeat.GetInstance();
            HeartBeat.Subscribe(this);
            SessionFactory = sessionFactory;
            Available = true;
            SwitchedOff = false;
            HeartBeatCounter = 0;
            LastCommand = -1;
        }

        public static explicit operator DataBase(List<DataBase> v)
        {
            throw new NotImplementedException();
        }

        public void SetAvailability(bool av)
        {
            Available = av;
        }

        private ISessionFactory GetSessionFactory()
        {
            return SessionFactory;
        }

        internal ISession GetSession()
        {
            return SessionFactory.OpenSession();
        }

        public bool IsAvailable()
        {
            return Available;
        }

        public void SwitchOff()
        {
            SwitchedOff = true;
        }

        public bool IsSwitchedOff()
        {
            return SwitchedOff;
        }

        public void AddHeartBit()
        {
            HeartBeatCounter++;
        }

        public int getLastHeartBit()
        {
            return HeartBeatCounter;
        }

        public void SetCommand(int command)
        {
            LastCommand = command;
        }

        public int GetLastCommand()
        {
            return LastCommand;
        }

        public void Update(List<DataBase> unreachable)
        {
           
            if (!SwitchedOff)
            {
                if (unreachable.Find(x => x == this) != null)
                {
                    Console.WriteLine(String.Format("Data Base {0} is not responding", GetSession().Connection.ConnectionString));
                    HeartBeatCounter += 1;
                    if (Available)
                    {
                        Available = false;
                    }
                    else
                    {
                        if (HeartBeatCounter == 50)
                        {
                            Console.WriteLine(String.Format("Data Base {0} has been switched off", GetSession().Connection.ConnectionString));
                          
                            SwitchedOff = true;
                          
                        }

                    }
                }
                else
                {
                    if (!Available)
                    {
                        Available = true;
                        HeartBeatCounter = 0;
                    }
                }
            }
          
           
        }

    }
}