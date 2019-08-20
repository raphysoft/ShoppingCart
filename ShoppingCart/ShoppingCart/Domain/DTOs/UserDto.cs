using System;
using System.ComponentModel.DataAnnotations;

namespace ShoppingCart.Domain.DTOs
{
    [Serializable]
    public class UserDto : Dto
    { 
        public Guid? ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }
    }
}
