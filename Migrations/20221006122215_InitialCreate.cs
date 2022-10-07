using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MailServiceApi.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MessageReports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MessageFrom = table.Column<string>(type: "text", nullable: true),
                    Subject = table.Column<string>(type: "text", nullable: true),
                    Result = table.Column<string>(type: "text", nullable: true),
                    FailedMessage = table.Column<string>(type: "text", nullable: true),
                    SendDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageReports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MessageBodies",
                columns: table => new
                {
                    MessageReportId = table.Column<Guid>(type: "uuid", nullable: false),
                    Body = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageBodies", x => x.MessageReportId);
                    table.ForeignKey(
                        name: "FK_MessageBodies_MessageReports_MessageReportId",
                        column: x => x.MessageReportId,
                        principalTable: "MessageReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MessageRecipients",
                columns: table => new
                {
                    MessageReportId = table.Column<Guid>(type: "uuid", nullable: false),
                    Recipient = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageRecipients", x => new { x.MessageReportId, x.Recipient });
                    table.ForeignKey(
                        name: "FK_MessageRecipients_MessageReports_MessageReportId",
                        column: x => x.MessageReportId,
                        principalTable: "MessageReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MessageBodies");

            migrationBuilder.DropTable(
                name: "MessageRecipients");

            migrationBuilder.DropTable(
                name: "MessageReports");
        }
    }
}
