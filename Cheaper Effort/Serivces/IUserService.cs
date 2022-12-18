﻿using System;
using System.Security.Claims;
using Cheaper_Effort.Data;
using Cheaper_Effort.Models;

namespace Cheaper_Effort.Serivces
{
    public interface IUserService
    {
        bool CheckUserData(string username, string password);
        string? GetUserPicture(string username, string password);
        bool CheckUserRegister(string username, string email);
        Task AddToDBasync(Account Account, IFormFile picture);
        /*Task AddPointToDBAsync(int Points, Account Account);*/
        Task SubtractPointToDBAsync(int Points, Account Account);

        Task AddDiscountToDB(string Name, Discount Discount);
    }
}

