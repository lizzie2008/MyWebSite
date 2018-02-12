using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MyWebSite.Migrations
{
    public partial class init1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Essays_EssayArchives_EssayArchiveID",
                table: "Essays");

            migrationBuilder.DropForeignKey(
                name: "FK_Essays_EssayCatalogs_EssayCatalogID",
                table: "Essays");

            migrationBuilder.AlterColumn<int>(
                name: "EssayCatalogID",
                table: "Essays",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "EssayArchiveID",
                table: "Essays",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Essays_EssayArchives_EssayArchiveID",
                table: "Essays",
                column: "EssayArchiveID",
                principalTable: "EssayArchives",
                principalColumn: "EssayArchiveID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Essays_EssayCatalogs_EssayCatalogID",
                table: "Essays",
                column: "EssayCatalogID",
                principalTable: "EssayCatalogs",
                principalColumn: "EssayCatalogID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Essays_EssayArchives_EssayArchiveID",
                table: "Essays");

            migrationBuilder.DropForeignKey(
                name: "FK_Essays_EssayCatalogs_EssayCatalogID",
                table: "Essays");

            migrationBuilder.AlterColumn<int>(
                name: "EssayCatalogID",
                table: "Essays",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EssayArchiveID",
                table: "Essays",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Essays_EssayArchives_EssayArchiveID",
                table: "Essays",
                column: "EssayArchiveID",
                principalTable: "EssayArchives",
                principalColumn: "EssayArchiveID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Essays_EssayCatalogs_EssayCatalogID",
                table: "Essays",
                column: "EssayCatalogID",
                principalTable: "EssayCatalogs",
                principalColumn: "EssayCatalogID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
