using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Forum.Data.Migrations
{
    /// <inheritdoc />
    public partial class addinglistofmessagesinforumthreadentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ThreadMessage_ForumThreadId",
                table: "ThreadMessage",
                column: "ForumThreadId");

            migrationBuilder.AddForeignKey(
                name: "FK_ThreadMessage_ForumThread_ForumThreadId",
                table: "ThreadMessage",
                column: "ForumThreadId",
                principalTable: "ForumThread",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ThreadMessage_ForumThread_ForumThreadId",
                table: "ThreadMessage");

            migrationBuilder.DropIndex(
                name: "IX_ThreadMessage_ForumThreadId",
                table: "ThreadMessage");
        }
    }
}
