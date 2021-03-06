// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Review.Service.API.Persistence.Contexts;

namespace Review.Service.API.Migrations
{
        [DbContext(typeof(ReviewContext))]
        [Migration("20210529120748_InitialModel")]
        partial class InitialModel
        {
                protected override void BuildTargetModel(ModelBuilder modelBuilder)
                {
#pragma warning disable 612, 618
                        modelBuilder
                            .HasAnnotation("Relational:MaxIdentifierLength", 63)
                            .HasAnnotation("ProductVersion", "5.0.6")
                            .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                        modelBuilder.Entity("Review.Service.API.Domain.Models.Criterion", b =>
                            {
                                    b.Property<int>("Id")
                            .ValueGeneratedOnAdd()
                            .HasColumnType("integer")
                            .HasColumnName("id")
                            .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                                    b.Property<string>("Description")
                            .IsRequired()
                            .HasColumnType("text")
                            .HasColumnName("description");

                                    b.Property<int>("MaxPoints")
                            .HasColumnType("integer")
                            .HasColumnName("max_points");

                                    b.Property<Guid>("TaskUid")
                            .HasColumnType("uuid")
                            .HasColumnName("task_uid");

                                    b.HasKey("Id");

                                    b.ToTable("criteria");
                            });

                        modelBuilder.Entity("Review.Service.API.Domain.Models.Grade", b =>
                            {
                                    b.Property<int>("ReviewId")
                            .HasColumnType("integer")
                            .HasColumnName("review_id");

                                    b.Property<int>("CriterionId")
                            .HasColumnType("integer")
                            .HasColumnName("criterion_id");

                                    b.Property<string>("Feedback")
                            .HasColumnType("text")
                            .HasColumnName("feedback");

                                    b.Property<int?>("Points")
                            .HasColumnType("integer")
                            .HasColumnName("points");

                                    b.HasKey("ReviewId", "CriterionId");

                                    b.HasIndex("CriterionId");

                                    b.ToTable("grades");
                            });

                        modelBuilder.Entity("Review.Service.API.Domain.Models.Review", b =>
                            {
                                    b.Property<int>("Id")
                            .ValueGeneratedOnAdd()
                            .HasColumnType("integer")
                            .HasColumnName("id")
                            .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                                    b.Property<DateTimeOffset?>("Created")
                            .HasColumnType("timestamp with time zone")
                            .HasColumnName("created");

                                    b.Property<DateTimeOffset?>("Modified")
                            .HasColumnType("timestamp with time zone")
                            .HasColumnName("modified");

                                    b.Property<int>("ReviewerId")
                            .HasColumnType("integer")
                            .HasColumnName("reviewer_id");

                                    b.Property<int>("WorkId")
                            .HasColumnType("integer")
                            .HasColumnName("work_id");

                                    b.HasKey("Id");

                                    b.HasIndex("WorkId");

                                    b.ToTable("reviews");
                            });

                        modelBuilder.Entity("Review.Service.API.Domain.Models.Work", b =>
                            {
                                    b.Property<int>("Id")
                            .ValueGeneratedOnAdd()
                            .HasColumnType("integer")
                            .HasColumnName("id")
                            .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                                    b.Property<int>("AuthorId")
                            .HasColumnType("integer")
                            .HasColumnName("author_id");

                                    b.Property<string>("Content")
                            .HasColumnType("text")
                            .HasColumnName("content");

                                    b.Property<Guid>("TaskUid")
                            .HasColumnType("uuid")
                            .HasColumnName("task_uid");

                                    b.HasKey("Id");

                                    b.ToTable("works");
                            });

                        modelBuilder.Entity("Review.Service.API.Domain.Models.Grade", b =>
                            {
                                    b.HasOne("Review.Service.API.Domain.Models.Criterion", "Criterion")
                            .WithMany("Grades")
                            .HasForeignKey("CriterionId")
                            .OnDelete(DeleteBehavior.Cascade)
                            .IsRequired();

                                    b.HasOne("Review.Service.API.Domain.Models.Review", "Review")
                            .WithMany("Grades")
                            .HasForeignKey("ReviewId")
                            .OnDelete(DeleteBehavior.Cascade)
                            .IsRequired();

                                    b.Navigation("Criterion");

                                    b.Navigation("Review");
                            });

                        modelBuilder.Entity("Review.Service.API.Domain.Models.Review", b =>
                            {
                                    b.HasOne("Review.Service.API.Domain.Models.Work", "Work")
                            .WithMany()
                            .HasForeignKey("WorkId")
                            .OnDelete(DeleteBehavior.Cascade)
                            .IsRequired();

                                    b.Navigation("Work");
                            });

                        modelBuilder.Entity("Review.Service.API.Domain.Models.Criterion", b =>
                            {
                                    b.Navigation("Grades");
                            });

                        modelBuilder.Entity("Review.Service.API.Domain.Models.Review", b =>
                            {
                                    b.Navigation("Grades");
                            });
#pragma warning restore 612, 618
                }
        }
}
