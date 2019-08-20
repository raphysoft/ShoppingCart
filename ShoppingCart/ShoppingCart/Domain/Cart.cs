using ShoppingCart.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace ShoppingCart.Domain
{
    public class Cart : IHaveAnID
    {
        public Cart()
        {
            Basket = new Dictionary<string, decimal>();
        }

        public Guid ID { get; set; }

        Dictionary<string, decimal> Basket { get; set; }

        public void ClearBasket()
        {
            Basket.Clear();
        }

        public Dictionary<string, decimal> GetBasket()
        {
            return Basket;
        }

        public void AddItem(string itemCode, decimal quantity)
        {
            ValidateQuantity(quantity);

            if (!Basket.ContainsKey(itemCode))
                Basket.Add(itemCode, quantity);
            else
                Basket[itemCode] += quantity;
        }

        public void RemoveQuantity(string itemCode, decimal quantity)
        {
            ValidateQuantity(quantity);
            ValidateItem(itemCode);

            if (Basket.ContainsKey(itemCode))
            {
                var remainingQty = Basket[itemCode] - quantity;
                if (remainingQty <= 0)
                    Basket.Remove(itemCode);
                else
                    Basket[itemCode] = remainingQty;
            }
        }

        void ValidateQuantity(decimal quantity)
        {
            if (quantity <= 0)
                throw new Exception("Validation exception: Quantity must be grater than 0");
        }

        void ValidateItem(string itemCode)
        {
            if(!Basket.ContainsKey(itemCode))
                throw new Exception("Validation exception: Item doesn't exist in the cart");
        }
    }
}
