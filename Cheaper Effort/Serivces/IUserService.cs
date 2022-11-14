using System;
using System.Security.Claims;
using Cheaper_Effort.Data;
using Cheaper_Effort.Models;

namespace Cheaper_Effort.Serivces
{
    public interface IUserService
    {
        public bool CheckUser(string firstData, string secondData, ProjectDbContext _context);
        public ClaimsPrincipal SetName(string username, ProjectDbContext _context);
        public void AddToDB(Account Account, ProjectDbContext _context);
    }
}

