﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ComputerAccessoriesV2.Models
{
    public partial class ComputerAccessoriesV2Context : DbContext
    {
        public ComputerAccessoriesV2Context()
        {
        }

        public ComputerAccessoriesV2Context(DbContextOptions<ComputerAccessoriesV2Context> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<Attributes> Attributes { get; set; }
        public virtual DbSet<BillDetails> BillDetails { get; set; }
        public virtual DbSet<BillStatus> BillStatus { get; set; }
        public virtual DbSet<Bills> Bills { get; set; }
        public virtual DbSet<Blog> Blog { get; set; }
        public virtual DbSet<Brand> Brand { get; set; }
        public virtual DbSet<Campaign> Campaign { get; set; }
        public virtual DbSet<CampaignDetails> CampaignDetails { get; set; }
        public virtual DbSet<CampaignType> CampaignType { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Districts> Districts { get; set; }
        public virtual DbSet<ErrLogs> ErrLogs { get; set; }
        public virtual DbSet<NoStroredGuest> NoStroredGuest { get; set; }
        public virtual DbSet<OrderStatusLog> OrderStatusLog { get; set; }
        public virtual DbSet<PaymentType> PaymentType { get; set; }
        public virtual DbSet<ProductAttribute> ProductAttribute { get; set; }
        public virtual DbSet<ProductImages> ProductImages { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<Provinces> Provinces { get; set; }
        public virtual DbSet<Reviews> Reviews { get; set; }
        public virtual DbSet<SystemConfig> SystemConfig { get; set; }
        public virtual DbSet<TransactionHistory> TransactionHistory { get; set; }
        public virtual DbSet<UserAddress> UserAddress { get; set; }
        public virtual DbSet<Vouchers> Vouchers { get; set; }
        public virtual DbSet<Ward> Ward { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=35.194.1.21,1433;Initial Catalog=ComputerAccessoriesV2;Persist Security Info=False;User ID=sqllol;Password=}eVcQQPHRPoRLIf4;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.HasIndex(e => e.RoleId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<Attributes>(entity =>
            {
                entity.Property(e => e.AttributeName).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Attributes)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_Attributes_Category");
            });

            modelBuilder.Entity<BillDetails>(entity =>
            {
                entity.HasKey(e => new { e.ProductId, e.BillId });

                entity.Property(e => e.UnitPrice).HasColumnType("money");

                entity.HasOne(d => d.Bill)
                    .WithMany(p => p.BillDetails)
                    .HasForeignKey(d => d.BillId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BillDetails_Bills");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.BillDetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BillDetails_Products");
            });

            modelBuilder.Entity<BillStatus>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CodeName).HasMaxLength(50);
            });

            modelBuilder.Entity<Bills>(entity =>
            {
                entity.HasKey(e => e.BillId);

                entity.Property(e => e.BillName).HasMaxLength(50);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.DeliveredDate).HasColumnType("datetime");

                entity.Property(e => e.LastPrice).HasColumnType("money");

                entity.Property(e => e.Note).HasMaxLength(256);

                entity.Property(e => e.ShippingAddress).HasMaxLength(256);

                entity.Property(e => e.TotalPrice).HasColumnType("money");

                entity.Property(e => e.Voucher).HasMaxLength(50);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Bills)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_Bills_AspNetUsers");

                entity.HasOne(d => d.GuestAnony)
                    .WithMany(p => p.Bills)
                    .HasForeignKey(d => d.GuestAnonyId)
                    .HasConstraintName("FK_Bills_NoStroredGuest");

                entity.HasOne(d => d.StatusNavigation)
                    .WithMany(p => p.Bills)
                    .HasForeignKey(d => d.Status)
                    .HasConstraintName("FK_Bills_BillStatus");
            });

            modelBuilder.Entity<Blog>(entity =>
            {
                entity.Property(e => e.BlogName).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.ShortUrl).HasMaxLength(50);

                entity.HasOne(d => d.CreatedUser)
                    .WithMany(p => p.Blog)
                    .HasForeignKey(d => d.CreatedUserId)
                    .HasConstraintName("FK_Blog_AspNetUsers");
            });

            modelBuilder.Entity<Brand>(entity =>
            {
                entity.Property(e => e.BrandName).HasMaxLength(100);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Campaign>(entity =>
            {
                entity.Property(e => e.CampaignName).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Campaign)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_Campaign_CampaignType");
            });

            modelBuilder.Entity<CampaignDetails>(entity =>
            {
                entity.HasKey(e => e.CampaignDetailId)
                    .HasName("PK_CampaignDetails_1");

                entity.Property(e => e.PromotionPrice).HasColumnType("money");

                entity.HasOne(d => d.Campaign)
                    .WithMany(p => p.CampaignDetails)
                    .HasForeignKey(d => d.CampaignId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CampaignDetails_Campaign");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.CampaignDetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CampaignDetails_Products");
            });

            modelBuilder.Entity<CampaignType>(entity =>
            {
                entity.HasKey(e => e.TypeId);

                entity.Property(e => e.Description).HasMaxLength(50);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.CategoryName).HasMaxLength(100);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Parend)
                    .WithMany(p => p.InverseParend)
                    .HasForeignKey(d => d.ParendId)
                    .HasConstraintName("FK_Category_Category");
            });

            modelBuilder.Entity<Districts>(entity =>
            {
                entity.HasKey(e => e.DistrictId);

                entity.Property(e => e.DistrictId).ValueGeneratedNever();

                entity.Property(e => e.DistrictName).HasMaxLength(50);

                entity.Property(e => e.DistrictType).HasMaxLength(50);

                entity.HasOne(d => d.Province)
                    .WithMany(p => p.Districts)
                    .HasForeignKey(d => d.ProvinceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Districts_Provinces");
            });

            modelBuilder.Entity<ErrLogs>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Url).HasMaxLength(50);
            });

            modelBuilder.Entity<NoStroredGuest>(entity =>
            {
                entity.Property(e => e.CustomerName).HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.PhoneNumber).HasMaxLength(20);

                entity.Property(e => e.PlaceDetail).HasMaxLength(50);
            });

            modelBuilder.Entity<OrderStatusLog>(entity =>
            {
                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.HasOne(d => d.Bill)
                    .WithMany(p => p.OrderStatusLog)
                    .HasForeignKey(d => d.BillId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderStatusLog_Bills");

                entity.HasOne(d => d.NewStatusNavigation)
                    .WithMany(p => p.OrderStatusLog)
                    .HasForeignKey(d => d.NewStatus)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderStatusLog_BillStatus");
            });

            modelBuilder.Entity<PaymentType>(entity =>
            {
                entity.HasKey(e => e.PaymentId);

                entity.Property(e => e.PaymentId).ValueGeneratedNever();

                entity.Property(e => e.Description).HasMaxLength(50);
            });

            modelBuilder.Entity<ProductAttribute>(entity =>
            {
                entity.HasKey(e => new { e.ProductId, e.AttributeId });

                entity.Property(e => e.Value).HasMaxLength(100);

                entity.HasOne(d => d.Attribute)
                    .WithMany(p => p.ProductAttribute)
                    .HasForeignKey(d => d.AttributeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductAttribute_Attributes");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductAttribute)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductAttribute_Products");
            });

            modelBuilder.Entity<ProductImages>(entity =>
            {
                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductImages)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_ProductImages_Products");
            });

            modelBuilder.Entity<Products>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(15);

                entity.Property(e => e.Color).HasMaxLength(25);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.IsNew)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Origin).HasMaxLength(50);

                entity.Property(e => e.OriginalPrice).HasColumnType("money");

                entity.Property(e => e.ProductName).HasMaxLength(100);

                entity.Property(e => e.PromotionPrice).HasColumnType("money");

                entity.Property(e => e.ShorDescription).HasMaxLength(256);

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.BrandId)
                    .HasConstraintName("FK_Products_Brand");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_Products_Category");
            });

            modelBuilder.Entity<Provinces>(entity =>
            {
                entity.HasKey(e => e.ProvinceId);

                entity.Property(e => e.ProvinceId).ValueGeneratedNever();

                entity.Property(e => e.ProvinceName).HasMaxLength(50);

                entity.Property(e => e.ProvinceType).HasMaxLength(50);
            });

            modelBuilder.Entity<Reviews>(entity =>
            {
                entity.HasKey(e => e.ReviewId)
                    .HasName("PK_Comments");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Reviews_Product");
            });

            modelBuilder.Entity<SystemConfig>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Facebook).HasMaxLength(50);

                entity.Property(e => e.LogoUrl).HasMaxLength(50);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.ShopAddress).HasMaxLength(256);

                entity.Property(e => e.ShopName).HasMaxLength(100);

                entity.Property(e => e.Skype).HasMaxLength(50);

                entity.Property(e => e.SystemEmail).HasMaxLength(50);

                entity.Property(e => e.SystemPhone).HasMaxLength(20);

                entity.Property(e => e.WorkingTime).HasMaxLength(50);

                entity.Property(e => e.Zalo).HasMaxLength(50);
            });

            modelBuilder.Entity<TransactionHistory>(entity =>
            {
                entity.HasKey(e => e.TransactionId);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.PaymentAmout).HasColumnType("money");

                entity.HasOne(d => d.Bill)
                    .WithMany(p => p.TransactionHistory)
                    .HasForeignKey(d => d.BillId)
                    .HasConstraintName("FK_TransactionHistory_Bills");

                entity.HasOne(d => d.PaymentTypeNavigation)
                    .WithMany(p => p.TransactionHistory)
                    .HasForeignKey(d => d.PaymentType)
                    .HasConstraintName("FK_TransactionHistory_PaymentType");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TransactionHistory)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_TransactionHistory_AspNetUsers");
            });

            modelBuilder.Entity<UserAddress>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.PlaceDetails).HasMaxLength(256);

                entity.HasOne(d => d.District)
                    .WithMany(p => p.UserAddress)
                    .HasForeignKey(d => d.DistrictId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserAddress_Districts");

                entity.HasOne(d => d.Province)
                    .WithMany(p => p.UserAddress)
                    .HasForeignKey(d => d.ProvinceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserAddress_Provinces");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserAddress)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserAddress_AspNetUsers");

                entity.HasOne(d => d.Ward)
                    .WithMany(p => p.UserAddress)
                    .HasForeignKey(d => d.WardId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserAddress_Ward");
            });

            modelBuilder.Entity<Vouchers>(entity =>
            {
                entity.HasKey(e => e.VoucherId);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.DateActive).HasColumnType("datetime");

                entity.Property(e => e.ExpiredDate).HasColumnType("datetime");

                entity.Property(e => e.VoucherName).HasMaxLength(50);
            });

            modelBuilder.Entity<Ward>(entity =>
            {
                entity.Property(e => e.WardId).ValueGeneratedNever();

                entity.Property(e => e.WardName).HasMaxLength(50);

                entity.Property(e => e.WardType).HasMaxLength(50);

                entity.HasOne(d => d.District)
                    .WithMany(p => p.Ward)
                    .HasForeignKey(d => d.DistrictId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ward_Districts");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
