using Microsoft.EntityFrameworkCore.Migrations;

namespace NameBandit.Migrations
{
    public partial class PrefixesAndSuffixes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Prefix",
                table: "Names",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Suffix",
                table: "Names",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Prefix",
                table: "Names");

            migrationBuilder.DropColumn(
                name: "Suffix",
                table: "Names");
        }
    }
}
