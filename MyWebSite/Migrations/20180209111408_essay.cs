using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MyWebSite.Migrations
{
    public partial class essay : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EssayTags_Essays_EssayID",
                table: "EssayTags");

            migrationBuilder.DropIndex(
                name: "IX_EssayTags_EssayID",
                table: "EssayTags");

            migrationBuilder.DropColumn(
                name: "EssayID",
                table: "EssayTags");

            migrationBuilder.CreateTable(
                name: "EssayTagAssignments",
                columns: table => new
                {
                    EssayID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EssayTagID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EssayTagAssignments", x => new { x.EssayID, x.EssayTagID });
                    table.ForeignKey(
                        name: "FK_EssayTagAssignments_Essays_EssayID",
                        column: x => x.EssayID,
                        principalTable: "Essays",
                        principalColumn: "EssayID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EssayTagAssignments_EssayTags_EssayTagID",
                        column: x => x.EssayTagID,
                        principalTable: "EssayTags",
                        principalColumn: "EssayTagID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EssayTagAssignments_EssayTagID",
                table: "EssayTagAssignments",
                column: "EssayTagID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EssayTagAssignments");

            migrationBuilder.AddColumn<string>(
                name: "EssayID",
                table: "EssayTags",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EssayTags_EssayID",
                table: "EssayTags",
                column: "EssayID");

            migrationBuilder.AddForeignKey(
                name: "FK_EssayTags_Essays_EssayID",
                table: "EssayTags",
                column: "EssayID",
                principalTable: "Essays",
                principalColumn: "EssayID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
