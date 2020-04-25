using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IWorker.Migrations
{
    public partial class addMessages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    From = table.Column<int>(maxLength: 5, nullable: false),
                    To = table.Column<int>(maxLength: 5, nullable: false),
                    MessageText = table.Column<string>(maxLength: 300, nullable: false),
                    Date = table.Column<DateTime>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Messages");
        }
    }
}
