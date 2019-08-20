using ShoppingCart.Domain;
using ShoppingCart.Domain.DTOs;
using ShoppingCart.Domain.Repository;
using System;
using System.Text;
using System.Web.Services;

namespace ShoppingCart
{
    /// <summary>
    /// Summary description for CartAPI
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class CartAPI : System.Web.Services.WebService
    {
        [WebMethod]
        public Result UpsertUser(UserDto userDto)
        {
            try
            {
                if (userDto.Mode == EnumCrudMode.Create)
                    UserRepo.AddUser(userDto);
                else if (userDto.Mode == EnumCrudMode.Update)
                    UserRepo.UpdateUser(userDto);
                else if (userDto.Mode == EnumCrudMode.Delete)
                    UserRepo.DeleteUser(userDto);

                return new Result() { ResultCode = EnumResultCode.OK, Dto = userDto };
            }
            catch (Exception ex)
            {
                return new Result() { ResultCode = EnumResultCode.Error, Details = ex.Message, Dto = userDto };
            }
        }

        [WebMethod]
        public Result UpsertItem(ItemDto itemDto)
        {
            try
            {
                if (itemDto.Mode == EnumCrudMode.Create)
                    ItemRepo.AddItem(itemDto);
                else if (itemDto.Mode == EnumCrudMode.Update)
                    ItemRepo.UpdateItem(itemDto);
                else if (itemDto.Mode == EnumCrudMode.Delete)
                    ItemRepo.DeleteItem(itemDto);

                return new Result() { ResultCode = EnumResultCode.OK, Dto = itemDto };
            }
            catch (Exception ex)
            {
                return new Result() { ResultCode = EnumResultCode.Error, Details = ex.Message, Dto = itemDto };
            }
        }

        [WebMethod]
        public Result CreateNewCart(UserDto userDto)
        {
            try
            {
                if (userDto?.Email == null)
                    throw new Exception("Validation exception: User doesn't exist");

                CartRepo.CreateNewCart(userDto.Email);

                return new Result() { ResultCode = EnumResultCode.OK };
            }
            catch (Exception ex)
            {
                return new Result() { ResultCode = EnumResultCode.Error, Details = ex.Message };
            }
        }

        [WebMethod]
        public Result AddItemToCart(ItemDto itemDto, UserDto userDto)
        {
            try
            {
                if (itemDto?.ItemCode == null)
                    throw new Exception("Validation exception: Item doesn't exist");

                if (itemDto?.Quantity == null)
                    throw new Exception("Validation exception: Item quantity is not specified");

                if (userDto?.Email == null)
                    throw new Exception("Validation exception: User doesn't exist");

                var cart = CartRepo.GetUserCart(userDto.Email);

                cart.AddItem(itemDto.ItemCode, itemDto.Quantity.Value);

                return new Result() { ResultCode = EnumResultCode.OK };
            }
            catch (Exception ex)
            {
                return new Result() { ResultCode = EnumResultCode.Error, Details = ex.Message };
            }
        }

        [WebMethod]
        public Result GetCartItemNames(UserDto userDto)
        {
            try
            {
                var cart = CartRepo.GetUserCart(userDto.Email);

                var basket = cart.GetBasket();
                StringBuilder sb = new StringBuilder();
                foreach (var elem in basket)
                {
                    sb.Append($@"{ItemRepo.GetItem(elem.Key).ItemCode},");
                }

                return new Result() { ResultCode = EnumResultCode.OK, Dto = sb.ToString() };
            }
            catch (Exception ex)
            {
                return new Result() { ResultCode = EnumResultCode.Error, Details = ex.Message };
            }
        }
    }

    public enum EnumResultCode
    {
        OK, Error
    }

    [Serializable]
    public class Result
    {
        public EnumResultCode ResultCode { get; set; }
        public string Details { get; set; }
        public dynamic Dto { get; set; }
    }
}
