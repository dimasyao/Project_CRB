using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations;

public partial class M9 : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        _ = migrationBuilder.AddColumn<string>(
            name: "Notification",
            table: "t_users",
            type: "text",
            nullable: true,
            defaultValue: "");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        _ = migrationBuilder.DropColumn(
            name: "Notification",
            table: "t_users");
    }
}
