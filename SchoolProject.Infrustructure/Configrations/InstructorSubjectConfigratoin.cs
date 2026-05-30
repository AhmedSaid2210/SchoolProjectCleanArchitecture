using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Data.Entities;


namespace SchoolProject.Infrustructure.Configrations
{
    public class InstructorSubjectConfigratoin : IEntityTypeConfiguration<InstructorSubject>
    {
        public void Configure(EntityTypeBuilder<InstructorSubject> builder)
        {
            builder.HasKey(x => new
            {
                x.InsId,
                x.SubId
            });

            builder.HasOne(x => x.Instructor)
                   .WithMany(x => x.InstructorSubjects)
                   .HasForeignKey(x => x.InsId);

            builder.HasOne(x => x.Subjects)
                   .WithMany(x => x.InstructorSubjects)
                   .HasForeignKey(x => x.SubId);
        }
    }
}
