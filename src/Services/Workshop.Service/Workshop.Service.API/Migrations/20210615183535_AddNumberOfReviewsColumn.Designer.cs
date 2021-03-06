// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Workshop.Service.API.Persistence.Contexts;

namespace Workshop.Service.API.Migrations
{
    [DbContext(typeof(WorkshopContext))]
    [Migration("20210615183535_AddNumberOfReviewsColumn")]
    partial class AddNumberOfReviewsColumn
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Workshop.Service.API.Domain.Models.Criterion", b =>
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

                    b.Property<int>("WorkshopId")
                        .HasColumnType("integer")
                        .HasColumnName("workshop_id");

                    b.HasKey("Id");

                    b.HasIndex("WorkshopId");

                    b.ToTable("criteria");
                });

            modelBuilder.Entity("Workshop.Service.API.Domain.Models.Participant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Auth0Id")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("auth0_id");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("email");

                    b.HasKey("Id");

                    b.ToTable("participants");
                });

            modelBuilder.Entity("Workshop.Service.API.Domain.Models.Workshop", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("InstructorId")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("instructor_id");

                    b.Property<DateTimeOffset?>("Modified")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("modified");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("name");

                    b.Property<int>("NumberOfReviews")
                        .HasColumnType("integer")
                        .HasColumnName("number_of_reviews");

                    b.Property<DateTimeOffset>("Published")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("published");

                    b.Property<DateTimeOffset>("ReviewEnd")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("review_end");

                    b.Property<DateTimeOffset>("ReviewStart")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("review_start");

                    b.Property<DateTimeOffset>("SubmissionEnd")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("submission_end");

                    b.Property<DateTimeOffset>("SubmissionStart")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("submission_start");

                    b.Property<Guid>("Uid")
                        .HasColumnType("uuid")
                        .HasColumnName("uid");

                    b.HasKey("Id");

                    b.HasIndex("Uid")
                        .IsUnique();

                    b.ToTable("workshops");
                });

            modelBuilder.Entity("workshop_participants", b =>
                {
                    b.Property<int>("participant_id")
                        .HasColumnType("integer");

                    b.Property<int>("workshop_id")
                        .HasColumnType("integer");

                    b.HasKey("participant_id", "workshop_id");

                    b.HasIndex("workshop_id");

                    b.ToTable("workshop_participants");
                });

            modelBuilder.Entity("Workshop.Service.API.Domain.Models.Criterion", b =>
                {
                    b.HasOne("Workshop.Service.API.Domain.Models.Workshop", "Workshop")
                        .WithMany("Criteria")
                        .HasForeignKey("WorkshopId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Workshop");
                });

            modelBuilder.Entity("workshop_participants", b =>
                {
                    b.HasOne("Workshop.Service.API.Domain.Models.Participant", null)
                        .WithMany()
                        .HasForeignKey("participant_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Workshop.Service.API.Domain.Models.Workshop", null)
                        .WithMany()
                        .HasForeignKey("workshop_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Workshop.Service.API.Domain.Models.Workshop", b =>
                {
                    b.Navigation("Criteria");
                });
#pragma warning restore 612, 618
        }
    }
}
