using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations;

public partial class M7 : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        _ = migrationBuilder.AlterColumn<string>(
            name: "Name",
            table: "t_winrate_rating",
            type: "text",
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "text",
            oldNullable: true);

        _ = migrationBuilder.AlterColumn<string>(
            name: "Name",
            table: "t_total_victories_rating",
            type: "text",
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "text",
            oldNullable: true);

        _ = migrationBuilder.AlterColumn<string>(
            name: "Name",
            table: "t_total_pLayed_rating",
            type: "text",
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "text",
            oldNullable: true);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        _ = migrationBuilder.AlterColumn<string>(
            name: "Name",
            table: "t_winrate_rating",
            type: "text",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "text");

        _ = migrationBuilder.AlterColumn<string>(
            name: "Name",
            table: "t_total_victories_rating",
            type: "text",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "text");

        _ = migrationBuilder.AlterColumn<string>(
            name: "Name",
            table: "t_total_pLayed_rating",
            type: "text",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "text");
    }
}
