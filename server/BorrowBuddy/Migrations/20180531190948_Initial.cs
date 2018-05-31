using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BorrowBuddy.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Code = table.Column<string>(maxLength: 3, nullable: false),
                    Symbol = table.Column<string>(maxLength: 10, nullable: true),
                    Scale = table.Column<int>(nullable: false, defaultValue: 100)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "Participants",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 36, nullable: false),
                    MiddleName = table.Column<string>(maxLength: 36, nullable: true),
                    LastName = table.Column<string>(maxLength: 36, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Flows",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    LenderId = table.Column<Guid>(nullable: false),
                    LendeeId = table.Column<Guid>(nullable: false),
                    Amount_Value = table.Column<long>(nullable: false),
                    Amount_CurrencyCode = table.Column<string>(nullable: true),
                    Comment = table.Column<string>(nullable: true),
                    Timestamp = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flows", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Flows_Participants_LendeeId",
                        column: x => x.LendeeId,
                        principalTable: "Participants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Flows_Participants_LenderId",
                        column: x => x.LenderId,
                        principalTable: "Participants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Flows_Currencies_Amount_CurrencyCode",
                        column: x => x.Amount_CurrencyCode,
                        principalTable: "Currencies",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Flows_LendeeId",
                table: "Flows",
                column: "LendeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Flows_LenderId",
                table: "Flows",
                column: "LenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Flows_Amount_CurrencyCode",
                table: "Flows",
                column: "Amount_CurrencyCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Flows");

            migrationBuilder.DropTable(
                name: "Participants");

            migrationBuilder.DropTable(
                name: "Currencies");
        }
    }
}
