using GymManagementDAL_Entities;
using GymManagmentDAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Data.Contexts
{
    public class GymDbContext : IdentityDbContext<ApplicationUser>
    {
        public GymDbContext(DbContextOptions<GymDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // TPH Configuration - All in one table
            //modelBuilder.Entity<GymUser>()
            //    .HasDiscriminator<string>("UserType")
            //   .HasValue<Trainer>("Trainer")
            //   .HasValue<Member>("Member");

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.Entity<ApplicationUser>(au =>
            {
                au.Property(x => x.FirstName)
                    .HasColumnType("varchar")
                    .HasMaxLength(50);
                au.Property(x => x.LastName)
                    .HasColumnType("varchar")
                    .HasMaxLength(50);
            });
        }


        #region DbSets
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<Member> Members { get; set; }
        //public DbSet<GymUser> GymUsers { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Membership> Memberships { get; set; }
        public DbSet<HealthRecord> HealthRecords { get; set; }
        public DbSet<Plan> Plans { get; set; }
        #endregion
    }
}

