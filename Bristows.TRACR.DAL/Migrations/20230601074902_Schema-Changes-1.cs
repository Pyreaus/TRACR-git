using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bristows.TRACR.DAL.Migrations
{
    /// <inheritdoc />
    public partial class SchemaChanges1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skill_DiaryTask_DiaryTaskId",
                schema: "dbo",
                table: "Skill");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Trainee",
                schema: "dbo",
                table: "Trainee");

            migrationBuilder.DropIndex(
                name: "IX_Skill_DiaryTaskId",
                schema: "dbo",
                table: "Skill");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DiaryTask",
                schema: "dbo",
                table: "DiaryTask");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Diary",
                schema: "dbo",
                table: "Diary");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Admin",
                schema: "dbo",
                table: "Admin");

            migrationBuilder.DropColumn(
                name: "DiaryTaskId",
                schema: "dbo",
                table: "Skill");

            migrationBuilder.AlterColumn<Guid>(
                name: "LocalId",
                schema: "dbo",
                table: "Trainee",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LocalId",
                schema: "dbo",
                table: "Trainee",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: Guid.NewGuid());

            migrationBuilder.AlterColumn<Guid>(
                name: "LocalId",
                schema: "dbo",
                table: "DiaryTask",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldMaxLength: 5,
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LocalId",
                schema: "dbo",
                table: "DiaryTask",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: Guid.NewGuid());

            migrationBuilder.AlterColumn<Guid>(
                name: "LocalId",
                schema: "dbo",
                table: "Diary",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LocalId",
                schema: "dbo",
                table: "Diary",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: Guid.NewGuid());

            migrationBuilder.AlterColumn<Guid>(
                name: "LocalId",
                schema: "dbo",
                table: "Admin",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LocalId",
                schema: "dbo",
                table: "Admin",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: Guid.NewGuid());

            migrationBuilder.AddPrimaryKey(
                name: "PK_Trainee",
                schema: "dbo",
                table: "Trainee",
                column: "LocalId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DiaryTask",
                schema: "dbo",
                table: "DiaryTask",
                column: "LocalId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Diary",
                schema: "dbo",
                table: "Diary",
                column: "LocalId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Admin",
                schema: "dbo",
                table: "Admin",
                column: "LocalId");

            migrationBuilder.CreateIndex(
                name: "IX_Skill_DiaryTask_LocalId",
                schema: "dbo",
                table: "Skill",
                column: "DiaryTaskLocalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Skill_DiaryTask_DiaryTaskLocalId",
                schema: "dbo",
                table: "Skill",
                column: "DiaryTaskLocalId",
                principalSchema: "dbo",
                principalTable: "DiaryTask",
                principalColumn: "LocalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skill_DiaryTask_DiaryTaskLocalId",
                schema: "dbo",
                table: "Skill");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Trainee",
                schema: "dbo",
                table: "Trainee");

            migrationBuilder.DropIndex(
                name: "IX_Skill_DiaryTask_LocalId",
                schema: "dbo",
                table: "Skill");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DiaryTask",
                schema: "dbo",
                table: "DiaryTask");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Diary",
                schema: "dbo",
                table: "Diary");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Admin",
                schema: "dbo",
                table: "Admin");

            migrationBuilder.DropColumn(
                name: "LocalId",
                schema: "dbo",
                table: "Trainee");

            migrationBuilder.DropColumn(
                name: "LocalId",
                schema: "dbo",
                table: "DiaryTask");

            migrationBuilder.DropColumn(
                name: "LocalId",
                schema: "dbo",
                table: "Diary");

            migrationBuilder.DropColumn(
                name: "LocalId",
                schema: "dbo",
                table: "Admin");

            migrationBuilder.AlterColumn<int>(
                name: "TraineeId",
                schema: "dbo",
                table: "Trainee",
                type: "int",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 15);

            migrationBuilder.AddColumn<int>(
                name: "DiaryTaskId",
                schema: "dbo",
                table: "Skill",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DiaryTaskId",
                schema: "dbo",
                table: "DiaryTask",
                type: "int",
                maxLength: 5,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 5);

            migrationBuilder.AlterColumn<int>(
                name: "DiaryId",
                schema: "dbo",
                table: "Diary",
                type: "int",
                maxLength: 5,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 5);

            migrationBuilder.AlterColumn<int>(
                name: "AdminId",
                schema: "dbo",
                table: "Admin",
                type: "int",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 15);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Trainee",
                schema: "dbo",
                table: "Trainee",
                column: "TraineeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DiaryTask",
                schema: "dbo",
                table: "DiaryTask",
                column: "DiaryTaskId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Diary",
                schema: "dbo",
                table: "Diary",
                column: "DiaryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Admin",
                schema: "dbo",
                table: "Admin",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_Skill_DiaryTaskId",
                schema: "dbo",
                table: "Skill",
                column: "DiaryTaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_Skill_DiaryTask_DiaryTaskId",
                schema: "dbo",
                table: "Skill",
                column: "DiaryTaskId",
                principalSchema: "dbo",
                principalTable: "DiaryTask",
                principalColumn: "DiaryTaskId");
        }
    }
}
