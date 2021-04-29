
using System.Threading.Tasks;
using danceschool.Models;
using Microsoft.EntityFrameworkCore;

namespace danceschool.Context
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Course> Course { get; set; }
        public DbSet<DanceClass> DanceClass { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<Instructor> Instructor { get; set; }
        public DbSet<Booking> Booking { get; set; }
        public DbSet<Membership> Membership { get; set; }
        public DbSet<Subscription> Subscription { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
           => options.UseSqlServer("Server=tcp:danceschoolserver.database.windows.net,1433;Initial Catalog=danceschooldb;Persist Security Info=False;User ID=serveradmin;Password=#8qp4MsuNeGswFz;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");


        public ApplicationContext(DbContextOptions<ApplicationContext> options)
                : base(options)
        { }

        public async Task<int> SaveChanges() => await base.SaveChangesAsync();
    }


}

