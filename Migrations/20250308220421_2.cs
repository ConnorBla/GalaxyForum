using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GalaxyForum.Migrations
{
    /// <inheritdoc />
    public partial class _2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Discussionxxxxxxxxxxxx_DiscussionId",
                table: "Comments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Discussionxxxxxxxxxxxx",
                table: "Discussionxxxxxxxxxxxx");

            migrationBuilder.RenameTable(
                name: "Discussionxxxxxxxxxxxx",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Discussion99999999999_DiscussionId",
                table: "Comments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Discussion99999999999",
                table: "Discussion99999999999");

            migrationBuilder.RenameTable(
                name: "Discussion99999999999",
                newName: "Discussionxxxxxxxxxxxx");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Discussionxxxxxxxxxxxx",
                table: "Discussionxxxxxxxxxxxx",
                column: "DiscussionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Discussionxxxxxxxxxxxx_DiscussionId",
                table: "Comments",
                column: "DiscussionId",
                principalTable: "Discussionxxxxxxxxxxxx",
                principalColumn: "DiscussionId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
