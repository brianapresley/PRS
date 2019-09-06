using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PRSProjectV1.Models
{
    public partial class SqlPRSContext : DbContext
    {
        public SqlPRSContext()
        {
        }

        public SqlPRSContext(DbContextOptions<SqlPRSContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Request> Request { get; set; }
        public virtual DbSet<RequestLine> RequestLine { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Vendors> Vendors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=LAPTOP-5UPQ1A8N\\SQLEXPRESS;Initial Catalog=SqlPRS;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasIndex(e => e.PartNbr)
                    .HasName("UQ__Product__DAFC0C1E479CC6B4")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.PartNbr)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.PhotoPath).HasMaxLength(255);

                entity.Property(e => e.Price).HasColumnType("decimal(11, 2)");

                entity.Property(e => e.Unit)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.VendorId).HasColumnName("VendorID");

                entity.HasOne(d => d.Vendor)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.VendorId)
                    .HasConstraintName("FK__Product__VendorI__5BE2A6F2");
            });

            modelBuilder.Entity<Request>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DeliveryMode)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasDefaultValueSql("('Pickup')");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(80);

                entity.Property(e => e.Justification)
                    .IsRequired()
                    .HasMaxLength(80);

                entity.Property(e => e.RejectionReason).HasMaxLength(80);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasDefaultValueSql("('NEW')");

                entity.Property(e => e.Total).HasColumnType("decimal(11, 2)");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Request)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Request__UserID__619B8048");
            });

            modelBuilder.Entity<RequestLine>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.Quantity).HasDefaultValueSql("((1))");

                entity.Property(e => e.RequestId).HasColumnName("RequestID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.RequestLine)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__RequestLi__Produ__656C112C");

                entity.HasOne(d => d.Request)
                    .WithMany(p => p.RequestLine)
                    .HasForeignKey(d => d.RequestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__RequestLi__Reque__6477ECF3");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasIndex(e => e.Username)
                    .HasName("UQ__Users__536C85E428D15769")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Phone).HasMaxLength(12);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<Vendors>(entity =>
            {
                entity.HasIndex(e => e.Code)
                    .HasName("UQ__Vendors__A25C5AA78B186CDB")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Phone).HasMaxLength(12);

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasMaxLength(2);

                entity.Property(e => e.Zip)
                    .IsRequired()
                    .HasMaxLength(5);
            });
        }
    }
}
