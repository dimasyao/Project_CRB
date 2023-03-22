using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations;

public partial class M6 : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        _ = migrationBuilder.CreateTable(
            name: "t_total_pLayed_rating",
            columns: table => new
            {
                Login = table.Column<string>(type: "text", nullable: false),
                Name = table.Column<string>(type: "text", nullable: true),
                Games = table.Column<int>(type: "integer", nullable: false)
            },
            constraints: table =>
            {
                _ = table.PrimaryKey("PK_t_total_pLayed_rating", x => x.Login);
            });

        _ = migrationBuilder.CreateTable(
            name: "t_total_victories_rating",
            columns: table => new
            {
                Login = table.Column<string>(type: "text", nullable: false),
                Name = table.Column<string>(type: "text", nullable: true),
                Games = table.Column<int>(type: "integer", nullable: false)
            },
            constraints: table =>
            {
                _ = table.PrimaryKey("PK_t_total_victories_rating", x => x.Login);
            });

        _ = migrationBuilder.CreateTable(
            name: "t_winrate_rating",
            columns: table => new
            {
                Login = table.Column<string>(type: "text", nullable: false),
                Name = table.Column<string>(type: "text", nullable: true),
                Games = table.Column<int>(type: "integer", nullable: false)
            },
            constraints: table =>
            {
                _ = table.PrimaryKey("PK_t_winrate_rating", x => x.Login);
            });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        _ = migrationBuilder.DropTable(
            name: "t_total_pLayed_rating");

        _ = migrationBuilder.DropTable(
            name: "t_total_victories_rating");

        _ = migrationBuilder.DropTable(
            name: "t_winrate_rating");
    }
}
