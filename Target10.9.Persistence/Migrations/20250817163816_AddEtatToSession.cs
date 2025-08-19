using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Target10._9.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddEtatToSession : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Etat",
                table: "Sessions",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Etat",
                table: "Sessions");
        }
    }
}
