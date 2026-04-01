using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FraudMonitoringSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddAlertIdToCase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AlertId",
                table: "Cases",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TransactionId",
                table: "Cases",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AlertId",
                table: "Cases");

            migrationBuilder.DropColumn(
                name: "TransactionId",
                table: "Cases");
        }
    }
}
