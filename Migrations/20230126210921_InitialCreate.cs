using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Salubrity = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TasK",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdCategory = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PriorityTask = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TasK", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TasK_Category_IdCategory",
                        column: x => x.IdCategory,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "Description", "Name", "Salubrity" },
                values: new object[,]
                {
                    { new Guid("0a9fa564-0604-4dfa-88df-3636fe395651"), null, "Actividad recreativa", "sadudable y recomendable" },
                    { new Guid("7b5e9399-8e95-4ae8-8745-9542a01e2cc0"), null, "Asunto domesticos", "Insalubre" }
                });

            migrationBuilder.InsertData(
                table: "TasK",
                columns: new[] { "Id", "Date", "Description", "IdCategory", "PriorityTask", "Title" },
                values: new object[,]
                {
                    { new Guid("629f9587-abc8-4c85-859f-acb762b754ed"), new DateTime(2023, 1, 26, 18, 9, 20, 871, DateTimeKind.Local).AddTicks(2439), null, new Guid("0a9fa564-0604-4dfa-88df-3636fe395651"), "medium", "Practica con el arco" },
                    { new Guid("f5d327bf-be98-4786-81d5-0a2412b7807e"), new DateTime(2023, 1, 26, 18, 9, 20, 871, DateTimeKind.Local).AddTicks(2410), null, new Guid("7b5e9399-8e95-4ae8-8745-9542a01e2cc0"), "medium", "Limpiar Baño" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TasK_IdCategory",
                table: "TasK",
                column: "IdCategory");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TasK");

            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}
