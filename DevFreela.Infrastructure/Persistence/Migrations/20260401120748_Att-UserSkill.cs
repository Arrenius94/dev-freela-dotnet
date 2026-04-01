using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevFreela.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AttUserSkill : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserSkill_Skills_SkillId",
                table: "UserSkill");

            migrationBuilder.DropIndex(
                name: "IX_UserSkill_SkillId",
                table: "UserSkill");

            migrationBuilder.DropColumn(
                name: "SkillId",
                table: "UserSkill");

            migrationBuilder.CreateIndex(
                name: "IX_UserSkill_IdUser",
                table: "UserSkill",
                column: "IdUser");

            migrationBuilder.AddForeignKey(
                name: "FK_UserSkill_Skills_IdSkill",
                table: "UserSkill",
                column: "IdSkill",
                principalTable: "Skills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSkill_User_IdUser",
                table: "UserSkill",
                column: "IdUser",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserSkill_Skills_IdSkill",
                table: "UserSkill");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSkill_User_IdUser",
                table: "UserSkill");

            migrationBuilder.DropIndex(
                name: "IX_UserSkill_IdUser",
                table: "UserSkill");

            migrationBuilder.AddColumn<int>(
                name: "SkillId",
                table: "UserSkill",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserSkill_SkillId",
                table: "UserSkill",
                column: "SkillId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserSkill_Skills_SkillId",
                table: "UserSkill",
                column: "SkillId",
                principalTable: "Skills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
