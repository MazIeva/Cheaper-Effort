using Cheaper_Effort.Models;
using Microsoft.EntityFrameworkCore;

namespace Cheaper_Effort.Data
{
    public class LoginDbContext : DbContext
    {

        public LoginDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Login> Logins { get; set; }
    }
}
