using Tasks;

namespace Purchasing.Tasks
{
    public class TaskPurchasing
    {
        private readonly Awaiter<string> _awaiter = new();
        private IPurchasing _purchasing;
        private readonly AwaiterTask<string> _task;

        public TaskPurchasing()
        {
            _task = new AwaiterTask<string>(_awaiter);
        }

        public IPurchasing Purchasing
        {
            set
            {
                _purchasing = value;
                value.Purchased += OnPurchase;
            }
        }

        public void Clear()
        {
            _awaiter.Clear();
            _purchasing.Purchased -= OnPurchase;
            _purchasing = null;
        }

        private void OnPurchase(string message, bool success)
        {
            _awaiter.Complete(success ? null : message);
        }

        public ITask<string> PurchaseAsync(string productId)
        {
            _awaiter.Clear();
            _purchasing.Purchase(productId);
            return _task;
        }
    }
}