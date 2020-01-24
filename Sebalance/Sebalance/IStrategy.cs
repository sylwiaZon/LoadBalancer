using System;
namespace Sebalance
{
    public interface IStrategy
    {
        int GetNext(int current, int max); 
    }
}
