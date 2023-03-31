using System;
using System.Collections.Generic;
using UnityEngine.Purchasing;

namespace Purchasing
{
    public static class UnityPurchasingUtility
    {
        public static ConfigurationBuilder Create(IEnumerable<ProductData> products)
        {
            var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
            foreach (var product in products)
            {
                var ids = new IDs();
                foreach (var store in product.Stores)
                {
                    ids.Add(ConvertStore(store.StoreHash), store.ProductHash);
                }

                builder.AddProduct(product.ProductHash, ConvertType(product.ProductType), ids);
            }

            return builder;
        }

        private static string ConvertStore(string value)
        {
            return value switch
            {
                StoreHash.Apple => AppleAppStore.Name,
                StoreHash.Play => GooglePlay.Name,
                _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
            };
        }

        private static UnityEngine.Purchasing.ProductType ConvertType(string value)
        {
            return value switch
            {
                ProductType.Consumable => UnityEngine.Purchasing.ProductType.Consumable,
                ProductType.NonConsumable => UnityEngine.Purchasing.ProductType.NonConsumable,
                ProductType.Subscription => UnityEngine.Purchasing.ProductType.Subscription,
                _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
            };
        }
    }
}