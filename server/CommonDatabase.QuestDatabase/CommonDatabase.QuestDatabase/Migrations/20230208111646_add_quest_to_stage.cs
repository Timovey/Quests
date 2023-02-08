using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CommonDatabase.QuestDatabase.Migrations
{
    /// <inheritdoc />
    public partial class addquesttostage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stage_quest_QuestId",
                table: "Stage");

            migrationBuilder.AlterColumn<int>(
                name: "QuestId",
                table: "Stage",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Stage_quest_QuestId",
                table: "Stage",
                column: "QuestId",
                principalTable: "quest",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stage_quest_QuestId",
                table: "Stage");

            migrationBuilder.AlterColumn<int>(
                name: "QuestId",
                table: "Stage",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Stage_quest_QuestId",
                table: "Stage",
                column: "QuestId",
                principalTable: "quest",
                principalColumn: "Id");
        }
    }
}
