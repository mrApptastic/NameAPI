using Microsoft.EntityFrameworkCore.Migrations;

namespace NameBandit.Migrations
{
    public partial class Vibration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Vibration",
                table: "Names",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Vibration",
                table: "Names");
        }
    }
}
