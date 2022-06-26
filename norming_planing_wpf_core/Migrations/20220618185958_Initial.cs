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
                .Annotation("Npgsql:Enum:draft_status", "defining,planning,finished,rejected")
                .Annotation("Npgsql:Enum:entry_type", "argument,result")
                .Annotation("Npgsql:Enum:shift_task_status", "waiting,performed,complited,defected");

            migrationBuilder.CreateTable(
                name: "Assemblie",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    StraightCount = table.Column<long>(type: "bigint", nullable: false),
                    OppositeCount = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assemblie", x => x.Id);
                });

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
                name: "EmployeePositions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Position = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeePositions", x => x.Id);
                    table.UniqueConstraint("AK_EmployeePositions_Position", x => x.Position);
                });

            migrationBuilder.CreateTable(
                name: "MaterialTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    Structure = table.Column<JsonDocument>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SteelGrades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SteelGrades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TOTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ArgumentCount = table.Column<int>(type: "integer", nullable: false),
                    IncludedWorks = table.Column<string>(type: "jsonb", nullable: true),
                    ParamsTypes = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TOTypes", x => x.Id);
                    table.UniqueConstraint("AK_TOTypes_Name", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "UserCollections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCollections", x => x.Id);
                    table.UniqueConstraint("AK_UserCollections_Name", x => x.Name);
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
                    Priority = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
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
                name: "Employees",
                columns: table => new
                {
                    Passport = table.Column<string>(type: "text", nullable: false),
                    Fullname = table.Column<string>(type: "text", nullable: false),
                    PositionId = table.Column<int>(type: "integer", nullable: false),
                    Qualification = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Passport);
                    table.ForeignKey(
                        name: "FK_Employees_EmployeePositions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "EmployeePositions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Materials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Scalars = table.Column<JsonDocument>(type: "jsonb", nullable: false),
                    TypeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Materials_MaterialTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "MaterialTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeePositionTOType",
                columns: table => new
                {
                    EmployeePositionsId = table.Column<int>(type: "integer", nullable: false),
                    TOTypesId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeePositionTOType", x => new { x.EmployeePositionsId, x.TOTypesId });
                    table.ForeignKey(
                        name: "FK_EmployeePositionTOType_EmployeePositions_EmployeePositionsId",
                        column: x => x.EmployeePositionsId,
                        principalTable: "EmployeePositions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeePositionTOType_TOTypes_TOTypesId",
                        column: x => x.TOTypesId,
                        principalTable: "TOTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NormingMaps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    FirstArg = table.Column<string>(type: "jsonb", nullable: true),
                    SecondArg = table.Column<string>(type: "jsonb", nullable: true),
                    Norms = table.Column<string>(type: "jsonb", nullable: true),
                    ParentMapId = table.Column<int>(type: "integer", nullable: true),
                    Coefficient = table.Column<double>(type: "double precision", nullable: true),
                    OneHourPrice = table.Column<double>(type: "double precision", nullable: false),
                    TOTypeId = table.Column<int>(type: "integer", nullable: false),
                    MembersCvalification = table.Column<string>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NormingMaps", x => x.Id);
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
                name: "UserCollectionItem",
                columns: table => new
                {
                    Value = table.Column<string>(type: "text", nullable: false),
                    UserCollectionId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCollectionItem", x => new { x.Value, x.UserCollectionId });
                    table.ForeignKey(
                        name: "FK_UserCollectionItem_UserCollections_UserCollectionId",
                        column: x => x.UserCollectionId,
                        principalTable: "UserCollections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Marks",
                columns: table => new
                {
                    Code = table.Column<string>(type: "text", nullable: false),
                    DraftId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    StraightCount = table.Column<long>(type: "bigint", nullable: false),
                    OppositeCount = table.Column<long>(type: "bigint", nullable: false)
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
                    Weight = table.Column<double>(type: "double precision", nullable: false),
                    MainLenght = table.Column<double>(type: "double precision", nullable: true),
                    MaterialId = table.Column<int>(type: "integer", nullable: true),
                    SteelGradeId = table.Column<int>(type: "integer", nullable: true),
                    StraightCount = table.Column<long>(type: "bigint", nullable: false),
                    OppositeCount = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L)
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
                        name: "FK_Details_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Details_SteelGrades_SteelGradeId",
                        column: x => x.SteelGradeId,
                        principalTable: "SteelGrades",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TOs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    PreviousId = table.Column<int>(type: "integer", nullable: true),
                    NormTime = table.Column<double>(type: "double precision", nullable: false),
                    NormPrice = table.Column<double>(type: "double precision", nullable: false),
                    TypeParams = table.Column<JsonDocument>(type: "jsonb", nullable: true),
                    OperationCount = table.Column<long>(type: "bigint", nullable: false, defaultValue: 1L),
                    MarkCode = table.Column<string>(type: "text", nullable: false),
                    MarkDraftId = table.Column<int>(type: "integer", nullable: false),
                    TypeId = table.Column<int>(type: "integer", nullable: false),
                    NormingMapId = table.Column<int>(type: "integer", nullable: true),
                    MembersCvalification = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TOs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TOs_Marks_MarkCode_MarkDraftId",
                        columns: x => new { x.MarkCode, x.MarkDraftId },
                        principalTable: "Marks",
                        principalColumns: new[] { "Code", "DraftId" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TOs_NormingMaps_NormingMapId",
                        column: x => x.NormingMapId,
                        principalTable: "NormingMaps",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TOs_TOs_PreviousId",
                        column: x => x.PreviousId,
                        principalTable: "TOs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TOs_TOTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "TOTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssemblieEntry",
                columns: table => new
                {
                    AssemblieId = table.Column<int>(type: "integer", nullable: false),
                    TOId = table.Column<int>(type: "integer", nullable: false),
                    EntryType = table.Column<EntryType>(type: "entry_type", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssemblieEntry", x => new { x.TOId, x.AssemblieId });
                    table.ForeignKey(
                        name: "FK_AssemblieEntry_Assemblie_AssemblieId",
                        column: x => x.AssemblieId,
                        principalTable: "Assemblie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssemblieEntry_TOs_TOId",
                        column: x => x.TOId,
                        principalTable: "TOs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DetailTO",
                columns: table => new
                {
                    TOsId = table.Column<int>(type: "integer", nullable: false),
                    DetailsCode = table.Column<string>(type: "text", nullable: false),
                    DetailsMarkCode = table.Column<string>(type: "text", nullable: false),
                    DetailsMarkDraftId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetailTO", x => new { x.TOsId, x.DetailsCode, x.DetailsMarkCode, x.DetailsMarkDraftId });
                    table.ForeignKey(
                        name: "FK_DetailTO_Details_DetailsCode_DetailsMarkCode_DetailsMarkDra~",
                        columns: x => new { x.DetailsCode, x.DetailsMarkCode, x.DetailsMarkDraftId },
                        principalTable: "Details",
                        principalColumns: new[] { "Code", "MarkCode", "MarkDraftId" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetailTO_TOs_TOsId",
                        column: x => x.TOsId,
                        principalTable: "TOs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Instruments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    TOId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instruments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Instruments_TOs_TOId",
                        column: x => x.TOId,
                        principalTable: "TOs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ShiftTasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TOId = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<ShiftTaskStatus>(type: "shift_task_status", nullable: false),
                    NormTime = table.Column<double>(type: "double precision", nullable: true),
                    NormPrice = table.Column<double>(type: "double precision", nullable: true),
                    Issue = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShiftTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShiftTasks_TOs_TOId",
                        column: x => x.TOId,
                        principalTable: "TOs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InstrumentTOType",
                columns: table => new
                {
                    InstrumentsId = table.Column<int>(type: "integer", nullable: false),
                    TOTypesId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstrumentTOType", x => new { x.InstrumentsId, x.TOTypesId });
                    table.ForeignKey(
                        name: "FK_InstrumentTOType_Instruments_InstrumentsId",
                        column: x => x.InstrumentsId,
                        principalTable: "Instruments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InstrumentTOType_TOTypes_TOTypesId",
                        column: x => x.TOTypesId,
                        principalTable: "TOTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaskParticipation",
                columns: table => new
                {
                    EmployeeId = table.Column<string>(type: "text", nullable: false),
                    ShiftTaskId = table.Column<int>(type: "integer", nullable: false),
                    ParticipationPercentage = table.Column<double>(type: "double precision", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskParticipation", x => new { x.EmployeeId, x.ShiftTaskId });
                    table.ForeignKey(
                        name: "FK_TaskParticipation_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Passport",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskParticipation_ShiftTasks_ShiftTaskId",
                        column: x => x.ShiftTaskId,
                        principalTable: "ShiftTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                table: "Instruments",
                columns: new[] { "Id", "Name", "TOId" },
                values: new object[,]
                {
                    { 1, "Плазма", null },
                    { 2, "Сверлильный станок", null },
                    { 3, "Автоматическая сварка в аргонной стреде", null },
                    { 5, "Болгарка", null },
                    { 6, "Летночная пила", null },
                    { 7, "Плазменный станок", null }
                });

            migrationBuilder.InsertData(
                table: "MaterialTypes",
                columns: new[] { "Id", "Name", "Structure" },
                values: new object[,]
                {
                    { 1, "Лист", System.Text.Json.JsonDocument.Parse("[{\"Var\":\"a\",\"Name\":\"\\u0421\\u0442\\u043E\\u0440\\u043E\\u043D\\u0430 \\u0410\",\"Func\":null},{\"Var\":\"b\",\"Name\":\"\\u0421\\u0442\\u043E\\u0440\\u043E\\u043D\\u0430 \\u0411\",\"Func\":null},{\"Var\":\"\\u0441\",\"Name\":\"\\u0422\\u043E\\u043B\\u0449\\u0438\\u043D\\u0430\",\"Func\":null},{\"Var\":\"S\",\"Name\":\"\\u041F\\u043B\\u043E\\u0449\\u0430\\u0434\\u044C\",\"Func\":\"a*b\"}]", new System.Text.Json.JsonDocumentOptions()) },
                    { 2, "Круг", System.Text.Json.JsonDocument.Parse("[{\"Var\":\"d\",\"Name\":\"\\u0414\\u0438\\u0430\\u043C\\u0435\\u0442\\u0440 \\u043D\\u0430\\u0440\\u0443\\u0436\\u043D\\u044B\\u0439\",\"Func\":null},{\"Var\":\"S\",\"Name\":\"\\u041F\\u043B\\u043E\\u0449\\u0430\\u0434\\u044C \\u0441\\u0435\\u0447\\u0435\\u043D\\u0438\\u044F\",\"Func\":\"pi*(d/2)^2\"}]", new System.Text.Json.JsonDocumentOptions()) },
                    { 3, "Балка", System.Text.Json.JsonDocument.Parse("[{\"Var\":\"l\",\"Name\":\"\\u0412\\u044B\\u0441\\u043E\\u0442\\u0430\",\"Func\":null},{\"Var\":\"w\",\"Name\":\"\\u0428\\u0438\\u0440\\u0438\\u043D\\u0430\",\"Func\":null},{\"Var\":\"t\",\"Name\":\"\\u0422\\u043E\\u043B\\u0449\\u0438\\u043D\\u0430\",\"Func\":null}]", new System.Text.Json.JsonDocumentOptions()) },
                    { 4, "Уголок", System.Text.Json.JsonDocument.Parse("[{\"Var\":\"l\",\"Name\":\"\\u0412\\u044B\\u0441\\u043E\\u0442\\u0430\",\"Func\":null},{\"Var\":\"w\",\"Name\":\"\\u0428\\u0438\\u0440\\u0438\\u043D\\u0430\",\"Func\":null},{\"Var\":\"t\",\"Name\":\"\\u0422\\u043E\\u043B\\u0449\\u0438\\u043D\\u0430\",\"Func\":null}]", new System.Text.Json.JsonDocumentOptions()) }
                });

            migrationBuilder.InsertData(
                table: "SteelGrades",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "С345" },
                    { 2, "C35E " }
                });

            migrationBuilder.InsertData(
                table: "TOTypes",
                columns: new[] { "Id", "ArgumentCount", "IncludedWorks", "Name", "ParamsTypes" },
                values: new object[,]
                {
                    { 1, 0, null, "Резка", null },
                    { 2, 0, null, "Сверление", "[{\"Name\":\"\\u0414\\u0438\\u0430\\u043C\\u0435\\u0442\\u0440 \\u043E\\u0442\\u0432\\u0435\\u0440\\u0441\\u0442\\u0438\\u0439\",\"Type\":\"Double\",\"Identifyer\":null}]" },
                    { 3, 1, null, "Сборка", null },
                    { 4, 1, null, "Сварка", "[{\"Name\":\"\\u0422\\u0438\\u043F \\u0448\\u0432\\u0430\",\"Type\":\"UserCollection\",\"Identifyer\":1}]" },
                    { 5, 0, null, "Зачистка", null },
                    { 6, 0, "[\"\\u041E\\u0447\\u0438\\u0441\\u0442\\u043A\\u0430\",\"\\u041E\\u0431\\u0435\\u0437\\u0436\\u0438\\u0440\\u0438\\u0432\\u0430\\u043D\\u0438\\u0435\",\"\\u0413\\u0440\\u0443\\u043D\\u0442\\u043E\\u0432\\u0430\\u043D\\u0438\\u0435 \\u043F\\u0438\\u0441\\u0442\\u043E\\u043B\\u0435\\u0442\\u043E\\u043C \\u043D\\u0430 1 \\u0441\\u043B\\u043E\\u0439\",\"\\u041E\\u043A\\u0440\\u0430\\u0448\\u0438\\u0432\\u0430\\u043D\\u0438\\u0435 \\u043F\\u0438\\u0441\\u0442\\u043E\\u043B\\u0435\\u0442\\u043E\\u043C \\u043D\\u0430 1 \\u0441\\u043B\\u043E\\u0439\"]", "Окраска", null }
                });

            migrationBuilder.InsertData(
                table: "UserCollections",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Типы сварочного шва" });

            migrationBuilder.InsertData(
                table: "Drafts",
                columns: new[] { "Id", "CustomerId", "Deadline", "Name" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2022, 6, 18, 18, 59, 57, 531, DateTimeKind.Utc).AddTicks(8757), "Свинокомлекс" },
                    { 2, 2, new DateTime(2022, 6, 18, 18, 59, 57, 531, DateTimeKind.Utc).AddTicks(8764), "РГС" }
                });

            migrationBuilder.InsertData(
                table: "Drafts",
                columns: new[] { "Id", "CustomerId", "Deadline", "Name", "Status" },
                values: new object[] { 3, 3, new DateTime(2022, 6, 18, 18, 59, 57, 531, DateTimeKind.Utc).AddTicks(8765), "Проект 3", DraftStatus.Planning });

            migrationBuilder.InsertData(
                table: "Materials",
                columns: new[] { "Id", "Name", "Scalars", "TypeId" },
                values: new object[,]
                {
                    { 1, "Балка 10", System.Text.Json.JsonDocument.Parse("[{\"Var\":\"l\",\"Val\":3},{\"Var\":\"w\",\"Val\":2},{\"Var\":\"t\",\"Val\":0.001}]", new System.Text.Json.JsonDocumentOptions()), 3 },
                    { 2, "У40", System.Text.Json.JsonDocument.Parse("[{\"Var\":\"l\",\"Val\":3},{\"Var\":\"w\",\"Val\":2},{\"Var\":\"t\",\"Val\":0.001}]", new System.Text.Json.JsonDocumentOptions()), 4 },
                    { 3, "У 50", System.Text.Json.JsonDocument.Parse("[{\"Var\":\"l\",\"Val\":3},{\"Var\":\"w\",\"Val\":2},{\"Var\":\"t\",\"Val\":0.001}]", new System.Text.Json.JsonDocumentOptions()), 4 },
                    { 4, "У 56", System.Text.Json.JsonDocument.Parse("[{\"Var\":\"l\",\"Val\":3},{\"Var\":\"w\",\"Val\":2},{\"Var\":\"t\",\"Val\":0.001}]", new System.Text.Json.JsonDocumentOptions()), 4 },
                    { 5, "У 63", System.Text.Json.JsonDocument.Parse("[{\"Var\":\"l\",\"Val\":3},{\"Var\":\"w\",\"Val\":2},{\"Var\":\"t\",\"Val\":0.001}]", new System.Text.Json.JsonDocumentOptions()), 4 },
                    { 6, "У 70", System.Text.Json.JsonDocument.Parse("[{\"Var\":\"l\",\"Val\":3},{\"Var\":\"w\",\"Val\":2},{\"Var\":\"t\",\"Val\":0.001}]", new System.Text.Json.JsonDocumentOptions()), 4 },
                    { 7, "У 75", System.Text.Json.JsonDocument.Parse("[{\"Var\":\"l\",\"Val\":3},{\"Var\":\"w\",\"Val\":2},{\"Var\":\"t\",\"Val\":0.001}]", new System.Text.Json.JsonDocumentOptions()), 4 },
                    { 8, "У 80", System.Text.Json.JsonDocument.Parse("[{\"Var\":\"l\",\"Val\":3},{\"Var\":\"w\",\"Val\":2},{\"Var\":\"t\",\"Val\":0.001}]", new System.Text.Json.JsonDocumentOptions()), 4 },
                    { 9, "У 90", System.Text.Json.JsonDocument.Parse("[{\"Var\":\"l\",\"Val\":3},{\"Var\":\"w\",\"Val\":2},{\"Var\":\"t\",\"Val\":0.001}]", new System.Text.Json.JsonDocumentOptions()), 4 },
                    { 10, "У 100", System.Text.Json.JsonDocument.Parse("[{\"Var\":\"l\",\"Val\":3},{\"Var\":\"w\",\"Val\":2},{\"Var\":\"t\",\"Val\":0.001}]", new System.Text.Json.JsonDocumentOptions()), 4 },
                    { 11, "У 110", System.Text.Json.JsonDocument.Parse("[{\"Var\":\"l\",\"Val\":3},{\"Var\":\"w\",\"Val\":2},{\"Var\":\"t\",\"Val\":0.001}]", new System.Text.Json.JsonDocumentOptions()), 4 },
                    { 12, "-12х240", System.Text.Json.JsonDocument.Parse("[{\"Var\":\"a\",\"Val\":3},{\"Var\":\"b\",\"Val\":2},{\"Var\":\"c\",\"Val\":0.001},{\"Var\":\"S\",\"Val\":6}]", new System.Text.Json.JsonDocumentOptions()), 1 },
                    { 13, "-10х249", System.Text.Json.JsonDocument.Parse("[{\"Var\":\"a\",\"Val\":3},{\"Var\":\"b\",\"Val\":2},{\"Var\":\"c\",\"Val\":0.001},{\"Var\":\"S\",\"Val\":6}]", new System.Text.Json.JsonDocumentOptions()), 1 },
                    { 14, "-30х330", System.Text.Json.JsonDocument.Parse("[{\"Var\":\"a\",\"Val\":3},{\"Var\":\"b\",\"Val\":2},{\"Var\":\"c\",\"Val\":0.001},{\"Var\":\"S\",\"Val\":6}]", new System.Text.Json.JsonDocumentOptions()), 1 },
                    { 15, "Балка 12", System.Text.Json.JsonDocument.Parse("[{\"Var\":\"l\",\"Val\":3},{\"Var\":\"w\",\"Val\":2},{\"Var\":\"t\",\"Val\":0.001}]", new System.Text.Json.JsonDocumentOptions()), 3 },
                    { 16, "Балка 14", System.Text.Json.JsonDocument.Parse("[{\"Var\":\"l\",\"Val\":3},{\"Var\":\"w\",\"Val\":2},{\"Var\":\"t\",\"Val\":0.001}]", new System.Text.Json.JsonDocumentOptions()), 3 },
                    { 17, "Балка 16", System.Text.Json.JsonDocument.Parse("[{\"Var\":\"l\",\"Val\":3},{\"Var\":\"w\",\"Val\":2},{\"Var\":\"t\",\"Val\":0.001}]", new System.Text.Json.JsonDocumentOptions()), 3 },
                    { 18, "Балка 18", System.Text.Json.JsonDocument.Parse("[{\"Var\":\"l\",\"Val\":3},{\"Var\":\"w\",\"Val\":2},{\"Var\":\"t\",\"Val\":0.001}]", new System.Text.Json.JsonDocumentOptions()), 3 },
                    { 19, "Балка 20", System.Text.Json.JsonDocument.Parse("[{\"Var\":\"l\",\"Val\":3},{\"Var\":\"w\",\"Val\":2},{\"Var\":\"t\",\"Val\":0.001}]", new System.Text.Json.JsonDocumentOptions()), 3 },
                    { 20, "Балка 25", System.Text.Json.JsonDocument.Parse("[{\"Var\":\"l\",\"Val\":3},{\"Var\":\"w\",\"Val\":2},{\"Var\":\"t\",\"Val\":0.001}]", new System.Text.Json.JsonDocumentOptions()), 3 },
                    { 21, "Балка 30", System.Text.Json.JsonDocument.Parse("[{\"Var\":\"l\",\"Val\":3},{\"Var\":\"w\",\"Val\":2},{\"Var\":\"t\",\"Val\":0.001}]", new System.Text.Json.JsonDocumentOptions()), 3 },
                    { 22, "Балка 35", System.Text.Json.JsonDocument.Parse("[{\"Var\":\"l\",\"Val\":3},{\"Var\":\"w\",\"Val\":2},{\"Var\":\"t\",\"Val\":0.001}]", new System.Text.Json.JsonDocumentOptions()), 3 },
                    { 23, "Балка 40", System.Text.Json.JsonDocument.Parse("[{\"Var\":\"l\",\"Val\":3},{\"Var\":\"w\",\"Val\":2},{\"Var\":\"t\",\"Val\":0.001}]", new System.Text.Json.JsonDocumentOptions()), 3 },
                    { 24, "Балка 45", System.Text.Json.JsonDocument.Parse("[{\"Var\":\"l\",\"Val\":3},{\"Var\":\"w\",\"Val\":2},{\"Var\":\"t\",\"Val\":0.001}]", new System.Text.Json.JsonDocumentOptions()), 3 },
                    { 25, "Балка 50", System.Text.Json.JsonDocument.Parse("[{\"Var\":\"l\",\"Val\":3},{\"Var\":\"w\",\"Val\":2},{\"Var\":\"t\",\"Val\":0.001}]", new System.Text.Json.JsonDocumentOptions()), 3 },
                    { 26, "Балка 55", System.Text.Json.JsonDocument.Parse("[{\"Var\":\"l\",\"Val\":3},{\"Var\":\"w\",\"Val\":2},{\"Var\":\"t\",\"Val\":0.001}]", new System.Text.Json.JsonDocumentOptions()), 3 }
                });

            migrationBuilder.InsertData(
                table: "NormingMaps",
                columns: new[] { "Id", "Coefficient", "FirstArg", "MembersCvalification", "Name", "Norms", "OneHourPrice", "ParentMapId", "SecondArg", "TOTypeId" },
                values: new object[,]
                {
                    { 1, null, "{\"Path\":\"Material\",\"Name\":\"\\u0422\\u0438\\u043F \\u043C\\u0430\\u0442\\u0435\\u0440\\u0438\\u0430\\u043B\\u0430, \\u0411\\u0430\\u043B\\u043A\\u0430\",\"Range\":[\"\\u0411\\u0430\\u043B\\u043A\\u0430 12\",\"\\u0411\\u0430\\u043B\\u043A\\u0430 14\",\"\\u0411\\u0430\\u043B\\u043A\\u0430 16\",\"\\u0411\\u0430\\u043B\\u043A\\u0430 18\",\"\\u0411\\u0430\\u043B\\u043A\\u0430 20\",\"\\u0411\\u0430\\u043B\\u043A\\u0430 25\",\"\\u0411\\u0430\\u043B\\u043A\\u0430 30\",\"\\u0411\\u0430\\u043B\\u043A\\u0430 35\",\"\\u0411\\u0430\\u043B\\u043A\\u0430 40\",\"\\u0411\\u0430\\u043B\\u043A\\u0430 45\",\"\\u0411\\u0430\\u043B\\u043A\\u0430 50\",\"\\u0411\\u0430\\u043B\\u043A\\u0430 55\"]}", "[{\"Cvalification\":1,\"Count\":1}]", "Резка на ленточной пиле", "[[0.045,0.059,0.07,0.092,0.1,0.12,0.14,0.17,0.18,0.2,0.23,0.25]]", 200.0, null, null, 1 },
                    { 2, null, null, "[{\"Cvalification\":3,\"Count\":1}]", "Плазменнная резка", null, 150.0, null, null, 1 },
                    { 3, null, null, "[{\"Cvalification\":4,\"Count\":1}]", "Механизированная дуговая сварка в углекислом газе", null, 300.0, null, null, 4 },
                    { 4, null, null, "[{\"Cvalification\":1,\"Count\":1}]", "Зачистка кромок", null, 100.0, null, null, 5 },
                    { 5, null, null, "[{\"Cvalification\":1,\"Count\":1},{\"Cvalification\":3,\"Count\":1}]", "Окрашивание лакокрасочными составами", null, 350.0, null, null, 6 },
                    { 6, null, null, "[{\"Cvalification\":1,\"Count\":1}]", "Сверление отверстий на станке", null, 350.0, null, null, 2 },
                    { 7, null, "{\"Path\":\"TotalWeight\",\"Name\":\"\\u041C\\u0430\\u0441\\u0441\\u0430 \\u043A\\u043E\\u043D\\u0441\\u0442\\u0440\\u0443\\u043A\\u0446\\u0438\\u0438, \\u043A\\u0433\",\"Range\":[\"150\",\"200\",\"300\",\"400\",\"500\",\"800\",\"1000\"]}", "[{\"Cvalification\":1,\"Count\":4},{\"Cvalification\":2,\"Count\":3},{\"Cvalification\":1,\"Count\":2}]", "Сборка балок, ригелей и прогонов", "[[3.1,2.7,2.2,1.8,1.6,1,0.83],[3.7,3.1,2.6,2,1.8,1.3,0.93],[5.1,4.5,3.4,2.5,2.3,1.7,1.2],[7.2,6.2,4.7,3.3,3,2.1,1.5],[9.5,8.4,6.2,4.4,3.8,2.8,2],[12.5,10.5,7.7,5.5,4.7,3.3,2.4],[15,13,9.2,6.6,5.5,4,2.9]]", 350.0, null, "{\"Path\":\"AssemblieEntries.Count\",\"Name\":\"\\u041A\\u043E\\u043B\\u0438\\u0447\\u0435\\u0441\\u0442\\u0432\\u043E \\u0434\\u0435\\u0442\\u0430\\u043B\\u0435\\u0439 \\u0432 \\u043A\\u043E\\u043D\\u0441\\u0442\\u0440\\u0443\\u043A\\u0446\\u0438\\u0438\",\"Range\":[\"5\",\"7\",\"10\",\"15\",\"20\",\"30\"]}", 3 }
                });

            migrationBuilder.InsertData(
                table: "UserCollectionItem",
                columns: new[] { "UserCollectionId", "Value" },
                values: new object[,]
                {
                    { 1, "C17" },
                    { 1, "C2" },
                    { 1, "C7" },
                    { 1, "T1" }
                });

            migrationBuilder.InsertData(
                table: "Marks",
                columns: new[] { "Code", "DraftId", "Name", "OppositeCount", "StraightCount" },
                values: new object[,]
                {
                    { "М1", 1, "Балка1", 0L, 2L },
                    { "М1", 2, "Балка4", 0L, 2L },
                    { "М2", 1, "Балка2", 0L, 2L },
                    { "М2", 2, "Балка5", 0L, 2L },
                    { "М3", 1, "Балка3", 0L, 2L },
                    { "М3", 2, "Балка6", 0L, 2L }
                });

            migrationBuilder.InsertData(
                table: "Details",
                columns: new[] { "Code", "MarkCode", "MarkDraftId", "MainLenght", "MaterialId", "SteelGradeId", "StraightCount", "Weight" },
                values: new object[,]
                {
                    { "Деталь 1", "М1", 1, null, 1, 1, 2L, 20.0 },
                    { "Деталь 1", "М2", 1, null, 2, 1, 2L, 20.0 },
                    { "Деталь 2", "М1", 1, null, 1, 1, 2L, 20.0 },
                    { "Деталь 2", "М2", 1, null, 2, 1, 2L, 20.0 }
                });

            migrationBuilder.InsertData(
                table: "TOs",
                columns: new[] { "Id", "MarkCode", "MarkDraftId", "MembersCvalification", "Name", "NormPrice", "NormTime", "NormingMapId", "PreviousId", "TypeId", "TypeParams" },
                values: new object[] { 1, "М1", 1, null, "Сегмент кольца жёсткости", 20.100000000000001, 0.20000000000000001, null, null, 1, null });

            migrationBuilder.InsertData(
                table: "TOs",
                columns: new[] { "Id", "MarkCode", "MarkDraftId", "MembersCvalification", "Name", "NormPrice", "NormTime", "NormingMapId", "PreviousId", "TypeId", "TypeParams" },
                values: new object[] { 2, "М1", 1, null, "Пластина кольца жёсткости", 20.100000000000001, 0.20000000000000001, null, 1, 1, null });

            migrationBuilder.InsertData(
                table: "TOs",
                columns: new[] { "Id", "MarkCode", "MarkDraftId", "MembersCvalification", "Name", "NormPrice", "NormTime", "NormingMapId", "PreviousId", "TypeId", "TypeParams" },
                values: new object[] { 3, "М1", 1, null, "Элемент днища", 20.100000000000001, 0.20000000000000001, null, 2, 1, null });

            migrationBuilder.InsertData(
                table: "TOs",
                columns: new[] { "Id", "MarkCode", "MarkDraftId", "MembersCvalification", "Name", "NormPrice", "NormTime", "NormingMapId", "PreviousId", "TypeId", "TypeParams" },
                values: new object[] { 4, "М1", 1, null, "Элемент днища", 20.100000000000001, 0.20000000000000001, null, 3, 1, null });

            migrationBuilder.InsertData(
                table: "TOs",
                columns: new[] { "Id", "MarkCode", "MarkDraftId", "MembersCvalification", "Name", "NormPrice", "NormTime", "NormingMapId", "PreviousId", "TypeId", "TypeParams" },
                values: new object[] { 5, "М1", 1, null, "Элемент горловины (рыбка)", 20.100000000000001, 0.20000000000000001, null, 4, 1, null });

            migrationBuilder.InsertData(
                table: "TOs",
                columns: new[] { "Id", "MarkCode", "MarkDraftId", "MembersCvalification", "Name", "NormPrice", "NormTime", "NormingMapId", "PreviousId", "TypeId", "TypeParams" },
                values: new object[] { 6, "М1", 1, null, "Установка краевых рёбер жёсткости на карту", 20.100000000000001, 0.20000000000000001, null, 5, 3, null });

            migrationBuilder.InsertData(
                table: "TOs",
                columns: new[] { "Id", "MarkCode", "MarkDraftId", "MembersCvalification", "Name", "NormPrice", "NormTime", "NormingMapId", "PreviousId", "TypeId", "TypeParams" },
                values: new object[] { 7, "М1", 1, null, "Установка днищ и рёбер жёсткости на карту обечайки", 20.100000000000001, 0.20000000000000001, null, 6, 3, null });

            migrationBuilder.InsertData(
                table: "TOs",
                columns: new[] { "Id", "MarkCode", "MarkDraftId", "MembersCvalification", "Name", "NormPrice", "NormTime", "NormingMapId", "PreviousId", "TypeId", "TypeParams" },
                values: new object[] { 8, "М1", 1, null, "Установка монтажного прогона", 20.100000000000001, 0.20000000000000001, null, 7, 3, null });

            migrationBuilder.InsertData(
                table: "TOs",
                columns: new[] { "Id", "MarkCode", "MarkDraftId", "MembersCvalification", "Name", "NormPrice", "NormTime", "NormingMapId", "PreviousId", "TypeId", "TypeParams" },
                values: new object[] { 9, "М1", 1, null, "Установка каната лебёдки с роликовым  блоком", 20.100000000000001, 0.20000000000000001, null, 8, 3, null });

            migrationBuilder.CreateIndex(
                name: "IX_AssemblieEntry_AssemblieId",
                table: "AssemblieEntry",
                column: "AssemblieId");

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
                name: "IX_DetailTO_DetailsCode_DetailsMarkCode_DetailsMarkDraftId",
                table: "DetailTO",
                columns: new[] { "DetailsCode", "DetailsMarkCode", "DetailsMarkDraftId" });

            migrationBuilder.CreateIndex(
                name: "IX_Drafts_CustomerId",
                table: "Drafts",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePositionTOType_TOTypesId",
                table: "EmployeePositionTOType",
                column: "TOTypesId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_PositionId",
                table: "Employees",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Instruments_TOId",
                table: "Instruments",
                column: "TOId");

            migrationBuilder.CreateIndex(
                name: "IX_InstrumentTOType_TOTypesId",
                table: "InstrumentTOType",
                column: "TOTypesId");

            migrationBuilder.CreateIndex(
                name: "IX_Marks_DraftId",
                table: "Marks",
                column: "DraftId");

            migrationBuilder.CreateIndex(
                name: "IX_Materials_TypeId",
                table: "Materials",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_NormingMaps_ParentMapId",
                table: "NormingMaps",
                column: "ParentMapId");

            migrationBuilder.CreateIndex(
                name: "IX_NormingMaps_TOTypeId",
                table: "NormingMaps",
                column: "TOTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ShiftTasks_TOId",
                table: "ShiftTasks",
                column: "TOId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskParticipation_ShiftTaskId",
                table: "TaskParticipation",
                column: "ShiftTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_TOs_MarkCode_MarkDraftId",
                table: "TOs",
                columns: new[] { "MarkCode", "MarkDraftId" });

            migrationBuilder.CreateIndex(
                name: "IX_TOs_NormingMapId",
                table: "TOs",
                column: "NormingMapId");

            migrationBuilder.CreateIndex(
                name: "IX_TOs_PreviousId",
                table: "TOs",
                column: "PreviousId");

            migrationBuilder.CreateIndex(
                name: "IX_TOs_TypeId",
                table: "TOs",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCollectionItem_UserCollectionId",
                table: "UserCollectionItem",
                column: "UserCollectionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssemblieEntry");

            migrationBuilder.DropTable(
                name: "DetailTO");

            migrationBuilder.DropTable(
                name: "EmployeePositionTOType");

            migrationBuilder.DropTable(
                name: "InstrumentTOType");

            migrationBuilder.DropTable(
                name: "TaskParticipation");

            migrationBuilder.DropTable(
                name: "UserCollectionItem");

            migrationBuilder.DropTable(
                name: "Assemblie");

            migrationBuilder.DropTable(
                name: "Details");

            migrationBuilder.DropTable(
                name: "Instruments");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "ShiftTasks");

            migrationBuilder.DropTable(
                name: "UserCollections");

            migrationBuilder.DropTable(
                name: "Materials");

            migrationBuilder.DropTable(
                name: "SteelGrades");

            migrationBuilder.DropTable(
                name: "EmployeePositions");

            migrationBuilder.DropTable(
                name: "TOs");

            migrationBuilder.DropTable(
                name: "MaterialTypes");

            migrationBuilder.DropTable(
                name: "Marks");

            migrationBuilder.DropTable(
                name: "NormingMaps");

            migrationBuilder.DropTable(
                name: "Drafts");

            migrationBuilder.DropTable(
                name: "TOTypes");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
