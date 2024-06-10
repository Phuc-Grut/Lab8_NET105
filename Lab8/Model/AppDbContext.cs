using Lab8.Model;
using Microsoft.EntityFrameworkCore;

namespace Lab8.Model
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
                
        }

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=PHUC\\SQLEXPRESS;Initial Catalog=Lab8API;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<NhanVien> nhanViens { get; set; }  
    }
}
