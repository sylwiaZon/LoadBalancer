using System;
namespace Sebalance
{
    public interface Strategy
    {
        int getNext(int current, int max);
      
    }
}
