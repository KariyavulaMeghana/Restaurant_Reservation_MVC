﻿using NuGet.Protocol.Plugins;
using Restaurant_Reservation_MVC.DTO;
using Restaurant_Reservation_MVC.Models;

namespace Restaurant_Reservation_MVC.IServices
{
    public interface IUserServices
    {
        Task<string> RegisterNewUser(User user);
        //Task<string> Login(LoginModel loginModel);
        Task<LoginResponse> Login(LoginModel loginModel);
        Task<string> DeleteUser(int id);
        Task<string> UpdateUser(int id, User user);
        Task<List<UserDTO>> GetAllUsers();
        Task<User> GetUserById(int id);
    }
}
