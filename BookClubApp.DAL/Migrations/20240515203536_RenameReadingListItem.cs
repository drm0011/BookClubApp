using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookClubApp.DAL.Migrations
{
    /// <inheritdoc />
    public partial class RenameReadingListItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReadingListItem_ReadingList_ReadingListId",
                table: "ReadingListItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReadingListItem",
                table: "ReadingListItem");

            migrationBuilder.RenameTable(
                name: "ReadingListItem",
                newName: "ReadingListItems");

            migrationBuilder.RenameIndex(
                name: "IX_ReadingListItem_ReadingListId",
                table: "ReadingListItems",
                newName: "IX_ReadingListItems_ReadingListId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReadingListItems",
                table: "ReadingListItems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReadingListItems_ReadingList_ReadingListId",
                table: "ReadingListItems",
                column: "ReadingListId",
                principalTable: "ReadingList",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReadingListItems_ReadingList_ReadingListId",
                table: "ReadingListItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReadingListItems",
                table: "ReadingListItems");

            migrationBuilder.RenameTable(
                name: "ReadingListItems",
                newName: "ReadingListItem");

            migrationBuilder.RenameIndex(
                name: "IX_ReadingListItems_ReadingListId",
                table: "ReadingListItem",
                newName: "IX_ReadingListItem_ReadingListId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReadingListItem",
                table: "ReadingListItem",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReadingListItem_ReadingList_ReadingListId",
                table: "ReadingListItem",
                column: "ReadingListId",
                principalTable: "ReadingList",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
