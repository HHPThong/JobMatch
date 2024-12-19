using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FPTJobMatch.Migrations
{
    /// <inheritdoc />
    public partial class table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_apps_status_StatusID",
                table: "apps");

            migrationBuilder.DropForeignKey(
                name: "FK_jobs_timeWork_TimeWorkID",
                table: "jobs");

            migrationBuilder.DropTable(
                name: "status");

            migrationBuilder.DropTable(
                name: "timeWork");

            migrationBuilder.DropIndex(
                name: "IX_apps_StatusID",
                table: "apps");

            migrationBuilder.DeleteData(
                table: "apps",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "apps",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "jobs",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "jobs",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "CV",
                table: "apps");

            migrationBuilder.DropColumn(
                name: "StatusID",
                table: "apps");

            migrationBuilder.RenameColumn(
                name: "TimeWorkID",
                table: "jobs",
                newName: "CategoryId");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "jobs",
                newName: "requiredQualification");

            migrationBuilder.RenameColumn(
                name: "Company",
                table: "jobs",
                newName: "Title");

            migrationBuilder.RenameIndex(
                name: "IX_jobs_TimeWorkID",
                table: "jobs",
                newName: "IX_jobs_CategoryId");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "apps",
                newName: "Email");

            migrationBuilder.AddColumn<DateTime>(
                name: "Deadline",
                table: "jobs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "jobs",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CV",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Introduction",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "AspNetUsers",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DayApply",
                table: "apps",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "apps",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Availability = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NotificationStatus = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_jobs_UserId",
                table: "jobs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_UserId",
                table: "Categories",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_jobs_AspNetUsers_UserId",
                table: "jobs",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_jobs_Categories_CategoryId",
                table: "jobs",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_jobs_AspNetUsers_UserId",
                table: "jobs");

            migrationBuilder.DropForeignKey(
                name: "FK_jobs_Categories_CategoryId",
                table: "jobs");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_jobs_UserId",
                table: "jobs");

            migrationBuilder.DropColumn(
                name: "Deadline",
                table: "jobs");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "jobs");

            migrationBuilder.DropColumn(
                name: "CV",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Introduction",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DayApply",
                table: "apps");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "apps");

            migrationBuilder.RenameColumn(
                name: "requiredQualification",
                table: "jobs",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "jobs",
                newName: "Company");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "jobs",
                newName: "TimeWorkID");

            migrationBuilder.RenameIndex(
                name: "IX_jobs_CategoryId",
                table: "jobs",
                newName: "IX_jobs_TimeWorkID");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "apps",
                newName: "Description");

            migrationBuilder.AddColumn<string>(
                name: "CV",
                table: "apps",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StatusID",
                table: "apps",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "status",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameStatus = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_status", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "timeWork",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_timeWork", x => x.ID);
                });

            migrationBuilder.InsertData(
                table: "status",
                columns: new[] { "Id", "NameStatus" },
                values: new object[,]
                {
                    { 1, "Pending" },
                    { 2, "Accepted" },
                    { 3, "Rejected" }
                });

            migrationBuilder.InsertData(
                table: "timeWork",
                columns: new[] { "ID", "Type" },
                values: new object[,]
                {
                    { 1, "Full Time" },
                    { 2, "Part Time" }
                });

            migrationBuilder.InsertData(
                table: "jobs",
                columns: new[] { "ID", "Company", "Description", "Name", "Request", "Salary", "TimeWorkID" },
                values: new object[,]
                {
                    { 1, "FPT Company", "introduction", "Software Engineer", "introduction", 40000.0, 1 },
                    { 2, "FPT Company", "introduction", "Data Scientist", "introduction", 50000.0, 2 }
                });

            migrationBuilder.InsertData(
                table: "apps",
                columns: new[] { "Id", "CV", "Description", "JobID", "StatusID" },
                values: new object[,]
                {
                    { 1, null, "introduction", 1, 1 },
                    { 2, null, "introduction", 2, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_apps_StatusID",
                table: "apps",
                column: "StatusID");

            migrationBuilder.AddForeignKey(
                name: "FK_apps_status_StatusID",
                table: "apps",
                column: "StatusID",
                principalTable: "status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_jobs_timeWork_TimeWorkID",
                table: "jobs",
                column: "TimeWorkID",
                principalTable: "timeWork",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
