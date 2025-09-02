using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StackbuldTechnicalAssessment.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreaed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PickupEmail",
                table: "Orders",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PickupName",
                table: "Orders",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PickupPhoneNumber",
                table: "Orders",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TotalItems",
                table: "Orders",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PickupEmail",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PickupName",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PickupPhoneNumber",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "TotalItems",
                table: "Orders");
        }
    }
}
