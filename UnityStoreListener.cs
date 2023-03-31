using System;
using UnityEngine.Purchasing;

namespace Purchasing
{
    public class UnityStoreListener : IStoreListener
    {
        private Action<UnityStoreListener, bool> _initialized;
        private Action<string> _log;
        private Action<string, bool> _purchased;

        public Action<UnityStoreListener, bool> Initialized
        {
            set => _initialized = value;
        }

        public Action<string> Log
        {
            set => _log = value;
        }

        public Action<string, bool> Purchased
        {
            set => _purchased = value;
        }

        public IStoreController Controller { get; private set; }

        public void OnInitializeFailed(InitializationFailureReason error)
        {
            _log($"reason: {error}");
            _initialized(this, false);
        }

        public void OnInitializeFailed(InitializationFailureReason error, string message)
        {
            _log($"message: {message}, reason: {error}");
            _initialized(this, false);
        }

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
        {
            _purchased?.Invoke(purchaseEvent.purchasedProduct.definition.id, true);
            return PurchaseProcessingResult.Complete;
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
        {
            _log($"reason: {failureReason}");
            _purchased?.Invoke(product.definition.id, false);
        }

        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            Controller = controller;
            _initialized(this, true);
        }
    }
}