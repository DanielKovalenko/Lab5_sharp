using System;

namespace Lab5_threads.Model
{
    public class PurchasedProduct
    {
        public string ProductName { get; }
        public int Quantity { get; }
        public decimal TotalCost { get; }

        public PurchasedProduct(string productName, int quantity, decimal totalCost)
        {
            ProductName = productName;
            Quantity = quantity;
            TotalCost = totalCost;
        }
    }
}
