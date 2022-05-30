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
            NpgsqlModelBuilderExtensions.HasPostgresEnum(modelBuilder, "shift_task_status", new[] { "waiting", "performed", "complited", "defected" });
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("EmployeePositionTOType", b =>
                {
                    b.Property<int>("EmployeePositionsId")
                        .HasColumnType("integer");

                    b.Property<int>("TOTypesId")
                        .HasColumnType("integer");

                    b.HasKey("EmployeePositionsId", "TOTypesId");

                    b.HasIndex("TOTypesId");

                    b.ToTable("EmployeePositionTOType");
                });

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

                    b.Property<int?>("HolesCount")
                        .HasColumnType("integer");

                    b.Property<int?>("HolesDiamtr")
                        .HasColumnType("integer");

                    b.Property<double?>("MainLenght")
                        .HasColumnType("double precision");

                    b.Property<int?>("MaterialId")
                        .HasColumnType("integer");

                    b.Property<long>("OppositeCount")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasDefaultValueSql("0");

                    b.Property<int?>("SteelGradeId")
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

                    b.HasData(
                        new
                        {
                            Code = "Деталь 1",
                            MarkCode = "М1",
                            MarkDraftId = 1,
                            MaterialId = 1,
                            SteelGradeId = 1,
                            StraightCount = 2L,
                            Weight = 20.0
                        },
                        new
                        {
                            Code = "Деталь 2",
                            MarkCode = "М1",
                            MarkDraftId = 1,
                            MaterialId = 1,
                            SteelGradeId = 1,
                            StraightCount = 2L,
                            Weight = 20.0
                        },
                        new
                        {
                            Code = "Деталь 1",
                            MarkCode = "М2",
                            MarkDraftId = 1,
                            MaterialId = 2,
                            SteelGradeId = 1,
                            StraightCount = 2L,
                            Weight = 20.0
                        },
                        new
                        {
                            Code = "Деталь 2",
                            MarkCode = "М2",
                            MarkDraftId = 1,
                            MaterialId = 2,
                            SteelGradeId = 1,
                            StraightCount = 2L,
                            Weight = 20.0
                        });
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
                            Deadline = new DateTime(2022, 5, 30, 17, 59, 29, 37, DateTimeKind.Utc).AddTicks(4284),
                            Name = "Свинокомлекс"
                        },
                        new
                        {
                            Id = 2,
                            CustomerId = 2,
                            Deadline = new DateTime(2022, 5, 30, 17, 59, 29, 37, DateTimeKind.Utc).AddTicks(4288),
                            Name = "РГС"
                        },
                        new
                        {
                            Id = 3,
                            CustomerId = 3,
                            Deadline = new DateTime(2022, 5, 30, 17, 59, 29, 37, DateTimeKind.Utc).AddTicks(4289),
                            Name = "Проект 3",
                            Status = DraftStatus.Planning
                        });
                });

            modelBuilder.Entity("norming_planing_wpf_core.Employee", b =>
                {
                    b.Property<string>("Passport")
                        .HasColumnType("text");

                    b.Property<string>("Fullname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("PositionId")
                        .HasColumnType("integer");

                    b.Property<long>("Qualification")
                        .HasColumnType("bigint");

                    b.HasKey("Passport");

                    b.HasIndex("PositionId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("norming_planing_wpf_core.EmployeePosition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasAlternateKey("Position");

                    b.ToTable("EmployeePositions");
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

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("OppositeCount")
                        .HasColumnType("bigint");

                    b.Property<long>("StraightCount")
                        .HasColumnType("bigint");

                    b.Property<long>("TotalCount")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("bigint")
                        .HasComputedColumnSql("\"StraightCount\" + \"OppositeCount\"", true);

                    b.HasKey("Code", "DraftId");

                    b.HasIndex("DraftId");

                    b.ToTable("Marks");

                    b.HasData(
                        new
                        {
                            Code = "М1",
                            DraftId = 1,
                            Name = "Балка1",
                            OppositeCount = 0L,
                            StraightCount = 2L
                        },
                        new
                        {
                            Code = "М2",
                            DraftId = 1,
                            Name = "Балка2",
                            OppositeCount = 0L,
                            StraightCount = 2L
                        },
                        new
                        {
                            Code = "М3",
                            DraftId = 1,
                            Name = "Балка3",
                            OppositeCount = 0L,
                            StraightCount = 2L
                        },
                        new
                        {
                            Code = "М1",
                            DraftId = 2,
                            Name = "Балка4",
                            OppositeCount = 0L,
                            StraightCount = 2L
                        },
                        new
                        {
                            Code = "М2",
                            DraftId = 2,
                            Name = "Балка5",
                            OppositeCount = 0L,
                            StraightCount = 2L
                        },
                        new
                        {
                            Code = "М3",
                            DraftId = 2,
                            Name = "Балка6",
                            OppositeCount = 0L,
                            StraightCount = 2L
                        });
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

                    b.Property<JsonDocument>("Scalars")
                        .IsRequired()
                        .HasColumnType("jsonb");

                    b.Property<int>("TypeId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("TypeId");

                    b.ToTable("Materials");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Балка 35Ш1",
                            Scalars = System.Text.Json.JsonDocument.Parse("{\"l\":3, \"w\": 2, \"t\": 0.001}", new System.Text.Json.JsonDocumentOptions()),
                            TypeId = 3
                        },
                        new
                        {
                            Id = 2,
                            Name = "У 140х90х10",
                            Scalars = System.Text.Json.JsonDocument.Parse("{\"l\":3, \"w\": 2, \"t\": 0.001}", new System.Text.Json.JsonDocumentOptions()),
                            TypeId = 4
                        },
                        new
                        {
                            Id = 3,
                            Name = "-12х240",
                            Scalars = System.Text.Json.JsonDocument.Parse("{\"a\":3, \"b\": 2, \"c\": 0.001}", new System.Text.Json.JsonDocumentOptions()),
                            TypeId = 1
                        },
                        new
                        {
                            Id = 4,
                            Name = "-10х249",
                            Scalars = System.Text.Json.JsonDocument.Parse("{\"a\":3, \"b\": 2, \"c\": 0.001}", new System.Text.Json.JsonDocumentOptions()),
                            TypeId = 1
                        },
                        new
                        {
                            Id = 5,
                            Name = "-30х330",
                            Scalars = System.Text.Json.JsonDocument.Parse("{\"a\":3, \"b\": 2, \"c\": 0.001}", new System.Text.Json.JsonDocumentOptions()),
                            TypeId = 1
                        });
                });

            modelBuilder.Entity("norming_planing_wpf_core.MaterialType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)");

                    b.Property<JsonDocument>("Structure")
                        .IsRequired()
                        .HasColumnType("jsonb");

                    b.HasKey("Id");

                    b.ToTable("MaterialTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Лист",
                            Structure = System.Text.Json.JsonDocument.Parse("{\"Сторона А\":{\"var\":\"a\"}, \"Сторона Б\":{\"var\":\"b\"}, \"Толщина\":{\"var\":\"c\"}, \"Площадь\":{\"func\":\"a*b\", \"var\":\"S\"}}", new System.Text.Json.JsonDocumentOptions())
                        },
                        new
                        {
                            Id = 2,
                            Name = "Круг",
                            Structure = System.Text.Json.JsonDocument.Parse("{\"Диаметр наружный\":{\"var\":\"d\"}, \"Площадь сечения\":{\"func\":\"pi*(d/2)^2\",\"var\":\"S\"}}", new System.Text.Json.JsonDocumentOptions())
                        },
                        new
                        {
                            Id = 3,
                            Name = "Балка",
                            Structure = System.Text.Json.JsonDocument.Parse("{\"Высота\":{\"var\":\"l\"},\"Ширина\":{\"var\":\"w\"},\"Толщина\":{\"var\":\"t\"}}", new System.Text.Json.JsonDocumentOptions())
                        },
                        new
                        {
                            Id = 4,
                            Name = "Уголок",
                            Structure = System.Text.Json.JsonDocument.Parse("{\"Высота\":{\"var\":\"l\"},\"Ширина\":{\"var\":\"w\"},\"Толщина\":{\"var\":\"t\"}}", new System.Text.Json.JsonDocumentOptions())
                        });
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

            modelBuilder.Entity("norming_planing_wpf_core.ShiftTask", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("Issue")
                        .HasColumnType("timestamp with time zone");

                    b.Property<double?>("NormPrice")
                        .HasColumnType("double precision");

                    b.Property<double?>("NormTime")
                        .HasColumnType("double precision");

                    b.Property<ShiftTaskStatus>("Status")
                        .HasColumnType("shift_task_status");

                    b.Property<int>("TOId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("TOId");

                    b.ToTable("ShiftTasks");
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

                    b.ToTable("SteelGrades");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "С345"
                        },
                        new
                        {
                            Id = 2,
                            Name = "C35E "
                        });
                });

            modelBuilder.Entity("norming_planing_wpf_core.TaskParticipation", b =>
                {
                    b.Property<string>("EmployeeId")
                        .HasColumnType("text");

                    b.Property<int>("ShiftTaskId")
                        .HasColumnType("integer");

                    b.Property<double?>("ParticipationPercentage")
                        .HasColumnType("double precision");

                    b.HasKey("EmployeeId", "ShiftTaskId");

                    b.HasIndex("ShiftTaskId");

                    b.ToTable("TaskParticipation", (string)null);
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

                    b.HasAlternateKey("Name");

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

            modelBuilder.Entity("EmployeePositionTOType", b =>
                {
                    b.HasOne("norming_planing_wpf_core.EmployeePosition", null)
                        .WithMany()
                        .HasForeignKey("EmployeePositionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("norming_planing_wpf_core.TOType", null)
                        .WithMany()
                        .HasForeignKey("TOTypesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("norming_planing_wpf_core.Detail", b =>
                {
                    b.HasOne("norming_planing_wpf_core.Material", "Material")
                        .WithMany()
                        .HasForeignKey("MaterialId");

                    b.HasOne("norming_planing_wpf_core.SteelGrade", "SteelGrade")
                        .WithMany()
                        .HasForeignKey("SteelGradeId");

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

            modelBuilder.Entity("norming_planing_wpf_core.Employee", b =>
                {
                    b.HasOne("norming_planing_wpf_core.EmployeePosition", "Position")
                        .WithMany()
                        .HasForeignKey("PositionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Position");
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

            modelBuilder.Entity("norming_planing_wpf_core.Material", b =>
                {
                    b.HasOne("norming_planing_wpf_core.MaterialType", "Type")
                        .WithMany("Materials")
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Type");
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

            modelBuilder.Entity("norming_planing_wpf_core.ShiftTask", b =>
                {
                    b.HasOne("norming_planing_wpf_core.TO", "TO")
                        .WithMany()
                        .HasForeignKey("TOId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TO");
                });

            modelBuilder.Entity("norming_planing_wpf_core.TaskParticipation", b =>
                {
                    b.HasOne("norming_planing_wpf_core.Employee", "Employee")
                        .WithMany("Participations")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("norming_planing_wpf_core.ShiftTask", "ShiftTask")
                        .WithMany("Participations")
                        .HasForeignKey("ShiftTaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("ShiftTask");
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

            modelBuilder.Entity("norming_planing_wpf_core.Employee", b =>
                {
                    b.Navigation("Participations");
                });

            modelBuilder.Entity("norming_planing_wpf_core.Mark", b =>
                {
                    b.Navigation("Details");

                    b.Navigation("TechProcesses");
                });

            modelBuilder.Entity("norming_planing_wpf_core.MaterialType", b =>
                {
                    b.Navigation("Materials");
                });

            modelBuilder.Entity("norming_planing_wpf_core.ShiftTask", b =>
                {
                    b.Navigation("Participations");
                });

            modelBuilder.Entity("norming_planing_wpf_core.TP", b =>
                {
                    b.Navigation("Operations");
                });
#pragma warning restore 612, 618
        }
    }
}
