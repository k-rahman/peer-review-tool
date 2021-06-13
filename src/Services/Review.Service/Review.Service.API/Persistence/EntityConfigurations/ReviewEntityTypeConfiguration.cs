using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Review.Service.API.Domain.Models;

namespace Review.Service.API.Persistence.EntityConfigurations
{
        public class ReviewEntityTypeConfiguration :
        IEntityTypeConfiguration<Domain.Models.Review>
        {
                public void Configure(EntityTypeBuilder<Domain.Models.Review> builder)
                {
                        builder.ToTable("reviews");

                        builder.HasKey(review => review.Id);

                        builder.Property(review => review.Id)
                              .HasColumnName("id");

                        builder.Property(review => review.Created)
                              .HasColumnName("created");

                        builder.Property(review => review.Modified)
                              .HasColumnName("modified");

                        builder.Property(review => review.ReviewerId)
                              .HasColumnName("reviewer_id")
                              .HasMaxLength(255)
                              .IsRequired();

                        builder.Property(review => review.SubmissionId)
                              .HasColumnName("submission_id");

                        builder.HasMany(review => review.Criteria)
                              .WithMany(review => review.Reviews)
                              .UsingEntity<Grade>(
                                    grade => grade
                                                .HasOne(grade => grade.Criterion)
                                                .WithMany(criterion => criterion.Grades)
                                                .HasForeignKey(grade => grade.CriterionId),
                                    grade => grade
                                                .HasOne(grade => grade.Review)
                                                .WithMany(review => review.Grades)
                                                .HasForeignKey(grade => grade.ReviewId),
                                    grade =>
                                    {
                                            grade.ToTable("grades");
                                            grade.Property(grade => grade.Points).HasColumnName("points");
                                            grade.Property(grade => grade.Feedback).HasColumnName("feedback");
                                            grade.Property(grade => grade.ReviewId).HasColumnName("review_id");
                                            grade.Property(grade => grade.CriterionId).HasColumnName("criterion_id");
                                            grade.HasKey(grade => new { grade.ReviewId, grade.CriterionId });
                                    }
                              );
                }
        }
}