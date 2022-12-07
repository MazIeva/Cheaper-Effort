using Cheaper_Effort.Data;
using Cheaper_Effort.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cheaper_Effort.Pages.Shared
{
    public class UserImageController : Controller
    {
        private readonly ProjectDbContext _context;
        public UserImageController(ProjectDbContext context)
        { 
            _context = context;
        }
        public string UserImage()
        {
            return Convert.ToBase64String(_context.User.SingleOrDefault(o => o.Username.Equals(User.Identity.Name)).Picture);
        }
    }
}
