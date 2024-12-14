using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DotNetPOS.Database.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblProduct> TblProducts { get; set; }

    public virtual DbSet<TblProductCategory> TblProductCategories { get; set; }

    public virtual DbSet<TblSale> TblSales { get; set; }

    public virtual DbSet<TblSaleDetail> TblSaleDetails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=DotNetCoreBatch5POS;User Id=sa;Password=sasa@123;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblProduct>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__TblProdu__B40CC6CD8FFAA800");

            entity.ToTable("TblProduct");

            entity.HasIndex(e => e.ProductCode, "UQ__TblProdu__2F4E024F4B4DBE7C").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Price).HasMaxLength(20);
            entity.Property(e => e.ProductCategoryCode).HasMaxLength(50);
            entity.Property(e => e.ProductCode).HasMaxLength(50);
        });

        modelBuilder.Entity<TblProductCategory>(entity =>
        {
            entity.HasKey(e => e.ProductCategoryId).HasName("PK__TblProdu__3224ECCEC6BD99EB");

            entity.ToTable("TblProductCategory");

            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.ProductCategoryCode).HasMaxLength(50);
        });

        modelBuilder.Entity<TblSale>(entity =>
        {
            entity.HasKey(e => e.SaleId).HasName("PK__TblSale__1EE3C3FF9ED36C78");

            entity.ToTable("TblSale");

            entity.HasIndex(e => e.VoucherNo, "UQ__TblSale__3AD31D6F90A2C9C8").IsUnique();

            entity.Property(e => e.SaleDate).HasColumnType("datetime");
            entity.Property(e => e.TotalAmount).HasMaxLength(20);
            entity.Property(e => e.VoucherNo).HasMaxLength(50);
        });

        modelBuilder.Entity<TblSaleDetail>(entity =>
        {
            entity.HasKey(e => e.SaleDetailId).HasName("PK__TblSaleD__70DB14FE8440BB65");

            entity.ToTable("TblSaleDetail");

            entity.Property(e => e.Price).HasMaxLength(20);
            entity.Property(e => e.ProductCode).HasMaxLength(50);
            entity.Property(e => e.VoucherNo).HasMaxLength(50);

            entity.HasOne(d => d.ProductCodeNavigation).WithMany(p => p.TblSaleDetails)
                .HasPrincipalKey(p => p.ProductCode)
                .HasForeignKey(d => d.ProductCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TblSaleDe__Produ__403A8C7D");

            entity.HasOne(d => d.VoucherNoNavigation).WithMany(p => p.TblSaleDetails)
                .HasPrincipalKey(p => p.VoucherNo)
                .HasForeignKey(d => d.VoucherNo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TblSaleDe__Vouch__3F466844");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
