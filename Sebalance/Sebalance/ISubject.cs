using System;
namespace Sebalance
{
     interface ISubject
    {
        void Subscribe(DataBase observer);
        void Unsubscribe(DataBase observer);
        void SendHeartBeat();
    }
}
