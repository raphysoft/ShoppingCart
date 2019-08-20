using ShoppingCart.Domain.DTOs;
using System;

namespace ShoppingCart.Domain.Repository
{
    public static class ItemRepo
    {
        private static void ValidateItemDto(ItemDto itemDto)
        {
            if (string.IsNullOrEmpty(itemDto.ItemCode))
                throw new Exception($"Desciprtion is required");

            if (itemDto.Mode != EnumCrudMode.Create)
                ValidateItemName(itemDto.ItemCode);
        }

        private static void ValidateItemName(string itemName)
        {
            if (!DBManager.itemRepo.ContainsKey(itemName))
                throw new Exception($@"Validation Exception: Item with name: {itemName} doesn't exists");
        }

        public static Item GetItem(string itemName)
        {
            ValidateItemName(itemName);
            return DBManager.itemRepo[itemName];
        }

        public static void AddItem(ItemDto itemDto)
        {
            ValidateItemDto(itemDto);

            if (DBManager.itemRepo.ContainsKey(itemDto.ItemCode))
                return;

            var item = itemDto.Item();
            item.ID = DBManager.GenerateKey();
            itemDto.ID = item.ID;
            DBManager.itemRepo.Add(itemDto.ItemCode, item);
        }

        public static void UpdateItem(ItemDto itemDto)
        {
            ValidateItemDto(itemDto);
            var item = GetItem(itemDto.ItemCode);
            item.Name = itemDto.Name;
            item.Description = itemDto.Description;
        }

        public static void DeleteItem(ItemDto itemDto)
        {
            ValidateItemDto(itemDto);

            var item = DBManager.itemRepo[itemDto.ItemCode];
            item.Archive();
        }
    }
}
