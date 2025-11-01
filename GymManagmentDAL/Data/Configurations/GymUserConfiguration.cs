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
    public class GymUserConfiguration<T> : IEntityTypeConfiguration<T> where T : GymUser
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(x => x.Name)
                .HasColumnType("varchar")
                .HasMaxLength(50);

            builder.Property(x => x.Email)
                .HasColumnType("varchar")
                .HasMaxLength(100);

            builder.Property(x => x.Phone)
                .HasColumnType("varchar")
                .HasMaxLength(11);

            builder.OwnsOne(x => x.Address, address =>
            {
                address.Property(a => a.City)
                    .HasColumnType("varchar")
                    .HasColumnName("City")
                    .HasMaxLength(50);

                address.Property(a => a.Street)
                    .HasColumnType("varchar")
                    .HasColumnName("Street")
                    .HasMaxLength(100);
            });

            builder.HasIndex(x => x.Email).IsUnique();
            builder.HasIndex(x => x.Phone).IsUnique();

            //builder.ToTable("GymUsers", t =>
            //{
            //    t.HasCheckConstraint("GymUser_EmailCheck", "Email LIKE '_%@_%._%'");
            //    t.HasCheckConstraint("GymUser_PhoneCheck", "Phone LIKE '01%' AND Phone NOT LIKE '%[^0-9]%'");
            //});
        }
    }

}
