using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookClubApp.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ChangeReadingListItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReadingListItem_Books_BookId",
                table: "ReadingListItem");

            migrationBuilder.DropIndex(
                name: "IX_ReadingListItem_BookId",
                table: "ReadingListItem");

            migrationBuilder.DropColumn(
                name: "BookId",
                table: "ReadingListItem");

            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "ReadingListItem",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PublishYear",
                table: "ReadingListItem",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "ReadingListItem",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Author",
                table: "ReadingListItem");

            migrationBuilder.DropColumn(
                name: "PublishYear",
                table: "ReadingListItem");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "ReadingListItem");

            migrationBuilder.AddColumn<int>(
                name: "BookId",
                table: "ReadingListItem",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ReadingListItem_BookId",
                table: "ReadingListItem",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReadingListItem_Books_BookId",
                table: "ReadingListItem",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
