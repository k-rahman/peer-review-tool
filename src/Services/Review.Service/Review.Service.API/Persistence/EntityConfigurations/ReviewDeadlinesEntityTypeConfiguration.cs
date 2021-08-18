using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Review.Service.API.Domain.Models;

namespace Review.Service.API.Persistence.EntityConfigurations
{
	public class ReviewDeadlinesEntityTypeConfiguration :
	IEntityTypeConfiguration<ReviewDeadlines>
	{
		public void Configure(EntityTypeBuilder<ReviewDeadlines> builder)
		{
			builder.ToTable("review_deadlines");

			builder.HasKey(reviewDeadlines => reviewDeadlines.Id);

			builder.Property(reviewDeadlines => reviewDeadlines.Id)
						.HasColumnName("id");

			builder.Property(reviewDeadlines => reviewDeadlines.Uid)
						.HasColumnName("uid")
						.HasColumnType("uuid")
						.IsRequired();

			builder.Property(reviewDeadlines => reviewDeadlines.ReviewStart)
						.HasColumnName("review_start");

			builder.Property(reviewDeadlines => reviewDeadlines.ReviewEnd)
						.HasColumnName("review_end");

			builder.Property(reviewDeadlines => reviewDeadlines.InstructorId)
						.HasColumnName("instructor_id")
						.HasMaxLength(255)
						.IsRequired();
		}
	}
}