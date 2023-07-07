using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace shop.Models;

public partial class SportShopContext : DbContext
{
    public SportShopContext()
    {
    }

    public SportShopContext(DbContextOptions<SportShopContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Manufacture> Manufactures { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<TypeProduct> TypeProducts { get; set; }

    public virtual DbSet<User> Users { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-70TQE1A\\BAZA;Initial Catalog=Sport_Shop;Persist Security Info=True;User ID=sa;Password=123; Encrypt=false");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Manufacture>(entity =>
        {
            entity.HasKey(e => e.IdManufacture);

            entity.ToTable("Manufacture");

            entity.HasIndex(e => e.CompanyName, "UQ_Company_Name").IsUnique();

            entity.Property(e => e.IdManufacture).HasColumnName("ID_Manufacture");
            entity.Property(e => e.CompanyName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Company_Name");
            entity.Property(e => e.Country)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.IdOrder);

            entity.ToTable("Order");

            entity.HasIndex(e => e.OrderNumber, "UQ_Order_Number").IsUnique();

            entity.Property(e => e.IdOrder).HasColumnName("ID_Order");
            entity.Property(e => e.OrderNumber)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("Order_Number");
            entity.Property(e => e.ProductId).HasColumnName("Product_ID");
            entity.Property(e => e.UserId).HasColumnName("User_ID");

            entity.HasOne(d => d.Product).WithMany(p => p.Orders)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_Order");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_Order");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.IdProduct);

            entity.ToTable("Product");

            entity.Property(e => e.IdProduct).HasColumnName("ID_Product");
            entity.Property(e => e.Image).IsUnicode(false);
            entity.Property(e => e.ManufactureId).HasColumnName("Manufacture_ID");
            entity.Property(e => e.ProductCost)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("Product_Cost");
            entity.Property(e => e.ProductName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Product_Name");
            entity.Property(e => e.TypeProductId).HasColumnName("Type_Product_ID");

            entity.HasOne(d => d.Manufacture).WithMany(p => p.Products)
                .HasForeignKey(d => d.ManufactureId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Manufacture_Product");

            entity.HasOne(d => d.TypeProduct).WithMany(p => p.Products)
                .HasForeignKey(d => d.TypeProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Type_Product_Product");
        });

        modelBuilder.Entity<TypeProduct>(entity =>
        {
            entity.HasKey(e => e.IdTypeProduct).HasName("PK_Type");

            entity.ToTable("Type_Product");

            entity.HasIndex(e => e.NameType, "UQ_Name_Type").IsUnique();

            entity.Property(e => e.IdTypeProduct).HasColumnName("ID_Type_Product");
            entity.Property(e => e.NameType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Name_Type");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser);

            entity.ToTable("User");

            entity.HasIndex(e => e.Email, "UQ_Email").IsUnique();

            entity.HasIndex(e => e.LoginUser, "UQ_Login_User").IsUnique();

            entity.HasIndex(e => e.PhoneNumber, "UQ_Phone_Number").IsUnique();

            entity.Property(e => e.IdUser).HasColumnName("ID_User");
            entity.Property(e => e.Email)
                .HasMaxLength(64)
                .IsUnicode(false);
            entity.Property(e => e.FirstNameUser)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("First_Name_User");
            entity.Property(e => e.LoginUser)
                .HasMaxLength(32)
                .IsUnicode(false)
                .HasColumnName("Login_User");
            entity.Property(e => e.MiddleNameUser)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasDefaultValueSql("('-')")
                .HasColumnName("Middle_Name_User");
            entity.Property(e => e.PasswordUser)
                .HasMaxLength(32)
                .IsUnicode(false)
                .HasColumnName("Password_User");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(17)
                .IsUnicode(false)
                .HasColumnName("Phone_Number");
            entity.Property(e => e.SecondNameUser)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("Second_Name_User");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
