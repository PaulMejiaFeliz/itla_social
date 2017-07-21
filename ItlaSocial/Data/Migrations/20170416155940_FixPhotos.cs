using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ItlaSocial.Data.Migrations
{
    public partial class FixPhotos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "ProfilePhotos");

            migrationBuilder.AddColumn<string>(
                name: "MediaUrl",
                table: "ProfilePhotos",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MediaUrl",
                table: "ProfilePhotos");

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "ProfilePhotos",
                nullable: true);
        }
    }
}
