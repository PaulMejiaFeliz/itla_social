using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ItlaSocial.Data.Migrations
{
    public partial class FixPhoto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AplicationUserId",
                table: "ProfilePhotos");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AplicationUserId",
                table: "ProfilePhotos",
                nullable: true);
        }
    }
}
