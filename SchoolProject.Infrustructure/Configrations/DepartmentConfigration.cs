using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Data.Entities;

namespace SchoolProject.Infrustructure.Configrations
{
    public class DepartmentConfigration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.HasKey(d => d.Id);

            builder.Property(d=>d.DName)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasMany(s => s.Students)
                   .WithOne(d => d.Department)
                   .HasForeignKey(d => d.DID)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.InstructorManager)
                   .WithOne(x=>x.DepartmentManger)
                   .HasForeignKey<Department>(x => x.InstructorManagerID)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(i => i.Instructors)
                    .WithOne(d => d.Department)
                    .HasForeignKey(d => d.DID)
                    .OnDelete(DeleteBehavior.Restrict);

       
        }
    }
}
