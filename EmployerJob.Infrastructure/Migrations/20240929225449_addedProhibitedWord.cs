using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EmployerJob.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addedProhibitedWord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProhibitedWords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Word = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProhibitedWords", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ExpirationDate", "PostedDate" },
                values: new object[] { new DateTime(2024, 10, 29, 22, 54, 48, 499, DateTimeKind.Utc).AddTicks(7863), new DateTime(2024, 9, 29, 22, 54, 48, 499, DateTimeKind.Utc).AddTicks(7860) });

            migrationBuilder.InsertData(
                table: "ProhibitedWords",
                columns: new[] { "Id", "Word" },
                values: new object[,]
                {
                    { 1, "sakıncalı1" },
                    { 2, "sakıncalı2" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProhibitedWords");

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ExpirationDate", "PostedDate" },
                values: new object[] { new DateTime(2024, 10, 29, 7, 42, 43, 964, DateTimeKind.Utc).AddTicks(2485), new DateTime(2024, 9, 29, 7, 42, 43, 964, DateTimeKind.Utc).AddTicks(2481) });
        }
    }
}
