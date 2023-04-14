using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EF_Core.Migrations
{
    /// <inheritdoc />
    public partial class Migration1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CprNumber",
                table: "Customers",
                newName: "AUID"
                );

            migrationBuilder.RenameColumn(
                name: "CprNumber",
                table: "Ratings",
                newName: "AUID"
                );

            migrationBuilder.RenameColumn(
                name: "CprNumber",
                table: "Reservations",
                newName: "AUID"
                );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
