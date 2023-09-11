using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebAppInsurancePolicy.Models.Db;

public partial class InsuranceDbContext : DbContext
{
    public InsuranceDbContext()
    {
    }

    public InsuranceDbContext(DbContextOptions<InsuranceDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Policy> Policies { get; set; }

    public virtual DbSet<PolicyHolder> PolicyHolders { get; set; }

    public virtual DbSet<PolicySold> PolicySolds { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=InsuranceDb;Integrated Security=true;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CatId).HasName("PK__Category__6A1C8AFAD07CF9D5");

            entity.ToTable("Category");

            entity.Property(e => e.CatId).ValueGeneratedNever();
            entity.Property(e => e.Cat)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Policy>(entity =>
        {
            entity.HasKey(e => e.PolicyId).HasName("PK__Policy__2E1339A421578669");

            entity.ToTable("Policy");

            entity.Property(e => e.PolicyId).ValueGeneratedNever();
            entity.Property(e => e.CoverageAmount).HasColumnType("money");
            entity.Property(e => e.Num).HasColumnName("num");
            entity.Property(e => e.PolicyName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PremiumAmount).HasColumnType("money");

            entity.HasOne(d => d.NumNavigation).WithMany(p => p.Policies)
                .HasForeignKey(d => d.Num)
                .HasConstraintName("FK__Policy__num__3A81B327");
        });

        modelBuilder.Entity<PolicyHolder>(entity =>
        {
            entity.HasKey(e => e.PolicyHolderId).HasName("PK__PolicyHo__0549D02B4C6AC4E2");

            entity.ToTable("PolicyHolder");

            entity.Property(e => e.PolicyHolderId).ValueGeneratedNever();
            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Contact)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Dob).HasColumnType("date");
            entity.Property(e => e.Email)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .IsUnicode(false);

            entity.HasOne(d => d.PolicyHolderNavigation).WithOne(p => p.PolicyHolder)
                .HasForeignKey<PolicyHolder>(d => d.PolicyHolderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PolicyHol__Polic__49C3F6B7");
        });

        modelBuilder.Entity<PolicySold>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PolicySo__3214EC07090AE9BB");

            entity.ToTable("PolicySold");

            entity.Property(e => e.EndDate).HasColumnType("date");
            entity.Property(e => e.PolicyName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.StartDate).HasColumnType("date");

            entity.HasOne(d => d.PolicyHolder).WithMany(p => p.PolicySolds)
                .HasForeignKey(d => d.PolicyHolderId)
                .HasConstraintName("FK__PolicySol__Polic__3E52440B");

            entity.HasOne(d => d.Policy).WithMany(p => p.PolicySolds)
                .HasForeignKey(d => d.PolicyId)
                .HasConstraintName("FK__PolicySol__Polic__3D5E1FD2");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4CC1CB45B6");

            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Role)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
