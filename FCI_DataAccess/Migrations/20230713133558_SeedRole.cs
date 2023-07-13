using FCI_DataAccess.Models.Seeding;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FCI_DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SeedRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                values: new object[] { AdminRole.Id, AdminRole.Name, AdminRole.NormalizedName, AdminRole.ConcurrencyStamp }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete From [Role]");
        }
    }

}
