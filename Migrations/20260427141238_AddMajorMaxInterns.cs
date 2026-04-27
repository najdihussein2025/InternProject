using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternSystemProject.Migrations
{
    /// <inheritdoc />
    public partial class AddMajorMaxInterns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaxInterns",
                table: "Majors",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxInterns",
                table: "Majors");
        }
    }
}
