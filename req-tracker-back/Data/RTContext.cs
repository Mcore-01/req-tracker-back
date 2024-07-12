using Microsoft.EntityFrameworkCore;
using req_tracker_back.Models;

namespace req_tracker_back.Data
{
    public class RTContext(DbContextOptions<RTContext> options) : DbContext(options)
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Status>().HasData(
                new Status() { Id = 1, Name = "Создана" }, 
                new Status() { Id = 2, Name = "В работе" }, 
                new Status() { Id = 3, Name = "На проверке" }, 
                new Status() { Id = 4, Name = "Завершена" });
        }

        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Status> Status { get; set; }

    }
}