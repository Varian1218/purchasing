using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Purchasing
{
    [CreateAssetMenu(menuName = "Purchasing/Product Database", fileName = "Product Database", order = 1)]
    public class ProductDatabase : ScriptableObject, IEnumerable<ProductData>
    {
        private enum ProductType
        {
            Consumable,
            NonConsumable,
            Subscription
        }

        private enum Store
        {
            Apple,
            Play
        }

        [Serializable]
        private struct ProductData
        {
            public string productHash;
            public ProductType productType;
            public StoreProductData[] stores;
        }

        [Serializable]
        private struct StoreProductData
        {
            public string productHash;
            public Store storeHash;
        }

        [SerializeField] private ProductData[] data;

        private static string ConvertStore(Store store)
        {
            return store switch
            {
                Store.Apple => StoreHash.Apple,
                Store.Play => StoreHash.Play,
                _ => throw new ArgumentOutOfRangeException(nameof(store), store, null)
            };
        }

        private static string ConvertType(ProductType productType)
        {
            return productType switch
            {
                ProductType.Consumable => global::Purchasing.ProductType.Consumable,
                ProductType.NonConsumable => global::Purchasing.ProductType.NonConsumable,
                ProductType.Subscription => global::Purchasing.ProductType.Subscription,
                _ => throw new ArgumentOutOfRangeException(nameof(productType), productType, null)
            };
        }

        public IEnumerator<global::Purchasing.ProductData> GetEnumerator()
        {
            return data.Select(it => new global::Purchasing.ProductData
            {
                ProductHash = it.productHash,
                ProductType = ConvertType(it.productType),
                Stores = it.stores.Select(store => new global::Purchasing.StoreProductData
                {
                    ProductHash = store.productHash,
                    StoreHash = ConvertStore(store.storeHash)
                }).ToArray()
            }).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}