using Client.ShoppingCart;
using NUnit.Framework;
using System.Text;

namespace Client
{
    [TestFixture]
    public class CartUnitTests
    {
        CartAPI _cartAPI = new CartAPI();

        [Test]
        [TestCase(EnumCrudMode.Create, "rafael@email.com", "")]
        public void UpsertUser(EnumCrudMode mode, string email, string name)
        {
            CreateUser(mode, email, name);
        }

        private Result CreateUser(EnumCrudMode mode, string email, string name)
        {
            var result = _cartAPI.UpsertUser(new UserDto()
            { Mode = mode, Email = email, Name = name });
            Assert.AreEqual(EnumResultCode.OK, result.ResultCode, result.Details, null);
            return result;
        }

        [Test]
        [TestCase(EnumCrudMode.Create, "Item1", "descA")]
        [TestCase(EnumCrudMode.Create, "Item2", "descB")]
        [TestCase(EnumCrudMode.Create, "Item3", "descC")]
        public void UpsertItem(EnumCrudMode mode, string itemCode, string description)
        {
            CreateItem(mode, itemCode, description);
        }

        private Result CreateItem(EnumCrudMode mode, string itemCode, string description)
        {
            var result = _cartAPI.UpsertItem(new ItemDto()
            { Mode = mode, ItemCode = itemCode, Description = description });
            Assert.AreEqual(EnumResultCode.OK, result.ResultCode, result.Details, null);
            return result;
        }

        [Test]
        public void FullTest()
        {
            var itemCode1 = "itemA";
            var itemCode2 = "itemB";

            var userResult = CreateUser(EnumCrudMode.Create, "rafael@test.com", "raf");

            var itemResult1 = CreateItem(EnumCrudMode.Create, itemCode1, "test item");
            var itemResult2 = CreateItem(EnumCrudMode.Create, itemCode2, "test item2");

            var userDto = (UserDto)userResult.Dto;
            var itemDto1 = (ItemDto)itemResult1.Dto;
            itemDto1.Quantity = 1;
            var itemDto2 = (ItemDto)itemResult2.Dto;
            itemDto2.Quantity = 2;

            var cartResult = _cartAPI.CreateNewCart(userDto);
            Assert.AreEqual(EnumResultCode.OK, cartResult.ResultCode, cartResult.Details, null);
            
            var addingResult = _cartAPI.AddItemToCart(itemDto1, userDto);
            Assert.AreEqual(EnumResultCode.OK, addingResult.ResultCode, addingResult.Details, null);

            addingResult = _cartAPI.AddItemToCart(itemDto2, userDto);
            Assert.AreEqual(EnumResultCode.OK, addingResult.ResultCode, addingResult.Details, null);


            var itemNamesResult = _cartAPI.GetCartItemNames(userDto);
            Assert.AreEqual(EnumResultCode.OK, itemNamesResult.ResultCode, itemNamesResult.Details, null);
            StringBuilder sb = new StringBuilder();
            sb.Append($@"{itemCode1},");
            sb.Append($@"{itemCode2},");
            Assert.AreEqual(sb.ToString(), itemNamesResult.Dto);
        }
    }
}
