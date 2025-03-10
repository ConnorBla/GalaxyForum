using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GalaxyForum.Migrations
{
    /// <inheritdoc />
    public partial class _3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Discussion99999999999_DiscussionId",
                table: "Comments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Discussion99999999999",
                table: "Discussion99999999999");

            migrationBuilder.RenameTable(
                name: "Discussion99999999999",
                newName: "Discussion");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Comments",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Discussion",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Discussion",
                table: "Discussion",
                column: "DiscussionId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ApplicationUserId",
                table: "Comments",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Discussion_ApplicationUserId",
                table: "Discussion",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_ApplicationUserId",
                table: "Comments",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_ApplicationUserId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Discussion_DiscussionId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Discussion_AspNetUsers_ApplicationUserId",
                table: "Discussion");

            migrationBuilder.DropIndex(
                name: "IX_Comments_ApplicationUserId",
                table: "Comments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Discussion",
                table: "Discussion");

            migrationBuilder.DropIndex(
                name: "IX_Discussion_ApplicationUserId",
                table: "Discussion");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Discussion");

            migrationBuilder.RenameTable(
                name: "Discussion",
                newName: "Discussion99999999999");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Discussion99999999999",
                table: "Discussion99999999999",
                column: "DiscussionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Discussion99999999999_DiscussionId",
                table: "Comments",
                column: "DiscussionId",
                principalTable: "Discussion99999999999",
                principalColumn: "DiscussionId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
