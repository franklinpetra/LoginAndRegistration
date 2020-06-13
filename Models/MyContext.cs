using Microsoft.EntityFrameworkCore;


namespace LoginAndRegistration.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options){}

        public DbSet<LRUser> LRUsers { get; set; }
    }
}