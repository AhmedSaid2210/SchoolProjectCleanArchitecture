using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Data.Entities;


namespace SchoolProject.Infrustructure.Configrations
{
    public class StudentSubjectConfigration : IEntityTypeConfiguration<StudentSubject>
    {
        public void Configure(EntityTypeBuilder<StudentSubject> builder)
        {
            builder.HasKey(x => new
            {
                x.StudID,
                x.SubID
            });

            builder.HasOne(x => x.Student)
                   .WithMany(x => x.StudentsSubjects)
                   .HasForeignKey(x => x.StudID);

            builder.HasOne(x => x.Subject)
                   .WithMany(x => x.StudentsSubjects)
                   .HasForeignKey(x => x.SubID);
        }
    }
}
