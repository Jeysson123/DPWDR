using Microsoft.EntityFrameworkCore;
using Technical.Interview.Model.Dtos;
using Technical.Interview.Model.Entities;

namespace Technical.Interview.Model
{
    /*class that connect EntityFramework and SqlServer*/
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Product> Product { get; set; }


        public ApplicationDbContext()
        {

        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Rating>().HasNoKey();
            modelBuilder.Entity<Product>().Ignore(p => p.Rating);

        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer(@"Server=DESKTOP-DH1O5F3;Database=DPWDR;MultipleActiveResultSets=true;Integrated Security=True");
        }

    }
}
