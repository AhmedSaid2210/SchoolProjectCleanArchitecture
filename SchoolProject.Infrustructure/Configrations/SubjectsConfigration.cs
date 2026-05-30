using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Data.Entities;

namespace SchoolProject.Infrustructure.Configrations
{
    public class SubjectsConfigration : IEntityTypeConfiguration<Subjects>
    {
        public void Configure(EntityTypeBuilder<Subjects> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.SubjectName).IsRequired().HasMaxLength(50);

            builder.Property(x => x.Period).IsRequired();

            
        }
    }
}
