using ShoppingCart.Domain.Interfaces;
using System;

namespace ShoppingCart.Domain
{
    public class Entity : IHaveAnID, IHaveAName
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
    }
}
