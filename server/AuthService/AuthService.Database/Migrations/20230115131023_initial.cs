using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AuthService.Database.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false, comment: "Первичный ключ")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false, comment: "Имя пользователя"),
                    surname = table.Column<string>(type: "text", nullable: false, comment: "Фамилия пользователя"),
                    username = table.Column<string>(type: "text", nullable: false, comment: "Логин пользователя"),
                    password = table.Column<string>(type: "text", nullable: false, comment: "Пароль пользователя")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.id);
                },
                comment: "Пользователь");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user");
        }
    }
}
