using FraudMonitoringSystem.Authentication;
using FraudMonitoringSystem.Models;
using FraudMonitoringSystem.Models.Admin;
using FraudMonitoringSystem.Models.AlertCase;
using FraudMonitoringSystem.Models.AlertsCase;
using FraudMonitoringSystem.Models.ComplianceOfficer;
using FraudMonitoringSystem.Models.Customer;
using FraudMonitoringSystem.Models.Investigator;
using FraudMonitoringSystem.Models.Notification;
using FraudMonitoringSystem.Models.Rules;
using FraudMonitoringSystem.Models.WatchList;
using Microsoft.EntityFrameworkCore;
using System;
namespace FraudMonitoringSystem.Data
{
    public class WebContext : DbContext
    {
        public WebContext(DbContextOptions<WebContext> options) : base(options) { }

        // Customer-related DbSets
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<PersonalDetails> PersonalDetails { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<KYCProfile> KYCProfile { get; set; }
            
        // Messaging/Notification
        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<DocumentAttachment> DocumentAttachments { get; set; }

        // Security/Admin
        public DbSet<SystemUser> SystemUsers { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }

        // Rules/Scenarios
        public DbSet<Scenario> Scenarios { get; set; }
        public DbSet<DetectionRule> DetectionRules { get; set; }

        // Transactions & Risk
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<RiskScore> RiskScores { get; set; }

        // Regulatory & Compliance
        public DbSet<Regulatory_Report> Regulatory_Report { get; set; }
        public DbSet<ControlChecklist> Control_Checklist { get; set; }

		// Alerts & Cases
		public DbSet<Alert> Alerts { get; set; }

		public DbSet<Case> Cases { get; set; }

		public DbSet<AlertCaseMapping> AlertCaseMappings { get; set; }

		public DbSet<CaseAttachment> CaseAttachments { get; set; }
        public DbSet<UserInfo> UserInfos { get; set; }

        public DbSet<InvestigationNote> InvestigationNotes { get; set; }



	
        public DbSet<WatchlistEntry> WatchlistEntries { get; set; }

        // Entity Links
        public DbSet<EntityLink> EntityLinks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Role config
            modelBuilder.Entity<Role>()
                .HasIndex(r => r.RoleName)
                .IsUnique();

            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Role)
                .WithMany(r => r.RolePermissions)
                .HasForeignKey(rp => rp.RoleId);

            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Permission)
                .WithMany(p => p.RolePermissions)
                .HasForeignKey(rp => rp.PermissionId);

            // Account → Customer
            modelBuilder.Entity<Account>()
                .HasOne(a => a.Customer)
                .WithMany(c => c.Accounts)
                .HasForeignKey(a => a.CustomerId);

            // Transaction → Account
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Account)
                .WithMany()
                .HasForeignKey(t => t.AccountID)
                .OnDelete(DeleteBehavior.Cascade);

            // Transaction → Customer
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Customer)
                .WithMany()
                .HasForeignKey(t => t.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Decimal precision fixes
            modelBuilder.Entity<Transaction>()
                .Property(t => t.Amount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Transaction>()
                .ToTable("Transactions")
                .HasKey(t => t.TransactionID);

            // RiskScore mapping
            modelBuilder.Entity<RiskScore>()
                .ToTable("RiskScores")
                .HasKey(rs => rs.ScoreID);   // ✅ use ScoreID (int)

            modelBuilder.Entity<RiskScore>()
                .HasOne<Transaction>()
                .WithMany()
                .HasForeignKey(rs => rs.TransactionID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DetectionRule>()
                .Property(d => d.Threshold)
                .HasPrecision(18, 2);

            modelBuilder.Entity<DocumentAttachment>()
                .HasOne(d => d.ChatMessage)
                .WithMany(c => c.Attachments)
                .HasForeignKey(d => d.ChatMessageId);

            modelBuilder.Entity<EntityLink>()
                .HasKey(e => e.LinkId);




                // Primary Keys
			modelBuilder.Entity<Alert>().HasKey(a => a.AlertID);

			modelBuilder.Entity<Case>().HasKey(c => c.CaseID);

			modelBuilder.Entity<AlertCaseMapping>().HasKey(ac => new{ac.AlertID,ac.CaseID});

			modelBuilder.Entity<CaseAttachment>().HasKey(ca => ca.AttachmentID);

			modelBuilder.Entity<InvestigationNote>().HasKey(n => n.NoteID);



			// AlertCaseMapping Relationships
			modelBuilder.Entity<AlertCaseMapping>()
				.HasOne(ac => ac.Alert)
				.WithMany(a => a.AlertCaseMappings)
				.HasForeignKey(ac => ac.AlertID);

			modelBuilder.Entity<AlertCaseMapping>()
				.HasOne(ac => ac.Case)
				.WithMany(c => c.AlertCaseMappings)
				.HasForeignKey(ac => ac.CaseID);


			// Case → InvestigationNotes
			modelBuilder.Entity<InvestigationNote>()
				.HasOne(n => n.Case)
				.WithMany(c => c.InvestigationNotes)
				.HasForeignKey(n => n.CaseID);


			// Case → Attachments
			modelBuilder.Entity<CaseAttachment>()
				.HasOne(a => a.Case)
				.WithMany(c => c.CaseAttachments)
				.HasForeignKey(a => a.CaseID);
        }
    }
}
