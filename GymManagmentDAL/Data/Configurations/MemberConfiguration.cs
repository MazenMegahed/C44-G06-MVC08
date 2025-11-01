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
    public class MemberConfiguration : GymUserConfiguration<Member>, IEntityTypeConfiguration<Member>
    {
        public new void Configure(EntityTypeBuilder<Member> builder)
        {
            builder.Property(x => x.CreatedAt)
                .HasColumnName("JoinDate")
                .HasDefaultValueSql("GETDATE()");

            builder.HasOne(x => x.HealthRecord)
                .WithOne()
                .HasForeignKey<HealthRecord>(x => x.Id);
            builder.ToTable("Members", t =>
            {
                t.HasCheckConstraint("Members_EmailCheck", "Email LIKE '_%@_%._%'");
                t.HasCheckConstraint("Members_PhoneCheck", "Phone LIKE '01%' AND Phone NOT LIKE '%[^0-9]%'");
            });
            base.Configure(builder);
        }
    }

}

