using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Report.Persistence.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReportLogs",
                columns: table => new
                {
                    UUID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StatusID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportLogs", x => x.UUID);
                });

            migrationBuilder.CreateTable(
                name: "ReportLogStatuses",
                columns: table => new
                {
                    UUID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportLogStatuses", x => x.UUID);
                });

            migrationBuilder.InsertData(
                table: "ReportLogStatuses",
                columns: new[] { "UUID", "Status" },
                values: new object[] { 1, "Talep alındı." });

            migrationBuilder.InsertData(
                table: "ReportLogStatuses",
                columns: new[] { "UUID", "Status" },
                values: new object[] { 2, "Hazırlanıyor." });

            migrationBuilder.InsertData(
                table: "ReportLogStatuses",
                columns: new[] { "UUID", "Status" },
                values: new object[] { 3, "Hazırlandı." });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReportLogs");

            migrationBuilder.DropTable(
                name: "ReportLogStatuses");
        }
    }
}
