using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Astrum.Calendar.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RemoveFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccessRole",
                schema: "Calendar",
                table: "Calendars");

            migrationBuilder.DropColumn(
                name: "ForegroundColor",
                schema: "Calendar",
                table: "Calendars");

            migrationBuilder.AlterColumn<string>(
                name: "BackgroundColor",
                schema: "Calendar",
                table: "Calendars",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "BackgroundColor",
                schema: "Calendar",
                table: "Calendars",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "AccessRole",
                schema: "Calendar",
                table: "Calendars",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ForegroundColor",
                schema: "Calendar",
                table: "Calendars",
                type: "text",
                nullable: true);
        }
    }
}
