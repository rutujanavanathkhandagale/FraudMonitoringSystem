using FraudMonitoringSystem.Models;
using FraudMonitoringSystem.Models.Admin;
using FraudMonitoringSystem.Models.AlertCase;
using FraudMonitoringSystem.Models.ComplianceOfficer;
using FraudMonitoringSystem.Models.Customer;
using FraudMonitoringSystem.Models.Investigator;
using FraudMonitoringSystem.Models.Notification;
using FraudMonitoringSystem.Models.Rules;
using FraudMonitoringSystem.Models.WatchList;
using Microsoft.EntityFrameworkCore;

namespace FraudMonitoringSystem.Data
{
    public class WebContext : DbContext
    {
        public WebContext(DbContextOptions<WebContext> options) : base(options) { }

        // Existing DbSets
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<PersonalDetails> PersonalDetails { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<KYCProfile> KYCProfile { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<DocumentAttachment> DocumentAttachments { get; set; }
        public DbSet<SystemUser> SystemUsers { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<Scenario> Scenarios { get; set; }

        // ✅ Fixed naming: plural DbSet
        public DbSet<DetectionRule> DetectionRules { get; set; }

        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<RiskScore> RiskScores { get; set; }
        public DbSet<Regulatory_Report> Regulatory_Report { get; set; } = default!;
        public DbSet<ControlChecklist> Control_Checklist { get; set; } = default!;
        public DbSet<Alert> Alerts { get; set; }
        public DbSet<Case> Cases { get; set; }
        public DbSet<Sanction> Sanctions { get; set; }
        public DbSet<PEPListModel> PEPList { get; set; }

        // New FraudShield DbSets
        public DbSet<WatchlistEntry> WatchlistEntries { get; set; }

        // Add EntityLink table
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
                .HasForeignKey(t => t.AccountId)
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

            modelBuilder.Entity<RiskScore>()
                .ToTable("RiskScores")
                .HasKey(rs => rs.ScoreId);

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
        }
    }
}
