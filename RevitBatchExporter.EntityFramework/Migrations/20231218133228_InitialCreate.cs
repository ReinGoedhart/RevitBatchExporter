using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RevitBatchExporter.EntityFramework.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Configurations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ConfigurationName = table.Column<string>(type: "TEXT", nullable: true),
                    RevitVersion = table.Column<int>(type: "INTEGER", nullable: false),
                    IsVisible = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Configurations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IsVisible = table.Column<bool>(type: "INTEGER", nullable: false),
                    ProjectName = table.Column<string>(type: "TEXT", nullable: false),
                    ModelGuid = table.Column<Guid>(type: "TEXT", nullable: true),
                    ProjectGuid = table.Column<Guid>(type: "TEXT", nullable: true),
                    LocalModelPath = table.Column<string>(type: "TEXT", nullable: true),
                    OutputName = table.Column<string>(type: "TEXT", nullable: true),
                    SaveAfterExport = table.Column<bool>(type: "INTEGER", nullable: false),
                    ViewName = table.Column<string>(type: "TEXT", nullable: true),
                    ConfigurationPath = table.Column<string>(type: "TEXT", nullable: true),
                    RevitVersion = table.Column<int>(type: "INTEGER", nullable: false),
                    RevitExportType = table.Column<int>(type: "INTEGER", nullable: false),
                    Region = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Configuration",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ConfigurationName = table.Column<string>(type: "TEXT", nullable: true),
                    RevitVersion = table.Column<int>(type: "INTEGER", nullable: false),
                    IsVisible = table.Column<bool>(type: "INTEGER", nullable: false),
                    ProjectDtoId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Configuration", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Configuration_Projects_ProjectDtoId",
                        column: x => x.ProjectDtoId,
                        principalTable: "Projects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LogFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ConfigurationsId = table.Column<int>(type: "INTEGER", nullable: false),
                    LogFilePath = table.Column<string>(type: "TEXT", nullable: true),
                    ErrorsOccured = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LogFiles_Configuration_ConfigurationsId",
                        column: x => x.ConfigurationsId,
                        principalTable: "Configuration",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IsVisible = table.Column<bool>(type: "INTEGER", nullable: false),
                    ProjectName = table.Column<string>(type: "TEXT", nullable: true),
                    ModelGuid = table.Column<Guid>(type: "TEXT", nullable: true),
                    ProjectGuid = table.Column<Guid>(type: "TEXT", nullable: true),
                    LocalModelPath = table.Column<string>(type: "TEXT", nullable: true),
                    OutputName = table.Column<string>(type: "TEXT", nullable: true),
                    SaveAfterExport = table.Column<bool>(type: "INTEGER", nullable: false),
                    ViewName = table.Column<string>(type: "TEXT", nullable: true),
                    ConfigurationPath = table.Column<string>(type: "TEXT", nullable: true),
                    RevitVersion = table.Column<int>(type: "INTEGER", nullable: false),
                    RevitExportType = table.Column<int>(type: "INTEGER", nullable: false),
                    Region = table.Column<int>(type: "INTEGER", nullable: false),
                    ConfigurationDtoId = table.Column<int>(type: "INTEGER", nullable: true),
                    LogFileDtoId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Project_Configurations_ConfigurationDtoId",
                        column: x => x.ConfigurationDtoId,
                        principalTable: "Configurations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Project_LogFiles_LogFileDtoId",
                        column: x => x.LogFileDtoId,
                        principalTable: "LogFiles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ConfigurationProject",
                columns: table => new
                {
                    ConfigurationsId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProjectsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigurationProject", x => new { x.ConfigurationsId, x.ProjectsId });
                    table.ForeignKey(
                        name: "FK_ConfigurationProject_Configuration_ConfigurationsId",
                        column: x => x.ConfigurationsId,
                        principalTable: "Configuration",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConfigurationProject_Project_ProjectsId",
                        column: x => x.ProjectsId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Configuration_ProjectDtoId",
                table: "Configuration",
                column: "ProjectDtoId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationProject_ProjectsId",
                table: "ConfigurationProject",
                column: "ProjectsId");

            migrationBuilder.CreateIndex(
                name: "IX_LogFiles_ConfigurationsId",
                table: "LogFiles",
                column: "ConfigurationsId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_ConfigurationDtoId",
                table: "Project",
                column: "ConfigurationDtoId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_LogFileDtoId",
                table: "Project",
                column: "LogFileDtoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConfigurationProject");

            migrationBuilder.DropTable(
                name: "Project");

            migrationBuilder.DropTable(
                name: "Configurations");

            migrationBuilder.DropTable(
                name: "LogFiles");

            migrationBuilder.DropTable(
                name: "Configuration");

            migrationBuilder.DropTable(
                name: "Projects");
        }
    }
}
