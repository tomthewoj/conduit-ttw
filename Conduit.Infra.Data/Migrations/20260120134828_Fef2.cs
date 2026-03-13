using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Conduit.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class Fef2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticleFavorite_Articles_ArticleEntityId",
                table: "ArticleFavorite");

            migrationBuilder.DropIndex(
                name: "IX_ArticleFavorite_ArticleEntityId",
                table: "ArticleFavorite");

            migrationBuilder.DropColumn(
                name: "ArticleEntityId",
                table: "ArticleFavorite");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ArticleEntityId",
                table: "ArticleFavorite",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ArticleFavorite_ArticleEntityId",
                table: "ArticleFavorite",
                column: "ArticleEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleFavorite_Articles_ArticleEntityId",
                table: "ArticleFavorite",
                column: "ArticleEntityId",
                principalTable: "Articles",
                principalColumn: "Id");
        }
    }
}
