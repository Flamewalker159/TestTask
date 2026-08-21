using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TestTask.Migrations
{
    /// <inheritdoc />
    public partial class AddFileImportAndResult : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FileImportId",
                table: "Values",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "FileImports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FileName = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileImports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Results",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FileImportId = table.Column<int>(type: "integer", nullable: false),
                    TimeDelta = table.Column<double>(type: "double precision", nullable: false),
                    StartDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    AverageExecutionTime = table.Column<double>(type: "double precision", nullable: false),
                    AverageValue = table.Column<double>(type: "double precision", nullable: false),
                    MedianValue = table.Column<double>(type: "double precision", nullable: false),
                    MaxValue = table.Column<double>(type: "double precision", nullable: false),
                    MinValue = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Results", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Results_FileImports_FileImportId",
                        column: x => x.FileImportId,
                        principalTable: "FileImports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Values_FileImportId",
                table: "Values",
                column: "FileImportId");

            migrationBuilder.CreateIndex(
                name: "IX_Results_FileImportId",
                table: "Results",
                column: "FileImportId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Values_FileImports_FileImportId",
                table: "Values",
                column: "FileImportId",
                principalTable: "FileImports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Values_FileImports_FileImportId",
                table: "Values");

            migrationBuilder.DropTable(
                name: "Results");

            migrationBuilder.DropTable(
                name: "FileImports");

            migrationBuilder.DropIndex(
                name: "IX_Values_FileImportId",
                table: "Values");

            migrationBuilder.DropColumn(
                name: "FileImportId",
                table: "Values");
        }
    }
}
