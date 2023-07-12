using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FCI_DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Updaterelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DepartmentSubject");

            migrationBuilder.AddColumn<int>(
                name: "SubjectId",
                table: "Departments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SubjectDepartments",
                columns: table => new
                {
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectDepartments", x => new { x.SubjectId, x.DepartmentId });
                    table.ForeignKey(
                        name: "FK_SubjectDepartments_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubjectDepartments_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Departments_SubjectId",
                table: "Departments",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectDepartments_DepartmentId",
                table: "SubjectDepartments",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Subjects_SubjectId",
                table: "Departments",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Subjects_SubjectId",
                table: "Departments");

            migrationBuilder.DropTable(
                name: "SubjectDepartments");

            migrationBuilder.DropIndex(
                name: "IX_Departments_SubjectId",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "Departments");

            migrationBuilder.CreateTable(
                name: "DepartmentSubject",
                columns: table => new
                {
                    DepartmentsId = table.Column<int>(type: "int", nullable: false),
                    SubjectsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentSubject", x => new { x.DepartmentsId, x.SubjectsId });
                    table.ForeignKey(
                        name: "FK_DepartmentSubject_Departments_DepartmentsId",
                        column: x => x.DepartmentsId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DepartmentSubject_Subjects_SubjectsId",
                        column: x => x.SubjectsId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentSubject_SubjectsId",
                table: "DepartmentSubject",
                column: "SubjectsId");
        }
    }
}
