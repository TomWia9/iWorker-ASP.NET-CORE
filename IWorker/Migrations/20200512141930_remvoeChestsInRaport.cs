using Microsoft.EntityFrameworkCore.Migrations;

namespace IWorker.Migrations
{
    public partial class remvoeChestsInRaport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Chests",
                table: "Raports");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Chests",
                table: "Raports",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");
        }
    }
}
