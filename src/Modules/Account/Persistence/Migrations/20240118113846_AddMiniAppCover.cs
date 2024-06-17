using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Astrum.Account.Migrations
{
    /// <inheritdoc />
    public partial class AddMiniAppCover : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CoverId",
                schema: "Account",
                table: "Miniapps",
                type: "uuid",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoverId",
                schema: "Account",
                table: "Miniapps");
        }
    }
}
