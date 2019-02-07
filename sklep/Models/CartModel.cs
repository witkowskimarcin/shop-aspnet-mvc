using sklep.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sklep.Models
{

    public class CartModel
    {

        public SortedDictionary<ProductModel, int> products { get; set; }
        private double sum { get; set; }

        public CartModel()
        {
            products = new SortedDictionary<ProductModel, int>();
            sum = 0.0;
        }

        public void addProduct(ProductModel product)
        {

            int value = 0;
            products.TryGetValue(product, out value);
            if (value>0)
            {
                value += 1;
                products[product] = value;
            }
            else
            {
                products.Add(product, 1);
            }
        }

        public void removeProduct(ProductModel product)
        {
            int value = 0;
            if (products.ContainsKey(product))
            {
                value = products[product];
                if (value <= 1)
                {
                    products.Remove(product);
                }
                else
                {
                    value -= 1;
                    products[product] = value;
                }
            }
        }

        public double getSum()
        {

            sum = 0.0;

            foreach(KeyValuePair<ProductModel,int> item in products)
            {
                sum += item.Key.Price * item.Value;
            }

            return sum;
        }

        public int getQuantity()
        {
            return products.Count;
        }

        public void removeCart()
        {
            sum = 0.0;
            products.Clear();
        }
    }
}