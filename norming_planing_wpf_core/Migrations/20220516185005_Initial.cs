using System;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using norming_planing_wpf_core;

#nullable disable

namespace norming_planing_wpf_core.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:draft_status", "defining,planning,finished,rejected");

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Instruments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instruments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Material",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Material", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SteelGrade",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SteelGrade", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TOTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TOTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Drafts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    Deadline = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<DraftStatus>(type: "draft_status", nullable: false, defaultValue: DraftStatus.Defining),
                    CustomerId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drafts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Drafts_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NormingMaps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Parametrs = table.Column<JsonDocument>(type: "jsonb", nullable: true),
                    InstrumentId = table.Column<int>(type: "integer", nullable: true),
                    ParentMapId = table.Column<int>(type: "integer", nullable: true),
                    Coefficient = table.Column<double>(type: "double precision", nullable: false),
                    TOTypeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NormingMaps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NormingMaps_Instruments_InstrumentId",
                        column: x => x.InstrumentId,
                        principalTable: "Instruments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NormingMaps_NormingMaps_ParentMapId",
                        column: x => x.ParentMapId,
                        principalTable: "NormingMaps",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NormingMaps_TOTypes_TOTypeId",
                        column: x => x.TOTypeId,
                        principalTable: "TOTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Marks",
                columns: table => new
                {
                    Code = table.Column<string>(type: "text", nullable: false),
                    DraftId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marks", x => new { x.Code, x.DraftId });
                    table.ForeignKey(
                        name: "FK_Marks_Drafts_DraftId",
                        column: x => x.DraftId,
                        principalTable: "Drafts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Details",
                columns: table => new
                {
                    Code = table.Column<string>(type: "text", nullable: false),
                    MarkCode = table.Column<string>(type: "text", nullable: false),
                    MarkDraftId = table.Column<int>(type: "integer", nullable: false),
                    StraightCount = table.Column<long>(type: "bigint", nullable: false),
                    OppositeCount = table.Column<long>(type: "bigint", nullable: false),
                    TotalCount = table.Column<long>(type: "bigint", nullable: false, computedColumnSql: "\"StraightCount\" + \"OppositeCount\"", stored: true),
                    Weight = table.Column<double>(type: "double precision", nullable: false),
                    TotalWeight = table.Column<double>(type: "double precision", nullable: false, computedColumnSql: "(\"StraightCount\" + \"OppositeCount\") * \"Weight\"", stored: true),
                    MainLenght = table.Column<double>(type: "double precision", nullable: true),
                    HolesCount = table.Column<int>(type: "integer", nullable: false),
                    HolesDiamtr = table.Column<int>(type: "integer", nullable: false),
                    MaterialId = table.Column<int>(type: "integer", nullable: false),
                    SteelGradeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Details", x => new { x.Code, x.MarkCode, x.MarkDraftId });
                    table.ForeignKey(
                        name: "FK_Details_Marks_MarkCode_MarkDraftId",
                        columns: x => new { x.MarkCode, x.MarkDraftId },
                        principalTable: "Marks",
                        principalColumns: new[] { "Code", "DraftId" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Details_Material_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Material",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Details_SteelGrade_SteelGradeId",
                        column: x => x.SteelGradeId,
                        principalTable: "SteelGrade",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TPs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    MarkCode = table.Column<string>(type: "text", nullable: true),
                    MarkDraftId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TPs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TPs_Marks_MarkCode_MarkDraftId",
                        columns: x => new { x.MarkCode, x.MarkDraftId },
                        principalTable: "Marks",
                        principalColumns: new[] { "Code", "DraftId" });
                });

            migrationBuilder.CreateTable(
                name: "TOs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NormTime = table.Column<double>(type: "double precision", nullable: false),
                    NormPrice = table.Column<double>(type: "double precision", nullable: false),
                    TypeId = table.Column<int>(type: "integer", nullable: true),
                    TPId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TOs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TOs_TOTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "TOTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TOs_TPs_TPId",
                        column: x => x.TPId,
                        principalTable: "TPs",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Заказчик1" },
                    { 2, "Заказчик2" },
                    { 3, "Заказчик3" }
                });

            migrationBuilder.InsertData(
                table: "Drafts",
                columns: new[] { "Id", "CustomerId", "Deadline", "Name" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(1, 1, 1, 0, 1, 40, 0, DateTimeKind.Utc), "Свинокомлекс" },
                    { 2, 2, new DateTime(1, 1, 1, 0, 1, 40, 0, DateTimeKind.Utc), "РГС" }
                });

            migrationBuilder.InsertData(
                table: "Drafts",
                columns: new[] { "Id", "CustomerId", "Deadline", "Name", "Status" },
                values: new object[] { 3, 3, new DateTime(1, 1, 1, 0, 1, 40, 0, DateTimeKind.Utc), "Проект 3", DraftStatus.Planning });

            migrationBuilder.CreateIndex(
                name: "IX_Details_MarkCode_MarkDraftId",
                table: "Details",
                columns: new[] { "MarkCode", "MarkDraftId" });

            migrationBuilder.CreateIndex(
                name: "IX_Details_MaterialId",
                table: "Details",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_Details_SteelGradeId",
                table: "Details",
                column: "SteelGradeId");

            migrationBuilder.CreateIndex(
                name: "IX_Drafts_CustomerId",
                table: "Drafts",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Marks_DraftId",
                table: "Marks",
                column: "DraftId");

            migrationBuilder.CreateIndex(
                name: "IX_NormingMaps_InstrumentId",
                table: "NormingMaps",
                column: "InstrumentId");

            migrationBuilder.CreateIndex(
                name: "IX_NormingMaps_ParentMapId",
                table: "NormingMaps",
                column: "ParentMapId");

            migrationBuilder.CreateIndex(
                name: "IX_NormingMaps_TOTypeId",
                table: "NormingMaps",
                column: "TOTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TOs_TPId",
                table: "TOs",
                column: "TPId");

            migrationBuilder.CreateIndex(
                name: "IX_TOs_TypeId",
                table: "TOs",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TPs_MarkCode_MarkDraftId",
                table: "TPs",
                columns: new[] { "MarkCode", "MarkDraftId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Details");

            migrationBuilder.DropTable(
                name: "NormingMaps");

            migrationBuilder.DropTable(
                name: "TOs");

            migrationBuilder.DropTable(
                name: "Material");

            migrationBuilder.DropTable(
                name: "SteelGrade");

            migrationBuilder.DropTable(
                name: "Instruments");

            migrationBuilder.DropTable(
                name: "TOTypes");

            migrationBuilder.DropTable(
                name: "TPs");

            migrationBuilder.DropTable(
                name: "Marks");

            migrationBuilder.DropTable(
                name: "Drafts");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
