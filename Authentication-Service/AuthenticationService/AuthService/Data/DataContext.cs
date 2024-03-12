using AuthService.Models.User;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<UserModel> Users { get; set; }
    }
}
