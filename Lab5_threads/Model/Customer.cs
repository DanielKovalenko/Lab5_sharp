using System;
using System.Collections.Generic;

namespace Lab5_threads.Model
{
    public class Customer
    {
        private static int counter = 0;
        public int Id { get; }
        public List<PurchasedProduct> PurchasedProducts { get; }

        public Customer()
        {
            counter++;
            Id = counter;
            PurchasedProducts = new List<PurchasedProduct>();
        }
    }
}
