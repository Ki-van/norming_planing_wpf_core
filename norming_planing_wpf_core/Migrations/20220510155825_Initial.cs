using System;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace norming_planing_wpf_core.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Deadline = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
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
                    Id = table.Column<string>(type: "text", nullable: false),
                    DraftId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marks", x => x.Id);
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
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    MarkId = table.Column<string>(type: "text", nullable: false),
                    HolesCount = table.Column<int>(type: "integer", nullable: false),
                    HolesDiamtr = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Details", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Details_Marks_MarkId",
                        column: x => x.MarkId,
                        principalTable: "Marks",
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
                    MarkId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TPs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TPs_Marks_MarkId",
                        column: x => x.MarkId,
                        principalTable: "Marks",
                        principalColumn: "Id");
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

            migrationBuilder.CreateIndex(
                name: "IX_Details_MarkId",
                table: "Details",
                column: "MarkId");

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
                name: "IX_TPs_MarkId",
                table: "TPs",
                column: "MarkId");
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
