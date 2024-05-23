using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_commerce.DAL.Migrations
{
    /// <inheritdoc />
    public partial class editItemPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "UnitPrice",
                table: "Cartitems",
                type: "decimal(10,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnitPrice",
                table: "Cartitems");
        }
    }
}
