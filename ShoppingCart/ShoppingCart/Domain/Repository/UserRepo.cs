using ShoppingCart.Domain.DTOs;
using System;

namespace ShoppingCart.Domain.Repository
{
    public static class UserRepo
    {
        private static void ValidateUserDto(UserDto userDto)
        {
            if (string.IsNullOrEmpty(userDto.Email))
                throw new Exception($"Email is required");

            if (userDto.Mode != EnumCrudMode.Create)
                ValidateUserEmail(userDto.Email);
        }

        private static void ValidateUserEmail(string email)
        {
            if (!DBManager.userRepo.ContainsKey(email))
                throw new Exception($@"Validation Exception: User with with email: {email} doesn't exists");
        }

        public static User GetUser(string email)
        {
            ValidateUserEmail(email);
            return DBManager.userRepo[email];
        }

        public static void AddUser(UserDto userDto)
        {
            ValidateUserDto(userDto);

            if (DBManager.userRepo.ContainsKey(userDto.Email))
                return;
            //    throw new Exception($@"Validation Exception: User with same email: {userDto.Email} already exists");
            
            var user = userDto.User();
            user.ID = DBManager.GenerateKey();
            userDto.ID = user.ID;
            DBManager.userRepo.Add(user.Email, user);
        }

        public static void UpdateUser(UserDto userDto)
        {
            ValidateUserDto(userDto);

            var user = GetUser(userDto.Email);
            user.Name = userDto.Name;
        }

        public static void DeleteUser(UserDto userDto)
        {
            ValidateUserDto(userDto);

            var user = DBManager.userRepo[userDto.Email];
            user.Archive();
        }
    }
}
