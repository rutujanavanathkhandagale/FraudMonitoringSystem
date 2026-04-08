using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FraudMonitoringSystem.Migrations
{
    /// <inheritdoc />
    public partial class data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuditLogs",
                columns: table => new
                {
                    AuditLogId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntityType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EntityId = table.Column<int>(type: "int", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    PerformedBy = table.Column<int>(type: "int", nullable: false),
                    PerformedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.AuditLogId);
                });

            migrationBuilder.CreateTable(
                name: "Cases",
                columns: table => new
                {
                    CaseID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CaseType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Priority = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TransactionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cases", x => x.CaseID);
                });

            migrationBuilder.CreateTable(
                name: "EntityLinks",
                columns: table => new
                {
                    LinkId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FromCustomerId = table.Column<long>(type: "bigint", nullable: false),
                    ToAccountId = table.Column<long>(type: "bigint", nullable: false),
                    LinkType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityLinks", x => x.LinkId);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<long>(type: "bigint", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    PermissionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModuleName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ActionName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.PermissionId);
                });

            migrationBuilder.CreateTable(
                name: "PersonalDetails",
                columns: table => new
                {
                    CustomerId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FatherName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MotherName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CustomerType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DOB = table.Column<DateOnly>(type: "date", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    PermanentAddress = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CurrentAddress = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ProfileImagePath = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalDetails", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "Registrations",
                columns: table => new
                {
                    RegistrationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ConfirmPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registrations", x => x.RegistrationId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "Scenarios",
                columns: table => new
                {
                    ScenarioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    RiskDomain = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scenarios", x => x.ScenarioId);
                });

            migrationBuilder.CreateTable(
                name: "WatchlistEntries",
                columns: table => new
                {
                    EntryId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ListType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Identifier = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WatchlistEntries", x => x.EntryId);
                });

            migrationBuilder.CreateTable(
                name: "Alerts",
                columns: table => new
                {
                    AlertID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionID = table.Column<int>(type: "int", nullable: false),
                    RuleID = table.Column<int>(type: "int", nullable: false),
                    Severity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CaseID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alerts", x => x.AlertID);
                    table.ForeignKey(
                        name: "FK_Alerts_Cases_CaseID",
                        column: x => x.CaseID,
                        principalTable: "Cases",
                        principalColumn: "CaseID");
                });

            migrationBuilder.CreateTable(
                name: "CaseAttachments",
                columns: table => new
                {
                    AttachmentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CaseID = table.Column<int>(type: "int", nullable: false),
                    FileURI = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UploadedBy = table.Column<int>(type: "int", nullable: false),
                    UploadedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaseAttachments", x => x.AttachmentID);
                    table.ForeignKey(
                        name: "FK_CaseAttachments_Cases_CaseID",
                        column: x => x.CaseID,
                        principalTable: "Cases",
                        principalColumn: "CaseID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Control_Checklist",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CaseID = table.Column<int>(type: "int", nullable: false),
                    CheckedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    OverallResult = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Control_Checklist", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Control_Checklist_Cases_CaseID",
                        column: x => x.CaseID,
                        principalTable: "Cases",
                        principalColumn: "CaseID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvestigationNotes",
                columns: table => new
                {
                    NoteID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CaseID = table.Column<int>(type: "int", nullable: false),
                    AuthorID = table.Column<int>(type: "int", nullable: false),
                    NoteText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvestigationNotes", x => x.NoteID);
                    table.ForeignKey(
                        name: "FK_InvestigationNotes_Cases_CaseID",
                        column: x => x.CaseID,
                        principalTable: "Cases",
                        principalColumn: "CaseID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Regulatory_Report",
                columns: table => new
                {
                    ReportID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CaseID = table.Column<int>(type: "int", nullable: false),
                    ReportType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Period = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubmittedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regulatory_Report", x => x.ReportID);
                    table.ForeignKey(
                        name: "FK_Regulatory_Report_Cases_CaseID",
                        column: x => x.CaseID,
                        principalTable: "Cases",
                        principalColumn: "CaseID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    AccountId = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CustomerId = table.Column<long>(type: "bigint", nullable: false),
                    AccountNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ProductType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.AccountId);
                    table.ForeignKey(
                        name: "FK_Accounts_PersonalDetails_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "PersonalDetails",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KYCProfile",
                columns: table => new
                {
                    KYCId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<long>(type: "bigint", nullable: false),
                    DocumentRefsJSON = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KYCProfile", x => x.KYCId);
                    table.ForeignKey(
                        name: "FK_KYCProfile_PersonalDetails_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "PersonalDetails",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserInfos",
                columns: table => new
                {
                    UserInfoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegistrationId = table.Column<int>(type: "int", nullable: false),
                    IsEmailVerified = table.Column<bool>(type: "bit", nullable: true),
                    VerificationOtp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OtpExpiry = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ResetToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResetTokenExpiry = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInfos", x => x.UserInfoId);
                    table.ForeignKey(
                        name: "FK_UserInfos_Registrations_RegistrationId",
                        column: x => x.RegistrationId,
                        principalTable: "Registrations",
                        principalColumn: "RegistrationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SystemUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SystemUserCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    RegistrationId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    ApprovedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ApprovedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SystemUsers_Registrations_RegistrationId",
                        column: x => x.RegistrationId,
                        principalTable: "Registrations",
                        principalColumn: "RegistrationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SystemUsers_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DetectionRules",
                columns: table => new
                {
                    RuleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Expression = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Threshold = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    CustomerType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Version = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ScenarioId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetectionRules", x => x.RuleId);
                    table.ForeignKey(
                        name: "FK_DetectionRules_Scenarios_ScenarioId",
                        column: x => x.ScenarioId,
                        principalTable: "Scenarios",
                        principalColumn: "ScenarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AlertCaseMappings",
                columns: table => new
                {
                    AlertID = table.Column<int>(type: "int", nullable: false),
                    CaseID = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlertCaseMappings", x => new { x.AlertID, x.CaseID });
                    table.ForeignKey(
                        name: "FK_AlertCaseMappings_Alerts_AlertID",
                        column: x => x.AlertID,
                        principalTable: "Alerts",
                        principalColumn: "AlertID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlertCaseMappings_Cases_CaseID",
                        column: x => x.CaseID,
                        principalTable: "Cases",
                        principalColumn: "CaseID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Control_Details",
                columns: table => new
                {
                    DetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ControlName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ControlChecklistId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Control_Details", x => x.DetailId);
                    table.ForeignKey(
                        name: "FK_Control_Details_Control_Checklist_ControlChecklistId",
                        column: x => x.ControlChecklistId,
                        principalTable: "Control_Checklist",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    TransactionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    CustomerId = table.Column<long>(type: "bigint", nullable: false),
                    CustomerType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CounterpartyAccount = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    TransactionType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Channel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GeoLocation = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SourceType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.TransactionID);
                    table.ForeignKey(
                        name: "FK_Transactions_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transactions_PersonalDetails_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "PersonalDetails",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RiskScores",
                columns: table => new
                {
                    ScoreID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionID = table.Column<int>(type: "int", nullable: false),
                    ScoreValue = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    ReasonsJSON = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EvaluatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RiskScores", x => x.ScoreID);
                    table.ForeignKey(
                        name: "FK_RiskScores_Transactions_TransactionID",
                        column: x => x.TransactionID,
                        principalTable: "Transactions",
                        principalColumn: "TransactionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_CustomerId",
                table: "Accounts",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_AlertCaseMappings_CaseID",
                table: "AlertCaseMappings",
                column: "CaseID");

            migrationBuilder.CreateIndex(
                name: "IX_Alerts_CaseID",
                table: "Alerts",
                column: "CaseID");

            migrationBuilder.CreateIndex(
                name: "IX_CaseAttachments_CaseID",
                table: "CaseAttachments",
                column: "CaseID");

            migrationBuilder.CreateIndex(
                name: "IX_Control_Checklist_CaseID",
                table: "Control_Checklist",
                column: "CaseID");

            migrationBuilder.CreateIndex(
                name: "IX_Control_Details_ControlChecklistId",
                table: "Control_Details",
                column: "ControlChecklistId");

            migrationBuilder.CreateIndex(
                name: "IX_DetectionRules_ScenarioId",
                table: "DetectionRules",
                column: "ScenarioId");

            migrationBuilder.CreateIndex(
                name: "IX_InvestigationNotes_CaseID",
                table: "InvestigationNotes",
                column: "CaseID");

            migrationBuilder.CreateIndex(
                name: "IX_KYCProfile_CustomerId",
                table: "KYCProfile",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Regulatory_Report_CaseID",
                table: "Regulatory_Report",
                column: "CaseID");

            migrationBuilder.CreateIndex(
                name: "IX_RiskScores_TransactionID",
                table: "RiskScores",
                column: "TransactionID");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_RoleName",
                table: "Roles",
                column: "RoleName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SystemUsers_RegistrationId",
                table: "SystemUsers",
                column: "RegistrationId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemUsers_RoleId",
                table: "SystemUsers",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemUsers_SystemUserCode",
                table: "SystemUsers",
                column: "SystemUserCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_AccountId",
                table: "Transactions",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CustomerId",
                table: "Transactions",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_UserInfos_RegistrationId",
                table: "UserInfos",
                column: "RegistrationId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlertCaseMappings");

            migrationBuilder.DropTable(
                name: "AuditLogs");

            migrationBuilder.DropTable(
                name: "CaseAttachments");

            migrationBuilder.DropTable(
                name: "Control_Details");

            migrationBuilder.DropTable(
                name: "DetectionRules");

            migrationBuilder.DropTable(
                name: "EntityLinks");

            migrationBuilder.DropTable(
                name: "InvestigationNotes");

            migrationBuilder.DropTable(
                name: "KYCProfile");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "Regulatory_Report");

            migrationBuilder.DropTable(
                name: "RiskScores");

            migrationBuilder.DropTable(
                name: "SystemUsers");

            migrationBuilder.DropTable(
                name: "UserInfos");

            migrationBuilder.DropTable(
                name: "WatchlistEntries");

            migrationBuilder.DropTable(
                name: "Alerts");

            migrationBuilder.DropTable(
                name: "Control_Checklist");

            migrationBuilder.DropTable(
                name: "Scenarios");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Registrations");

            migrationBuilder.DropTable(
                name: "Cases");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "PersonalDetails");
        }
    }
}
