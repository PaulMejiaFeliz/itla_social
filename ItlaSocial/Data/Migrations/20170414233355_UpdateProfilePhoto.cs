using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ItlaSocial.Data.Migrations
{
    public partial class UpdateProfilePhoto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AplicationUserId",
                table: "ProfilePhotos",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "AplicationUserId",
                table: "ProfilePhotos",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
