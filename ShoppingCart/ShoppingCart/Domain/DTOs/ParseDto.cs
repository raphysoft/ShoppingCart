namespace ShoppingCart.Domain.DTOs
{
    public static class ParseDto
    {
        public static User User(this UserDto userDto)
        {
            return new User() { Email = userDto.Email, Name = userDto.Name };
        }

        public static Item Item(this ItemDto itemDto)
        {
            return new Item() { ItemCode = itemDto.ItemCode, Description = itemDto.Description };
        }
    }
}