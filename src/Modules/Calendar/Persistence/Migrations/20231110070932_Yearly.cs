using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Astrum.Calendar.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Yearly : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Yearly",
                schema: "Calendar",
                table: "Events",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Yearly",
                schema: "Calendar",
                table: "Events");
        }
    }
}
