using Microsoft.EntityFrameworkCore;
using studentsApi.Core.Models;

namespace studentsApi.Persistence
{
    public class MyDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public MyDbContext(DbContextOptions<MyDbContext> options)
         : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}