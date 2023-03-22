using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations;

public partial class M1 : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        _ = migrationBuilder.CreateTable(
            name: "t_users",
            columns: table => new
            {
                Login = table.Column<string>(type: "text", nullable: false),
                Password = table.Column<string>(type: "text", nullable: false),
                Name = table.Column<string>(type: "text", nullable: false),
                Balance = table.Column<decimal>(type: "numeric", nullable: false, defaultValue: 100m),
                TotalGames = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                Victories = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                History = table.Column<string>(type: "text", nullable: true, defaultValue: "")
            },
            constraints: table =>
            {
                _ = table.PrimaryKey("PK_t_users", x => x.Login);
            });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        _ = migrationBuilder.DropTable(
            name: "t_users");
    }
}
