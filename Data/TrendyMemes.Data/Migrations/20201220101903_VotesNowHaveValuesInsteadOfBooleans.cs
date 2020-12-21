using Microsoft.EntityFrameworkCore.Migrations;

namespace TrendyMemes.Data.Migrations
{
    public partial class VotesNowHaveValuesInsteadOfBooleans : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsUpvote",
                table: "Votes");

            migrationBuilder.AddColumn<int>(
                name: "Value",
                table: "Votes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Value",
                table: "Votes");

            migrationBuilder.AddColumn<bool>(
                name: "IsUpvote",
                table: "Votes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
