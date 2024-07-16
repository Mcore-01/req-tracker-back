﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using req_tracker_back.Data;

#nullable disable

namespace req_tracker_back.Migrations
{
    [DbContext(typeof(RTContext))]
    [Migration("20240716144135_UpdateTableTicketAddField")]
    partial class UpdateTableTicketAddField
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("req_tracker_back.Models.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Groups");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Остальные"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Внешние"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Внутренние"
                        });
                });

            modelBuilder.Entity("req_tracker_back.Models.Status", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Status");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Создана"
                        },
                        new
                        {
                            Id = 2,
                            Name = "В работе"
                        },
                        new
                        {
                            Id = 3,
                            Name = "На проверке"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Завершена"
                        });
                });

            modelBuilder.Entity("req_tracker_back.Models.Ticket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Comment")
                        .HasColumnType("text");

                    b.Property<string>("Executor")
                        .HasColumnType("text");

                    b.Property<int>("GroupId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsLocked")
                        .HasColumnType("boolean");

                    b.Property<string>("Observer")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Result")
                        .HasColumnType("text");

                    b.Property<int>("StatusId")
                        .HasColumnType("integer");

                    b.Property<string>("Text")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("StatusId");

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("req_tracker_back.Models.Ticket", b =>
                {
                    b.HasOne("req_tracker_back.Models.Group", "Group")
                        .WithMany()
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("req_tracker_back.Models.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("Status");
                });
#pragma warning restore 612, 618
        }
    }
}
