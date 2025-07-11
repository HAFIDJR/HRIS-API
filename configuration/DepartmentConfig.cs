using HRIS.models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRIS.Configuration
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.HasKey(d => d.DepID);

            builder.Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(d => d.Location)
                .HasMaxLength(150);

            builder.HasOne(d => d.Manager)
                .WithOne(m => m.Department)
                .HasForeignKey<Department>(d => d.ManagerID)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(d => d.Employees)
                .WithOne(e => e.Department)
                .HasForeignKey(e => e.DepID)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}