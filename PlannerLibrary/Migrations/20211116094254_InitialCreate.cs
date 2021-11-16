using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PlannerLibrary.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblModule",
                columns: table => new
                {
                    module_id = table.Column<string>(type: "varchar(8)", unicode: false, maxLength: 8, nullable: false),
                    module_name = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: false),
                    module_credits = table.Column<int>(type: "int", nullable: false),
                    module_class_hours = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tblModul__1A2D06535FD6E50A", x => x.module_id);
                });

            migrationBuilder.CreateTable(
                name: "tblStudent",
                columns: table => new
                {
                    student_number = table.Column<int>(type: "int", nullable: false),
                    student_name = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: true),
                    student_surname = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: false),
                    student_email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    student_hash_password = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    start_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    number_of_weeks = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tblStude__0E749A78C500C332", x => x.student_number);
                });

            migrationBuilder.CreateTable(
                name: "tblStudentModule",
                columns: table => new
                {
                    student_module_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    student_number = table.Column<int>(type: "int", nullable: true),
                    module_id = table.Column<string>(type: "varchar(8)", unicode: false, maxLength: 8, nullable: true),
                    module_self_study_hour = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    study_hours_remains = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    study_reminder_day = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tblStude__EC62D1BAE16B185E", x => x.student_module_id);
                    table.ForeignKey(
                        name: "FK__tblStuden__modul__29572725",
                        column: x => x.module_id,
                        principalTable: "tblModule",
                        principalColumn: "module_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__tblStuden__stude__286302EC",
                        column: x => x.student_number,
                        principalTable: "tblStudent",
                        principalColumn: "student_number",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblTrackStudies",
                columns: table => new
                {
                    track_studies_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    hours_worked = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    date_worked = table.Column<DateTime>(type: "date", nullable: true),
                    week_number = table.Column<int>(type: "int", nullable: true),
                    student_number = table.Column<int>(type: "int", nullable: true),
                    module_id = table.Column<string>(type: "varchar(8)", unicode: false, maxLength: 8, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tblTrack__92A794307520D04F", x => x.track_studies_id);
                    table.ForeignKey(
                        name: "FK__tblTrackS__modul__2C3393D0",
                        column: x => x.module_id,
                        principalTable: "tblModule",
                        principalColumn: "module_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__tblTrackS__stude__2D27B809",
                        column: x => x.student_number,
                        principalTable: "tblStudent",
                        principalColumn: "student_number",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblStudentModule_module_id",
                table: "tblStudentModule",
                column: "module_id");

            migrationBuilder.CreateIndex(
                name: "IX_tblStudentModule_student_number",
                table: "tblStudentModule",
                column: "student_number");

            migrationBuilder.CreateIndex(
                name: "IX_tblTrackStudies_module_id",
                table: "tblTrackStudies",
                column: "module_id");

            migrationBuilder.CreateIndex(
                name: "IX_tblTrackStudies_student_number",
                table: "tblTrackStudies",
                column: "student_number");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblStudentModule");

            migrationBuilder.DropTable(
                name: "tblTrackStudies");

            migrationBuilder.DropTable(
                name: "tblModule");

            migrationBuilder.DropTable(
                name: "tblStudent");
        }
    }
}
