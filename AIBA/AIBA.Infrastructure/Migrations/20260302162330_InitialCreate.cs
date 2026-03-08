using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AIBA.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Analyses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Idea = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserStories = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UseCases = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FunctionalRequirements = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatabaseSchema = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApiSuggestions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Analyses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Analyses_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Analyses_ProjectId",
                table: "Analyses",
                column: "ProjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Analyses");

            migrationBuilder.DropTable(
                name: "Projects");
        }
    }
}
