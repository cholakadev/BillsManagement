using Microsoft.EntityFrameworkCore;

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
        public virtual DbSet<Bill> Bills { get; set; }
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

            modelBuilder.Entity<Bill>(entity =>
            {
                entity.Property(e => e.BillId).ValueGeneratedNever();

                entity.Property(e => e.BillDate).HasColumnType("date");

                entity.Property(e => e.BillName)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.BillStatus).HasMaxLength(32);

                entity.Property(e => e.DueAmount).HasColumnType("decimal(19, 4)");

                entity.Property(e => e.PaidAmount).HasColumnType("decimal(19, 4)");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Bills)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UsersBills");
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
