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

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductCategory> ProductCategories { get; set; }

    public virtual DbSet<Sale> Sales { get; set; }

    public virtual DbSet<SaleDetail> SaleDetails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            string connectionString = "Data Source=.; Initial Catalog = MYTDotNetCoreBatch5POS; User ID=sa; Password=sasa@123; TrustServerCertificate = true";
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Product__2D10D16AA441091E");

            entity.ToTable("Product");

            entity.HasIndex(e => e.ProductCode, "UQ__Product__C2068389823829EB").IsUnique();

            entity.Property(e => e.ProductId).HasColumnName("productId");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("price");
            entity.Property(e => e.ProductCategoryCode)
                .HasMaxLength(10)
                .HasColumnName("productCategoryCode");
            entity.Property(e => e.ProductCode)
                .HasMaxLength(10)
                .HasColumnName("productCode");
            entity.Property(e => e.ProductName)
                .HasMaxLength(10)
                .HasColumnName("productName");

            entity.HasOne(d => d.ProductCategoryCodeNavigation).WithMany(p => p.Products)
                .HasPrincipalKey(p => p.ProductCategoryCode)
                .HasForeignKey(d => d.ProductCategoryCode)
                .HasConstraintName("FK__Product__product__4D94879B");
        });

        modelBuilder.Entity<ProductCategory>(entity =>
        {
            entity.HasKey(e => e.ProductCategoryId).HasName("PK__ProductC__A944253B3E29E1CA");

            entity.ToTable("ProductCategory");

            entity.HasIndex(e => e.ProductCategoryCode, "UQ__ProductC__54C506DB1D52EC18").IsUnique();

            entity.Property(e => e.ProductCategoryId).HasColumnName("productCategoryId");
            entity.Property(e => e.ProductCategoryCode)
                .HasMaxLength(10)
                .HasColumnName("productCategoryCode");
            entity.Property(e => e.ProductCategoryName)
                .HasMaxLength(50)
                .HasColumnName("productCategoryName");
        });

        modelBuilder.Entity<Sale>(entity =>
        {
            entity.HasKey(e => e.SaleId).HasName("PK__Sale__FAE8F4F5E7178E60");

            entity.ToTable("Sale");

            entity.HasIndex(e => e.VoucherNo, "UQ__Sale__F533A04B8BD798D9").IsUnique();

            entity.Property(e => e.SaleId).HasColumnName("saleId");
            entity.Property(e => e.SaleDate)
                .HasColumnType("datetime")
                .HasColumnName("saleDate");
            entity.Property(e => e.TotalAmount)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("totalAmount");
            entity.Property(e => e.VoucherNo)
                .HasMaxLength(10)
                .HasColumnName("voucherNo");
        });

        modelBuilder.Entity<SaleDetail>(entity =>
        {
            entity.HasKey(e => e.SaleDetailsId).HasName("PK__SaleDeta__EF18C02015845BB8");

            entity.Property(e => e.SaleDetailsId).HasColumnName("saleDetailsId");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("price");
            entity.Property(e => e.ProductCode)
                .HasMaxLength(10)
                .HasColumnName("productCode");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.VoucherNo)
                .HasMaxLength(10)
                .HasColumnName("voucherNo");

            entity.HasOne(d => d.ProductCodeNavigation).WithMany(p => p.SaleDetails)
                .HasPrincipalKey(p => p.ProductCode)
                .HasForeignKey(d => d.ProductCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SaleDetai__produ__5441852A");

            entity.HasOne(d => d.VoucherNoNavigation).WithMany(p => p.SaleDetails)
                .HasPrincipalKey(p => p.VoucherNo)
                .HasForeignKey(d => d.VoucherNo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SaleDetai__vouch__534D60F1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
