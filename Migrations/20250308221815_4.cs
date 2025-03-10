using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GalaxyForum.Migrations
{
    /// <inheritdoc />
    public partial class _4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Discussion_DiscussionId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Discussion_AspNetUsers_ApplicationUserId",
                table: "Discussion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Discussion",
                table: "Discussion");

            migrationBuilder.RenameTable(
                name: "Discussion",
                newName: "Discussions");

            migrationBuilder.RenameIndex(
                name: "IX_Discussion_ApplicationUserId",
                table: "Discussions",
                newName: "IX_Discussions_ApplicationUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Discussions",
                table: "Discussions",
                column: "DiscussionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Discussions_DiscussionId",
                table: "Comments",
                column: "DiscussionId",
                principalTable: "Discussions",
                principalColumn: "DiscussionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Discussions_AspNetUsers_ApplicationUserId",
                table: "Discussions",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Discussions_DiscussionId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Discussions_AspNetUsers_ApplicationUserId",
                table: "Discussions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Discussions",
                table: "Discussions");

            migrationBuilder.RenameTable(
                name: "Discussions",
                newName: "Discussion");

            migrationBuilder.RenameIndex(
                name: "IX_Discussions_ApplicationUserId",
                table: "Discussion",
                newName: "IX_Discussion_ApplicationUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Discussion",
                table: "Discussion",
                column: "DiscussionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Discussion_DiscussionId",
                table: "Comments",
                column: "DiscussionId",
                principalTable: "Discussion",
                principalColumn: "DiscussionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Discussion_AspNetUsers_ApplicationUserId",
                table: "Discussion",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
