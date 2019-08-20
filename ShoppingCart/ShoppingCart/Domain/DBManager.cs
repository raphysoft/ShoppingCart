using System;
using System.Collections.Generic;

namespace ShoppingCart.Domain
{
    public static class DBManager
    {
        public static Dictionary<string, Cart> userCartRepo = new Dictionary<string, Cart>();
        public static Dictionary<string, User> userRepo = new Dictionary<string, User>();
        public static Dictionary<string, Item> itemRepo = new Dictionary<string, Item>();

        public static Guid GenerateKey()
        {
            return Guid.NewGuid();
        }
    }
}
