using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace BillsManagement.DAL.Models
{
    public partial class BillsManagementContext : DbContext
    {
        public BillsManagementContext()
        {
        }

        public BillsManagementContext(DbContextOptions<BillsManagementContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Authentication> Authentications { get; set; }
        public virtual DbSet<CashAccount> CashAccounts { get; set; }
        public virtual DbSet<Charge> Charges { get; set; }
        public virtual DbSet<ChargeType> ChargeTypes { get; set; }
        public virtual DbSet<NotificationSetting> NotificationSettings { get; set; }
        public virtual DbSet<SecurityToken> SecurityTokens { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=BillsManagement;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<Authentication>(entity =>
            {
                entity.ToTable("Authentication");

                entity.Property(e => e.AuthenticationId).ValueGeneratedNever();

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(512);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Authentications)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UsersAuthentication");
            });

            modelBuilder.Entity<CashAccount>(entity =>
            {
                entity.ToTable("CashAccount");

                entity.Property(e => e.CashAccountId).ValueGeneratedNever();

                entity.Property(e => e.Balance)
                    .HasColumnType("decimal(7, 4)")
                    .HasDefaultValueSql("((0.0000))");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.CashAccounts)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__CashAccou__UserI__5CD6CB2B");
            });

            modelBuilder.Entity<Charge>(entity =>
            {
                entity.Property(e => e.ChargeId).ValueGeneratedNever();

                entity.Property(e => e.ChargeDate).HasColumnType("smalldatetime");

                entity.Property(e => e.DueAmount)
                    .HasColumnType("decimal(7, 4)")
                    .HasDefaultValueSql("((0.0000))");

                entity.HasOne(d => d.ChargeType)
                    .WithMany(p => p.Charges)
                    .HasForeignKey(d => d.ChargeTypeId)
                    .HasConstraintName("FK__Charges__ChargeT__66603565");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Charges)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Charges__UserId__656C112C");
            });

            modelBuilder.Entity<ChargeType>(entity =>
            {
                entity.Property(e => e.ChargeTypeId).ValueGeneratedNever();

                entity.Property(e => e.ChargeTypeName)
                    .IsRequired()
                    .HasMaxLength(64);
            });

            modelBuilder.Entity<NotificationSetting>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.BusinessEmail).HasMaxLength(128);

                entity.Property(e => e.BusinessEmailPassword).HasMaxLength(256);
            });

            modelBuilder.Entity<SecurityToken>(entity =>
            {
                entity.ToTable("SecurityToken");

                entity.Property(e => e.SecurityTokenId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.IsExpired).HasDefaultValueSql("((0))");

                entity.Property(e => e.SecurityToken1)
                    .IsRequired()
                    .HasMaxLength(1024)
                    .HasColumnName("SecurityToken");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.SecurityTokens)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SecurityT__UserI__71D1E811");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId).ValueGeneratedNever();

                entity.Property(e => e.Address).HasMaxLength(256);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.FirstName).HasMaxLength(64);

                entity.Property(e => e.LastName).HasMaxLength(64);

                entity.Property(e => e.MiddleName).HasMaxLength(64);

                entity.Property(e => e.Phone).HasMaxLength(128);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
