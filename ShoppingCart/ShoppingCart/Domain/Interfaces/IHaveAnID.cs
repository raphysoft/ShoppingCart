using System;

namespace ShoppingCart.Domain.Interfaces
{
    public interface IHaveAnID
    {
        Guid ID { get; set; }
    }
}
