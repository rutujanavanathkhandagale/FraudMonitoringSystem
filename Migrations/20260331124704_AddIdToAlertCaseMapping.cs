using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FraudMonitoringSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddIdToAlertCaseMapping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AlertCaseMappings_AlertCaseMappings_AlertCaseMappingAlertID_AlertCaseMappingCaseID",
                table: "AlertCaseMappings");

            migrationBuilder.DropForeignKey(
                name: "FK_CaseAttachments_AlertCaseMappings_AlertCaseMappingAlertID_AlertCaseMappingCaseID",
                table: "CaseAttachments");

            migrationBuilder.DropForeignKey(
                name: "FK_InvestigationNotes_AlertCaseMappings_AlertCaseMappingAlertID_AlertCaseMappingCaseID",
                table: "InvestigationNotes");

            migrationBuilder.DropIndex(
                name: "IX_InvestigationNotes_AlertCaseMappingAlertID_AlertCaseMappingCaseID",
                table: "InvestigationNotes");

            migrationBuilder.DropIndex(
                name: "IX_CaseAttachments_AlertCaseMappingAlertID_AlertCaseMappingCaseID",
                table: "CaseAttachments");

            migrationBuilder.DropIndex(
                name: "IX_AlertCaseMappings_AlertCaseMappingAlertID_AlertCaseMappingCaseID",
                table: "AlertCaseMappings");

            migrationBuilder.DropColumn(
                name: "AlertCaseMappingAlertID",
                table: "InvestigationNotes");

            migrationBuilder.DropColumn(
                name: "AlertCaseMappingCaseID",
                table: "InvestigationNotes");

            migrationBuilder.DropColumn(
                name: "AlertCaseMappingAlertID",
                table: "CaseAttachments");

            migrationBuilder.DropColumn(
                name: "AlertCaseMappingCaseID",
                table: "CaseAttachments");

            migrationBuilder.DropColumn(
                name: "AlertCaseMappingAlertID",
                table: "AlertCaseMappings");

            migrationBuilder.DropColumn(
                name: "AlertCaseMappingCaseID",
                table: "AlertCaseMappings");

            migrationBuilder.DropColumn(
                name: "CaseType",
                table: "AlertCaseMappings");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "AlertCaseMappings");

            migrationBuilder.DropColumn(
                name: "Priority",
                table: "AlertCaseMappings");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "AlertCaseMappings");

            migrationBuilder.RenameColumn(
                name: "PrimaryCustomerID",
                table: "AlertCaseMappings",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "AlertCaseMappings",
                newName: "PrimaryCustomerID");

            migrationBuilder.AddColumn<int>(
                name: "AlertCaseMappingAlertID",
                table: "InvestigationNotes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AlertCaseMappingCaseID",
                table: "InvestigationNotes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AlertCaseMappingAlertID",
                table: "CaseAttachments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AlertCaseMappingCaseID",
                table: "CaseAttachments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AlertCaseMappingAlertID",
                table: "AlertCaseMappings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AlertCaseMappingCaseID",
                table: "AlertCaseMappings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CaseType",
                table: "AlertCaseMappings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "AlertCaseMappings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Priority",
                table: "AlertCaseMappings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "AlertCaseMappings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_InvestigationNotes_AlertCaseMappingAlertID_AlertCaseMappingCaseID",
                table: "InvestigationNotes",
                columns: new[] { "AlertCaseMappingAlertID", "AlertCaseMappingCaseID" });

            migrationBuilder.CreateIndex(
                name: "IX_CaseAttachments_AlertCaseMappingAlertID_AlertCaseMappingCaseID",
                table: "CaseAttachments",
                columns: new[] { "AlertCaseMappingAlertID", "AlertCaseMappingCaseID" });

            migrationBuilder.CreateIndex(
                name: "IX_AlertCaseMappings_AlertCaseMappingAlertID_AlertCaseMappingCaseID",
                table: "AlertCaseMappings",
                columns: new[] { "AlertCaseMappingAlertID", "AlertCaseMappingCaseID" });

            migrationBuilder.AddForeignKey(
                name: "FK_AlertCaseMappings_AlertCaseMappings_AlertCaseMappingAlertID_AlertCaseMappingCaseID",
                table: "AlertCaseMappings",
                columns: new[] { "AlertCaseMappingAlertID", "AlertCaseMappingCaseID" },
                principalTable: "AlertCaseMappings",
                principalColumns: new[] { "AlertID", "CaseID" });

            migrationBuilder.AddForeignKey(
                name: "FK_CaseAttachments_AlertCaseMappings_AlertCaseMappingAlertID_AlertCaseMappingCaseID",
                table: "CaseAttachments",
                columns: new[] { "AlertCaseMappingAlertID", "AlertCaseMappingCaseID" },
                principalTable: "AlertCaseMappings",
                principalColumns: new[] { "AlertID", "CaseID" });

            migrationBuilder.AddForeignKey(
                name: "FK_InvestigationNotes_AlertCaseMappings_AlertCaseMappingAlertID_AlertCaseMappingCaseID",
                table: "InvestigationNotes",
                columns: new[] { "AlertCaseMappingAlertID", "AlertCaseMappingCaseID" },
                principalTable: "AlertCaseMappings",
                principalColumns: new[] { "AlertID", "CaseID" });
        }
    }
}
