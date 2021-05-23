﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Task.Service.API.Persistence.Contexts;

namespace Task.Service.API.Migrations
{
    [DbContext(typeof(TaskContext))]
    [Migration("20210523142929_CreateUniqueIndexOnUidInTaskTable")]
    partial class CreateUniqueIndexOnUidInTaskTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Task.Service.API.Domain.Models.Criterion", b =>
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

                    b.Property<int>("TaskId")
                        .HasColumnType("integer")
                        .HasColumnName("task_id");

                    b.HasKey("Id");

                    b.HasIndex("TaskId");

                    b.ToTable("criteria");
                });

            modelBuilder.Entity("Task.Service.API.Domain.Models.Task", b =>
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

                    b.Property<int>("InstructorId")
                        .HasColumnType("integer");

                    b.Property<DateTimeOffset?>("Modified")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("modified");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("name");

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

                    b.ToTable("tasks");
                });

            modelBuilder.Entity("Task.Service.API.Domain.Models.Criterion", b =>
                {
                    b.HasOne("Task.Service.API.Domain.Models.Task", "Task")
                        .WithMany("Criteria")
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Task");
                });

            modelBuilder.Entity("Task.Service.API.Domain.Models.Task", b =>
                {
                    b.Navigation("Criteria");
                });
#pragma warning restore 612, 618
        }
    }
}
