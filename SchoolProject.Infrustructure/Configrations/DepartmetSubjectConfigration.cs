using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Data.Entities;

namespace SchoolProject.Infrustructure.Configrations
{
    public class DepartmetSubjectConfigration : IEntityTypeConfiguration<DepartmetSubject>
    {
        public void Configure(EntityTypeBuilder<DepartmetSubject> builder)
        {
            builder.HasKey(x => new
            {
                x.DID,
                x.SubID
            });

            builder.HasOne(x => x.Department)
                   .WithMany(x => x.DepartmentSubjects)
                   .HasForeignKey(x => x.DID);


            builder.HasOne(x => x.Subjects)
                   .WithMany(x => x.DepartmetsSubjects)
                   .HasForeignKey(x => x.SubID);
                   
        }
    
    }
}
