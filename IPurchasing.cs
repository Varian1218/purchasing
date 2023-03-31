using System;

namespace Purchasing
{
    public interface IPurchasing
    {
        event Action<string, bool> Purchased; 
        void Purchase(string productId);
    }
}