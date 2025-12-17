using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lab10.Migrations
{
    /// <inheritdoc />
    public partial class FormImg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Articles",
                newName: "ImageUrl");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Articles",
                newName: "Image");
        }
    }
}
