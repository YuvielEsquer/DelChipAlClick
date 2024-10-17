using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiTallerDelChipAlClick.Migrations
{
    /// <inheritdoc />
    public partial class RoomsInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Rooms",
                table: "Leds",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rooms",
                table: "Leds");
        }
    }
}
