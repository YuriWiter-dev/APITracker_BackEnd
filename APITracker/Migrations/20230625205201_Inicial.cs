using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APITracker.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EnderecoApi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Endereco = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    StatusCode = table.Column<int>(type: "int", nullable: false),
                    Error = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    TimeOutEmMinutos = table.Column<int>(type: "int", nullable: false),
                    Method = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: true),
                    Body = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnderecoApi", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnderecoApi");
        }
    }
}
