using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tea.Migrations
{
    /// <inheritdoc />
    public partial class ModifyProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductType",
                table: "products",
                newName: "Name");

            migrationBuilder.AlterColumn<decimal>(
                name: "UnitPrice",
                table: "products",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(12, 2)");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "products",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "products");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "products",
                newName: "ProductType");

            migrationBuilder.AlterColumn<decimal>(
                name: "UnitPrice",
                table: "products",
                type: "decimal(12, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT");
        }
    }
}
