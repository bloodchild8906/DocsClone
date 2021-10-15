using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DocsClone.EfCore.Migrations
{
    public partial class tablefixesv2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Details_Users_UserId",
                table: "Details");

            migrationBuilder.DropForeignKey(
                name: "FK_Revisions_Projects_DocumentId",
                table: "Revisions");

            migrationBuilder.DropForeignKey(
                name: "FK_Revisions_Users_CreatedById",
                table: "Revisions");

            migrationBuilder.DropForeignKey(
                name: "FK_Revisions_Users_DocumentOwnerId",
                table: "Revisions");

            migrationBuilder.DropForeignKey(
                name: "FK_Revisions_Users_ModifiedById",
                table: "Revisions");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Projects_DocumentId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Revisions",
                table: "Revisions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Projects",
                table: "Projects");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "users");

            migrationBuilder.RenameTable(
                name: "Revisions",
                newName: "revisions");

            migrationBuilder.RenameTable(
                name: "Projects",
                newName: "documents");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "users",
                newName: "username");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "users",
                newName: "password");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "users",
                newName: "user_id");

            migrationBuilder.RenameIndex(
                name: "IX_Users_DocumentId",
                table: "users",
                newName: "IX_users_DocumentId");

            migrationBuilder.RenameColumn(
                name: "Modifications",
                table: "revisions",
                newName: "modifications");

            migrationBuilder.RenameColumn(
                name: "ModifiedWithTimezone",
                table: "revisions",
                newName: "modified_with_timezone");

            migrationBuilder.RenameColumn(
                name: "ModifiedOn",
                table: "revisions",
                newName: "modified_on");

            migrationBuilder.RenameColumn(
                name: "DocumentVersion",
                table: "revisions",
                newName: "document_version");

            migrationBuilder.RenameColumn(
                name: "DocumentData",
                table: "revisions",
                newName: "document_data");

            migrationBuilder.RenameColumn(
                name: "CreatedWithTimezone",
                table: "revisions",
                newName: "created_with_timezone");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "revisions",
                newName: "created_on");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "revisions",
                newName: "revision_id");

            migrationBuilder.RenameIndex(
                name: "IX_Revisions_ModifiedById",
                table: "revisions",
                newName: "IX_revisions_ModifiedById");

            migrationBuilder.RenameIndex(
                name: "IX_Revisions_DocumentOwnerId",
                table: "revisions",
                newName: "IX_revisions_DocumentOwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Revisions_DocumentId",
                table: "revisions",
                newName: "IX_revisions_DocumentId");

            migrationBuilder.RenameIndex(
                name: "IX_Revisions_CreatedById",
                table: "revisions",
                newName: "IX_revisions_CreatedById");

            migrationBuilder.RenameColumn(
                name: "Surname",
                table: "Details",
                newName: "surname");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Details",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Details",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "PrimaryContactNumber",
                table: "Details",
                newName: "primary_contact_number");

            migrationBuilder.RenameColumn(
                name: "ModifiedWithTimezone",
                table: "Details",
                newName: "modified_with_timezone");

            migrationBuilder.RenameColumn(
                name: "DateOfBirth",
                table: "Details",
                newName: "date_of_birth");

            migrationBuilder.RenameColumn(
                name: "DateModified",
                table: "Details",
                newName: "date_modified");

            migrationBuilder.RenameColumn(
                name: "DateCreated",
                table: "Details",
                newName: "date_created");

            migrationBuilder.RenameColumn(
                name: "CreatedWithTimezone",
                table: "Details",
                newName: "created_with_timezone");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Details",
                newName: "detail_id");

            migrationBuilder.RenameColumn(
                name: "CurrentVersion",
                table: "documents",
                newName: "current_revision");

            migrationBuilder.RenameColumn(
                name: "AccessLevel",
                table: "documents",
                newName: "access_level");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "documents",
                newName: "document_id");

            migrationBuilder.AlterColumn<string>(
                name: "modifications",
                table: "revisions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "document_data",
                table: "revisions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_users",
                table: "users",
                column: "user_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_revisions",
                table: "revisions",
                column: "revision_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_documents",
                table: "documents",
                column: "document_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Details_users_UserId",
                table: "Details",
                column: "UserId",
                principalTable: "users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_revisions_documents_DocumentId",
                table: "revisions",
                column: "DocumentId",
                principalTable: "documents",
                principalColumn: "document_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_revisions_users_CreatedById",
                table: "revisions",
                column: "CreatedById",
                principalTable: "users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_revisions_users_DocumentOwnerId",
                table: "revisions",
                column: "DocumentOwnerId",
                principalTable: "users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_revisions_users_ModifiedById",
                table: "revisions",
                column: "ModifiedById",
                principalTable: "users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_users_documents_DocumentId",
                table: "users",
                column: "DocumentId",
                principalTable: "documents",
                principalColumn: "document_id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Details_users_UserId",
                table: "Details");

            migrationBuilder.DropForeignKey(
                name: "FK_revisions_documents_DocumentId",
                table: "revisions");

            migrationBuilder.DropForeignKey(
                name: "FK_revisions_users_CreatedById",
                table: "revisions");

            migrationBuilder.DropForeignKey(
                name: "FK_revisions_users_DocumentOwnerId",
                table: "revisions");

            migrationBuilder.DropForeignKey(
                name: "FK_revisions_users_ModifiedById",
                table: "revisions");

            migrationBuilder.DropForeignKey(
                name: "FK_users_documents_DocumentId",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_users",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_revisions",
                table: "revisions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_documents",
                table: "documents");

            migrationBuilder.RenameTable(
                name: "users",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "revisions",
                newName: "Revisions");

            migrationBuilder.RenameTable(
                name: "documents",
                newName: "Projects");

            migrationBuilder.RenameColumn(
                name: "username",
                table: "Users",
                newName: "Username");

            migrationBuilder.RenameColumn(
                name: "password",
                table: "Users",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "Users",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_users_DocumentId",
                table: "Users",
                newName: "IX_Users_DocumentId");

            migrationBuilder.RenameColumn(
                name: "modifications",
                table: "Revisions",
                newName: "Modifications");

            migrationBuilder.RenameColumn(
                name: "modified_with_timezone",
                table: "Revisions",
                newName: "ModifiedWithTimezone");

            migrationBuilder.RenameColumn(
                name: "modified_on",
                table: "Revisions",
                newName: "ModifiedOn");

            migrationBuilder.RenameColumn(
                name: "document_version",
                table: "Revisions",
                newName: "DocumentVersion");

            migrationBuilder.RenameColumn(
                name: "document_data",
                table: "Revisions",
                newName: "DocumentData");

            migrationBuilder.RenameColumn(
                name: "created_with_timezone",
                table: "Revisions",
                newName: "CreatedWithTimezone");

            migrationBuilder.RenameColumn(
                name: "created_on",
                table: "Revisions",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "revision_id",
                table: "Revisions",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_revisions_ModifiedById",
                table: "Revisions",
                newName: "IX_Revisions_ModifiedById");

            migrationBuilder.RenameIndex(
                name: "IX_revisions_DocumentOwnerId",
                table: "Revisions",
                newName: "IX_Revisions_DocumentOwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_revisions_DocumentId",
                table: "Revisions",
                newName: "IX_Revisions_DocumentId");

            migrationBuilder.RenameIndex(
                name: "IX_revisions_CreatedById",
                table: "Revisions",
                newName: "IX_Revisions_CreatedById");

            migrationBuilder.RenameColumn(
                name: "surname",
                table: "Details",
                newName: "Surname");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Details",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Details",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "primary_contact_number",
                table: "Details",
                newName: "PrimaryContactNumber");

            migrationBuilder.RenameColumn(
                name: "modified_with_timezone",
                table: "Details",
                newName: "ModifiedWithTimezone");

            migrationBuilder.RenameColumn(
                name: "date_of_birth",
                table: "Details",
                newName: "DateOfBirth");

            migrationBuilder.RenameColumn(
                name: "date_modified",
                table: "Details",
                newName: "DateModified");

            migrationBuilder.RenameColumn(
                name: "date_created",
                table: "Details",
                newName: "DateCreated");

            migrationBuilder.RenameColumn(
                name: "created_with_timezone",
                table: "Details",
                newName: "CreatedWithTimezone");

            migrationBuilder.RenameColumn(
                name: "detail_id",
                table: "Details",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "current_revision",
                table: "Projects",
                newName: "CurrentVersion");

            migrationBuilder.RenameColumn(
                name: "access_level",
                table: "Projects",
                newName: "AccessLevel");

            migrationBuilder.RenameColumn(
                name: "document_id",
                table: "Projects",
                newName: "Id");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Modifications",
                table: "Revisions",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "DocumentData",
                table: "Revisions",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Revisions",
                table: "Revisions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Projects",
                table: "Projects",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Details_Users_UserId",
                table: "Details",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Revisions_Projects_DocumentId",
                table: "Revisions",
                column: "DocumentId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Revisions_Users_CreatedById",
                table: "Revisions",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Revisions_Users_DocumentOwnerId",
                table: "Revisions",
                column: "DocumentOwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Revisions_Users_ModifiedById",
                table: "Revisions",
                column: "ModifiedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Projects_DocumentId",
                table: "Users",
                column: "DocumentId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
