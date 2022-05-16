﻿// <auto-generated />
using System;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using norming_planing_wpf_core;

#nullable disable

namespace norming_planing_wpf_core.Migrations
{
    [DbContext(typeof(AcszmkdbContext))]
    partial class AcszmkdbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresEnum(modelBuilder, "draft_status", new[] { "defining", "planning", "finished", "rejected" });
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("norming_planing_wpf_core.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Customers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Заказчик1"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Заказчик2"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Заказчик3"
                        });
                });

            modelBuilder.Entity("norming_planing_wpf_core.Detail", b =>
                {
                    b.Property<string>("Code")
                        .HasColumnType("text");

                    b.Property<string>("MarkCode")
                        .HasColumnType("text");

                    b.Property<int>("MarkDraftId")
                        .HasColumnType("integer");

                    b.Property<int>("HolesCount")
                        .HasColumnType("integer");

                    b.Property<int>("HolesDiamtr")
                        .HasColumnType("integer");

                    b.Property<double?>("MainLenght")
                        .HasColumnType("double precision");

                    b.Property<int>("MaterialId")
                        .HasColumnType("integer");

                    b.Property<long>("OppositeCount")
                        .HasColumnType("bigint");

                    b.Property<int>("SteelGradeId")
                        .HasColumnType("integer");

                    b.Property<long>("StraightCount")
                        .HasColumnType("bigint");

                    b.Property<long>("TotalCount")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("bigint")
                        .HasComputedColumnSql("\"StraightCount\" + \"OppositeCount\"", true);

                    b.Property<double>("TotalWeight")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("double precision")
                        .HasComputedColumnSql("(\"StraightCount\" + \"OppositeCount\") * \"Weight\"", true);

                    b.Property<double>("Weight")
                        .HasColumnType("double precision");

                    b.HasKey("Code", "MarkCode", "MarkDraftId");

                    b.HasIndex("MaterialId");

                    b.HasIndex("SteelGradeId");

                    b.HasIndex("MarkCode", "MarkDraftId");

                    b.ToTable("Details");
                });

            modelBuilder.Entity("norming_planing_wpf_core.Draft", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<int>("CustomerId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Deadline")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DraftStatus>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("draft_status")
                        .HasDefaultValue(DraftStatus.Defining);

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Drafts");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CustomerId = 1,
                            Deadline = new DateTime(1, 1, 1, 0, 1, 40, 0, DateTimeKind.Utc),
                            Name = "Свинокомлекс"
                        },
                        new
                        {
                            Id = 2,
                            CustomerId = 2,
                            Deadline = new DateTime(1, 1, 1, 0, 1, 40, 0, DateTimeKind.Utc),
                            Name = "РГС"
                        },
                        new
                        {
                            Id = 3,
                            CustomerId = 3,
                            Deadline = new DateTime(1, 1, 1, 0, 1, 40, 0, DateTimeKind.Utc),
                            Name = "Проект 3",
                            Status = DraftStatus.Planning
                        });
                });

            modelBuilder.Entity("norming_planing_wpf_core.Instrument", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Instruments");
                });

            modelBuilder.Entity("norming_planing_wpf_core.Mark", b =>
                {
                    b.Property<string>("Code")
                        .HasColumnType("text");

                    b.Property<int>("DraftId")
                        .HasColumnType("integer");

                    b.HasKey("Code", "DraftId");

                    b.HasIndex("DraftId");

                    b.ToTable("Marks");
                });

            modelBuilder.Entity("norming_planing_wpf_core.Material", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Material");
                });

            modelBuilder.Entity("norming_planing_wpf_core.NormingMap", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<double>("Coefficient")
                        .HasColumnType("double precision");

                    b.Property<int?>("InstrumentId")
                        .HasColumnType("integer");

                    b.Property<JsonDocument>("Parametrs")
                        .HasColumnType("jsonb");

                    b.Property<int?>("ParentMapId")
                        .HasColumnType("integer");

                    b.Property<int>("TOTypeId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("InstrumentId");

                    b.HasIndex("ParentMapId");

                    b.HasIndex("TOTypeId");

                    b.ToTable("NormingMaps");
                });

            modelBuilder.Entity("norming_planing_wpf_core.SteelGrade", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("SteelGrade");
                });

            modelBuilder.Entity("norming_planing_wpf_core.TO", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<double>("NormPrice")
                        .HasColumnType("double precision");

                    b.Property<double>("NormTime")
                        .HasColumnType("double precision");

                    b.Property<int?>("TPId")
                        .HasColumnType("integer");

                    b.Property<int?>("TypeId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("TPId");

                    b.HasIndex("TypeId");

                    b.ToTable("TOs");
                });

            modelBuilder.Entity("norming_planing_wpf_core.TOType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("TOTypes");
                });

            modelBuilder.Entity("norming_planing_wpf_core.TP", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("MarkCode")
                        .HasColumnType("text");

                    b.Property<int?>("MarkDraftId")
                        .HasColumnType("integer");

                    b.Property<int>("Order")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("MarkCode", "MarkDraftId");

                    b.ToTable("TPs");
                });

            modelBuilder.Entity("norming_planing_wpf_core.Detail", b =>
                {
                    b.HasOne("norming_planing_wpf_core.Material", "Material")
                        .WithMany()
                        .HasForeignKey("MaterialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("norming_planing_wpf_core.SteelGrade", "SteelGrade")
                        .WithMany()
                        .HasForeignKey("SteelGradeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("norming_planing_wpf_core.Mark", "Mark")
                        .WithMany("Details")
                        .HasForeignKey("MarkCode", "MarkDraftId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Mark");

                    b.Navigation("Material");

                    b.Navigation("SteelGrade");
                });

            modelBuilder.Entity("norming_planing_wpf_core.Draft", b =>
                {
                    b.HasOne("norming_planing_wpf_core.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("norming_planing_wpf_core.Mark", b =>
                {
                    b.HasOne("norming_planing_wpf_core.Draft", "Draft")
                        .WithMany("Marks")
                        .HasForeignKey("DraftId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Draft");
                });

            modelBuilder.Entity("norming_planing_wpf_core.NormingMap", b =>
                {
                    b.HasOne("norming_planing_wpf_core.Instrument", "Instrument")
                        .WithMany()
                        .HasForeignKey("InstrumentId");

                    b.HasOne("norming_planing_wpf_core.NormingMap", "ParentMap")
                        .WithMany()
                        .HasForeignKey("ParentMapId");

                    b.HasOne("norming_planing_wpf_core.TOType", "TOType")
                        .WithMany()
                        .HasForeignKey("TOTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Instrument");

                    b.Navigation("ParentMap");

                    b.Navigation("TOType");
                });

            modelBuilder.Entity("norming_planing_wpf_core.TO", b =>
                {
                    b.HasOne("norming_planing_wpf_core.TP", null)
                        .WithMany("Operations")
                        .HasForeignKey("TPId");

                    b.HasOne("norming_planing_wpf_core.TOType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("norming_planing_wpf_core.TP", b =>
                {
                    b.HasOne("norming_planing_wpf_core.Mark", null)
                        .WithMany("TechProcesses")
                        .HasForeignKey("MarkCode", "MarkDraftId");
                });

            modelBuilder.Entity("norming_planing_wpf_core.Draft", b =>
                {
                    b.Navigation("Marks");
                });

            modelBuilder.Entity("norming_planing_wpf_core.Mark", b =>
                {
                    b.Navigation("Details");

                    b.Navigation("TechProcesses");
                });

            modelBuilder.Entity("norming_planing_wpf_core.TP", b =>
                {
                    b.Navigation("Operations");
                });
#pragma warning restore 612, 618
        }
    }
}
