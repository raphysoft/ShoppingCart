using System;

namespace ShoppingCart.Domain.Repository
{
    public static class CartRepo
    {
        /// <summary>
        /// Each user has only one cart?
        /// </summary>
        /// <param name="userEmail"></param>
        /// <param name="cart"></param>
        public static void CreateNewCart(string userEmail)
        {
            var cart = new Cart();

            if (!DBManager.userCartRepo.ContainsKey(userEmail))
                DBManager.userCartRepo.Add(userEmail, cart);
            else
                DBManager.userCartRepo[userEmail] = cart;
        }

        public static Cart GetUserCart(string userEmail)
        {
            if (DBManager.userCartRepo.ContainsKey(userEmail))
                return DBManager.userCartRepo[userEmail];

            throw new Exception("User cart doesn't exist");
        }


    }
}