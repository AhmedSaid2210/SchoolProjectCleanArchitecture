using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Data.Entities;

namespace SchoolProject.Infrustructure.Configrations
{
    public class InstructorConfigration : IEntityTypeConfiguration<Instructor>
    {
        public void Configure(EntityTypeBuilder<Instructor> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(x => x.Address)
                   .HasMaxLength(250);

            builder.Property(x => x.Position)
                   .HasMaxLength(100);

            builder.Property(x => x.Salary)
                   .HasColumnType("decimal(18,2)");

            builder.HasOne(x => x.InstructorSupervisor)
                   .WithMany(x => x.Instructors)
                   .HasForeignKey(x => x.SupervisorID)
                   .OnDelete(DeleteBehavior.Restrict);


        }
    }
}
