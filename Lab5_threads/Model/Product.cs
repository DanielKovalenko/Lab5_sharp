using System;

namespace Lab5_threads.Model
{
    public class Product
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal BuyingPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public bool IsAvailable => Quantity > 0;

        public Product(string name, int quantity, decimal buyingPrice, decimal sellingPrice)
        {
            Name = name;
            Quantity = quantity;
            BuyingPrice = buyingPrice;
            SellingPrice = sellingPrice;
        }
    }
}
