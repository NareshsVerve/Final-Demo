using Microsoft.EntityFrameworkCore;
using SiteInspectionWebApi.Models.Database_Models;

namespace SiteInspectionWebApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option) : base(option)
        {

        }

        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<State> States { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Site> Sites { get; set; }
        public virtual DbSet<Assignment> Assignments { get; set; }
        public virtual DbSet<ErrorFinding> ErrorFindings { get; set; }
        public virtual DbSet<LoggerEntry> Loggers { get; set; }
        public virtual DbSet<Otp> Otps { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Site>()
                        .HasOne(s => s.Country)
                        .WithMany()
                        .HasForeignKey(s => s.CountryId)
                        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Site>()
                       .HasOne(s => s.State)
                       .WithMany()
                       .HasForeignKey(s => s.StateId)
                       .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Assignment>()
                   .HasOne(a => a.User)
                   .WithMany(u => u.Assignments)
                   .HasForeignKey(a => a.UserId)
                   .OnDelete(DeleteBehavior.Restrict); // Change to Restrict
        }



        ////OnConfiguring() method is used to select and configure the data source
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    //Enabling Lazy Loading
        //    optionsBuilder.UseLazyLoadingProxies();
        //}
        //OnModelCreating() method is used to configure the model using ModelBuilder Fluent API

      

    }
}
