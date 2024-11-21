using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITOS_LEARN.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTableLogins : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Logins",
                newName: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Logins",
                newName: "Id");
        }
    }
}
