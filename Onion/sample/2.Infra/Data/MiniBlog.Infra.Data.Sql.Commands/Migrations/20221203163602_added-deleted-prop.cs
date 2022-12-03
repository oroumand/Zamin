using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniBlog.Infra.Data.Sql.Commands.Migrations
{
    public partial class addeddeletedprop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Blogs",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Blogs");
        }
    }
}
