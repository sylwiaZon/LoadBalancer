namespace Sebalance
{
    public interface ISubject1
    {
        void SendHeartBeat();
        void Subscribe(DataBase observer);
        void Unsubscribe(DataBase observer);
    }
}