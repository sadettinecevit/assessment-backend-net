using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Contacts.Persistence.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContactInfos",
                columns: table => new
                {
                    UUID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContactId = table.Column<int>(type: "int", nullable: false),
                    InfoTypeId = table.Column<int>(type: "int", nullable: false),
                    Info = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactInfos", x => x.UUID);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    UUID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lastname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Company = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.UUID);
                });

            migrationBuilder.CreateTable(
                name: "InfoTypes",
                columns: table => new
                {
                    UUID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InfoTypes", x => x.UUID);
                });

            migrationBuilder.InsertData(
                table: "ContactInfos",
                columns: new[] { "UUID", "ContactId", "Info", "InfoTypeId" },
                values: new object[,]
                {
                    { 1, 1, "05xx xxx xx xx", 1 },
                    { 2, 1, "Amsterdam", 3 },
                    { 3, 2, "05xx xxx xx xx", 1 },
                    { 4, 3, "Amsterdam", 3 },
                    { 5, 3, "05xx xxx xx xx", 3 }
                });

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "UUID", "Company", "Lastname", "Name" },
                values: new object[,]
                {
                    { 1, "Test", "Ecevit", "Sadettin" },
                    { 2, "Test", "Aksu", "Sezen" },
                    { 3, "Test", "Senar", "Müzeyyen" }
                });

            migrationBuilder.InsertData(
                table: "InfoTypes",
                columns: new[] { "UUID", "Name" },
                values: new object[,]
                {
                    { 1, "Telefon Numarası" },
                    { 2, "E-mail Adresi" },
                    { 3, "Konum" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactInfos");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "InfoTypes");
        }
    }
}
