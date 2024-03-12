using Microsoft.EntityFrameworkCore;
using ProfileMicroService.Models;

namespace ProfileMicroService.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Profile> Profiles { get; set; }
    }
}
