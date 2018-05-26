﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace BorrowBuddy.Migrations
{
    public partial class CurrencyPrecision : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Precision",
                table: "Currencies",
                nullable: false,
                defaultValue: 1);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Precision",
                table: "Currencies");
        }
    }
}
