using Microsoft.EntityFrameworkCore.Migrations;

namespace DocsClone.EfCore.Migrations
{
    public partial class tablefixes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Users_UserId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_UserId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Revisions",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Projects");

            migrationBuilder.AddColumn<long>(
                name: "DocumentId",
                table: "Users",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_DocumentId",
                table: "Users",
                column: "DocumentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Projects_DocumentId",
                table: "Users",
                column: "DocumentId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Projects_DocumentId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_DocumentId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DocumentId",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "Revisions",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "Projects",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_UserId",
                table: "Projects",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Users_UserId",
                table: "Projects",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
