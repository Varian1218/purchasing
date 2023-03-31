using System;
using UnityEngine.Purchasing;

namespace Purchasing
{
    public class Purchasing : IPurchasing
    {
        private IStoreController _controller;

        public IStoreController Controller
        {
            set => _controller = value;
        }

        public Action<string, bool> Purchased { get; private set; }

        event Action<string, bool> IPurchasing.Purchased
        {
            add => Purchased += value;
            remove => Purchased -= value;
        }

        public void Purchase(string productId)
        {
            _controller.InitiatePurchase(productId);
        }
    }
}