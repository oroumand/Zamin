using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Zamin.MiniBlog.Infra.Data.Sql.Commands.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OutBoxEventItems",
                columns: table => new
                {
                    OutBoxEventItemId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventId = table.Column<Guid>(nullable: false),
                    AccuredByUserId = table.Column<string>(maxLength: 255, nullable: true),
                    AccuredOn = table.Column<DateTime>(nullable: false),
                    AggregateName = table.Column<string>(maxLength: 255, nullable: true),
                    AggregateTypeName = table.Column<string>(maxLength: 500, nullable: true),
                    AggregateId = table.Column<string>(nullable: true),
                    EventName = table.Column<string>(maxLength: 255, nullable: true),
                    EventTypeName = table.Column<string>(maxLength: 500, nullable: true),
                    EventPayload = table.Column<string>(nullable: true),
                    IsProcessed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutBoxEventItems", x => x.OutBoxEventItemId);
                });

            migrationBuilder.CreateTable(
                name: "Writers",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusinessId = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    CreatedByUserId = table.Column<int>(nullable: true),
                    CreatedDateTime = table.Column<DateTime>(nullable: true),
                    ModifiedByUserId = table.Column<int>(nullable: true),
                    ModifiedDateTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Writers", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OutBoxEventItems");

            migrationBuilder.DropTable(
                name: "Writers");
        }
    }
}
