using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ItlaSocial.Data.Migrations
{
    public partial class AddFriendship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Friendships",
                columns: table => new
                {
                    ApplicationUser1Id = table.Column<string>(nullable: false),
                    ApplicationUser2Id = table.Column<string>(nullable: false),
                    Sent = table.Column<bool>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Friendships", x => new { x.ApplicationUser1Id, x.ApplicationUser2Id });
                    table.ForeignKey(
                        name: "FK_Friendships_AspNetUsers_ApplicationUser1Id",
                        column: x => x.ApplicationUser1Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Friendships_AspNetUsers_ApplicationUser2Id",
                        column: x => x.ApplicationUser2Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Friendships_ApplicationUser2Id",
                table: "Friendships",
                column: "ApplicationUser2Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Friendships");
        }
    }
}
