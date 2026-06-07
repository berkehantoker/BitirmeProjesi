using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BitirmeProjesi.Migrations
{
    /// <inheritdoc />
    public partial class UseruserAddv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserResponses_Questions_QuestionId",
                table: "UserResponses");

            migrationBuilder.DropForeignKey(
                name: "FK_UserResponses_Users_UserId",
                table: "UserResponses");

            migrationBuilder.AddForeignKey(
                name: "FK_UserResponses_Questions_QuestionId",
                table: "UserResponses",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserResponses_Users_UserId",
                table: "UserResponses",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserResponses_Questions_QuestionId",
                table: "UserResponses");

            migrationBuilder.DropForeignKey(
                name: "FK_UserResponses_Users_UserId",
                table: "UserResponses");

            migrationBuilder.AddForeignKey(
                name: "FK_UserResponses_Questions_QuestionId",
                table: "UserResponses",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserResponses_Users_UserId",
                table: "UserResponses",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
