using System;
using System.Collections.Generic;
using System.Text;

namespace Sebalance
{
    class DataBase
    {
        private object SessionFacotry { get; set; }
        private int Port { get; set; }
        private string Username { get; set; }
        private string Password { get; set; }
        private bool Available { get; set; }
        private bool SwitchedOff{ get; set; }
        private int HeartBitCounter { get; set; }
        private int LastCommand { get; set; }
        
        DataBase(object sessionFactory, int port, string username, string password)
        {
            SessionFacotry = sessionFactory;
            Port = port;
            Username = username;
            Password = password;
            Available = true;
            SwitchedOff = false;
            HeartBitCounter = 0;
            LastCommand = -1;
        }

        public void SetAvailability(bool av)
        {
            Available = av;
        }

        public object GetSessionFactory()
        {
            return SessionFacotry;
        }

        public int GetPortNumber()
        {
            return Port;
        }

        public string GetUsername()
        {
            return Username;
        }

        public string GetPassword()
        {
            return Password;
        }

        public bool IsAvailable()
        {
            return Available;
        }

        public  void SwitchOff()
        {
            SwitchedOff = true;
        }

        public bool IsSwitchedOff()
        {
            return SwitchedOff;
        }

        public void AddHeartBit()
        {
            HeartBitCounter++;
        }

        public int getLastHeartBit()
        {
            return HeartBitCounter;
        }

        public void SetCommand(int command)
        {
            LastCommand = command;
        }

        public int GetLastCommand()
        {
            return LastCommand;
        }
    }
}
