using GymManagmentDAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Data.Configurations
{
    public class TrainerConfiguration : GymUserConfiguration<Trainer> ,IEntityTypeConfiguration<Trainer>
    {
        public new void Configure(EntityTypeBuilder<Trainer> builder)
        {
            builder.Property(x => x.CreatedAt)
                .HasColumnName("HireDate")
                .HasDefaultValueSql("GETDATE()");

            builder.ToTable("Trainers", t =>
            {
                t.HasCheckConstraint("Trainers_EmailCheck", "Email LIKE '_%@_%._%'");
                t.HasCheckConstraint("Trainers_PhoneCheck", "Phone LIKE '01%' AND Phone NOT LIKE '%[^0-9]%'");
            });
            // base.Configure(builder);
        }
    }

}
