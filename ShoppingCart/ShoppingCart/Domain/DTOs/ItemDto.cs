using System;
using System.ComponentModel.DataAnnotations;

namespace ShoppingCart.Domain.DTOs
{
    [Serializable]
    public class ItemDto : Dto
    {
        public Guid? ID { get; set; }

        [Required]
        public string ItemCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        public decimal? Quantity { get; set; }
    }
}
