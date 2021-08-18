﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Submission.Service.API.Persistence.Contexts;

namespace Submission.Service.API.Migrations
{
    [DbContext(typeof(SubmissionContext))]
    [Migration("20210712141053_AddWorkshopUidColumnToSubmissions")]
    partial class AddWorkshopUidColumnToSubmissions
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Submission.Service.API.Domain.Models.Submission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("author");

                    b.Property<string>("AuthorId")
                        .HasColumnType("text")
                        .HasColumnName("author_id");

                    b.Property<string>("Content")
                        .HasColumnType("text")
                        .HasColumnName("content");

                    b.Property<DateTimeOffset?>("Modified")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("modified");

                    b.Property<DateTimeOffset?>("Submitted")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("submitted");

                    b.Property<Guid>("WorkshopUid")
                        .HasColumnType("uuid")
                        .HasColumnName("workshop_uid");

                    b.HasKey("Id");

                    b.ToTable("submissions");
                });

            modelBuilder.Entity("Submission.Service.API.Domain.Models.SubmissionDeadlines", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("InstructorId")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("instructor_id");

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

                    b.ToTable("submission_deadlines");
                });
#pragma warning restore 612, 618
        }
    }
}
