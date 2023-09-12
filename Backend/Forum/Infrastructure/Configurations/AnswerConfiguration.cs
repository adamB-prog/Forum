using Domain.Entities.Answers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    internal class AnswerConfiguration : IEntityTypeConfiguration<Answer>
    {
        public void Configure(EntityTypeBuilder<Answer> builder)
        {
            //PK
            builder.HasKey(a => a.Id);

            //Properies
            builder.Property(a => a.Description).IsRequired().HasMaxLength(1000);

            //Relationships

            builder.HasOne(a => a.Post)
                .WithMany(p => p.Answers)
                .HasForeignKey(a => a.PostId)
                .OnDelete(DeleteBehavior.Cascade);


            //builder.HasMany(a => a.LikesFromUsers)
            //    .WithMany(u => u.LikedAnswers);

            //builder.HasMany(a => a.DislikesFromUsers)
            //    .WithMany(u => u.DislikedAnswers);


            builder.HasOne(a => a.Owner)
                .WithMany(u => u.Answers)
                .HasForeignKey(a => a.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
