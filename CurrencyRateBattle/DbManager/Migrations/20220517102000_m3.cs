using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations;

public partial class M3 : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        _ = migrationBuilder.CreateTable(
            name: "t_rooms",
            columns: table => new
            {
                RoomId = table.Column<string>(type: "text", nullable: false),
                Currency = table.Column<string>(type: "text", nullable: false),
                Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                Bets = table.Column<string>(type: "text", nullable: true, defaultValue: "")
            },
            constraints: table =>
            {
                _ = table.PrimaryKey("PK_t_rooms", x => x.RoomId);
            });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        _ = migrationBuilder.DropTable(
            name: "t_rooms");
    }
}
