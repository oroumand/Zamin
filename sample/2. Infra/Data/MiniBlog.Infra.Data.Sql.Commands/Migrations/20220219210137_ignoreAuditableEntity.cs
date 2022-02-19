using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniBlog.Infra.Data.Sql.Commands.Migrations
{
    public partial class ignoreAuditableEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IgnoreAuditableEntities",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BusinessId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IgnoreAuditableEntities", x => x.Id);
                    table.UniqueConstraint("AK_IgnoreAuditableEntities_BusinessId", x => x.BusinessId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IgnoreAuditableEntities");
        }
    }
}
