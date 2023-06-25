using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APITracker.Migrations
{
    /// <inheritdoc />
    public partial class addcolumnambiente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Ambiente",
                table: "EnderecoApi",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ambiente",
                table: "EnderecoApi");
        }
    }
}
