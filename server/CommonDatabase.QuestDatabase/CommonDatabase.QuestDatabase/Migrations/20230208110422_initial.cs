using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CommonDatabase.QuestDatabase.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "coordinates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Latitude = table.Column<decimal>(type: "numeric", nullable: false),
                    Longitude = table.Column<decimal>(type: "numeric", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_coordinates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "quest",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Img = table.Column<string>(type: "text", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_quest", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    QuestId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stage_quest_QuestId",
                        column: x => x.QuestId,
                        principalTable: "quest",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "map_stage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    CoordsId = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_map_stage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_map_stage_Stage_Id",
                        column: x => x.Id,
                        principalTable: "Stage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_map_stage_coordinates_CoordsId",
                        column: x => x.CoordsId,
                        principalTable: "coordinates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "qrcode_stage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_qrcode_stage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_qrcode_stage_Stage_Id",
                        column: x => x.Id,
                        principalTable: "Stage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "test_stage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_test_stage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_test_stage_Stage_Id",
                        column: x => x.Id,
                        principalTable: "Stage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "text_stage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_text_stage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_text_stage_Stage_Id",
                        column: x => x.Id,
                        principalTable: "Stage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "video_stage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Url = table.Column<string>(type: "text", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_video_stage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_video_stage_Stage_Id",
                        column: x => x.Id,
                        principalTable: "Stage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_coordinates_Id",
                table: "coordinates",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_map_stage_CoordsId",
                table: "map_stage",
                column: "CoordsId");

            migrationBuilder.CreateIndex(
                name: "IX_map_stage_Id",
                table: "map_stage",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_qrcode_stage_Id",
                table: "qrcode_stage",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_quest_Id",
                table: "quest",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stage_QuestId",
                table: "Stage",
                column: "QuestId");

            migrationBuilder.CreateIndex(
                name: "IX_test_stage_Id",
                table: "test_stage",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_text_stage_Id",
                table: "text_stage",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_video_stage_Id",
                table: "video_stage",
                column: "Id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "map_stage");

            migrationBuilder.DropTable(
                name: "qrcode_stage");

            migrationBuilder.DropTable(
                name: "test_stage");

            migrationBuilder.DropTable(
                name: "text_stage");

            migrationBuilder.DropTable(
                name: "video_stage");

            migrationBuilder.DropTable(
                name: "coordinates");

            migrationBuilder.DropTable(
                name: "Stage");

            migrationBuilder.DropTable(
                name: "quest");
        }
    }
}
