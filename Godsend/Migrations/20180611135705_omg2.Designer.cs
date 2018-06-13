﻿// <auto-generated />
using Godsend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;

namespace Godsend.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20180611135705_omg2")]
    partial class Omg2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Godsend.Models.Article", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<Guid?>("EntityInformationId");

                    b.Property<Guid?>("InfoId");

                    b.HasKey("Id");

                    b.HasIndex("EntityInformationId");

                    b.HasIndex("InfoId");

                    b.ToTable("Articles");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Article");
                });

            modelBuilder.Entity("Godsend.Models.Column", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("DirectoryId");

                    b.HasKey("Id");

                    b.HasIndex("DirectoryId");

                    b.ToTable("Column");
                });

            modelBuilder.Entity("Godsend.Models.Data", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ColumnId");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<int?>("RealDataId");

                    b.HasKey("Id");

                    b.HasIndex("ColumnId");

                    b.HasIndex("RealDataId")
                        .IsUnique()
                        .HasFilter("[RealDataId] IS NOT NULL");

                    b.ToTable("Data");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Data");
                });

            modelBuilder.Entity("Godsend.Models.Directory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("BaseId");

                    b.HasKey("Id");

                    b.HasIndex("BaseId");

                    b.ToTable("DirList");
                });

            modelBuilder.Entity("Godsend.Models.Information", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Name");

                    b.Property<double>("Rating");

                    b.Property<int>("Watches");

                    b.HasKey("Id");

                    b.ToTable("Information");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Information");
                });

            modelBuilder.Entity("Godsend.Models.LinkProductsSuppliers", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Price");

                    b.Property<Guid>("ProductId");

                    b.Property<Guid>("SupplierId");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("SupplierId");

                    b.ToTable("LinkProductsSuppliers");
                });

            modelBuilder.Entity("Godsend.Models.Location", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.HasKey("Id");

                    b.ToTable("Location");
                });

            modelBuilder.Entity("Godsend.Models.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<DateTime?>("Done");

                    b.Property<string>("EFCustomerId");

                    b.Property<DateTime>("Ordered");

                    b.Property<int>("Status");

                    b.HasKey("Id");

                    b.HasIndex("EFCustomerId");

                    b.ToTable("Orders");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Order");
                });

            modelBuilder.Entity("Godsend.Models.OrderPartDiscrete", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("OrderId");

                    b.Property<Guid>("ProductId");

                    b.Property<int>("Quantity");

                    b.Property<Guid>("SupplierId");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.HasIndex("SupplierId");

                    b.ToTable("OrderPartDiscrete");
                });

            modelBuilder.Entity("Godsend.Models.OrderPartWeighted", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("OrderId");

                    b.Property<Guid>("ProductId");

                    b.Property<Guid>("SupplierId");

                    b.Property<double>("Weight");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.HasIndex("SupplierId");

                    b.ToTable("OrderPartWeighted");
                });

            modelBuilder.Entity("Godsend.Models.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<Guid?>("EntityInformationId");

                    b.Property<Guid?>("InfoId");

                    b.HasKey("Id");

                    b.HasIndex("EntityInformationId");

                    b.HasIndex("InfoId");

                    b.ToTable("Products");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Product");
                });

            modelBuilder.Entity("Godsend.Models.StringWrapper", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("ArticleInformationId");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.HasIndex("ArticleInformationId");

                    b.ToTable("StringWrapper");
                });

            modelBuilder.Entity("Godsend.Models.Supplier", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<Guid?>("EntityInformationId");

                    b.Property<Guid?>("InfoId");

                    b.HasKey("Id");

                    b.HasIndex("EntityInformationId");

                    b.HasIndex("InfoId");

                    b.ToTable("Suppliers");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Supplier");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Godsend.Models.SimpleArticle", b =>
                {
                    b.HasBaseType("Godsend.Models.Article");


                    b.ToTable("SimpleArticle");

                    b.HasDiscriminator().HasValue("SimpleArticle");
                });

            modelBuilder.Entity("Godsend.Models.PrimitiveData", b =>
                {
                    b.HasBaseType("Godsend.Models.Data");


                    b.ToTable("PrimitiveData");

                    b.HasDiscriminator().HasValue("PrimitiveData");
                });

            modelBuilder.Entity("Godsend.Models.ArticleInformation", b =>
                {
                    b.HasBaseType("Godsend.Models.Information");

                    b.Property<DateTime>("Created");

                    b.Property<string>("EFAuthorId");

                    b.HasIndex("EFAuthorId");

                    b.ToTable("ArticleInformation");

                    b.HasDiscriminator().HasValue("ArticleInformation");
                });

            modelBuilder.Entity("Godsend.Models.ProductInformation", b =>
                {
                    b.HasBaseType("Godsend.Models.Information");

                    b.Property<string>("Description");

                    b.ToTable("ProductInformation");

                    b.HasDiscriminator().HasValue("ProductInformation");
                });

            modelBuilder.Entity("Godsend.Models.SupplierInformation", b =>
                {
                    b.HasBaseType("Godsend.Models.Information");

                    b.Property<Guid?>("LocationId");

                    b.HasIndex("LocationId");

                    b.ToTable("SupplierInformation");

                    b.HasDiscriminator().HasValue("SupplierInformation");
                });

            modelBuilder.Entity("Godsend.Models.SimpleOrder", b =>
                {
                    b.HasBaseType("Godsend.Models.Order");


                    b.ToTable("SimpleOrder");

                    b.HasDiscriminator().HasValue("SimpleOrder");
                });

            modelBuilder.Entity("Godsend.Models.DiscreteProduct", b =>
                {
                    b.HasBaseType("Godsend.Models.Product");


                    b.ToTable("DiscreteProduct");

                    b.HasDiscriminator().HasValue("DiscreteProduct");
                });

            modelBuilder.Entity("Godsend.Models.WeightedProduct", b =>
                {
                    b.HasBaseType("Godsend.Models.Product");


                    b.ToTable("WeightedProduct");

                    b.HasDiscriminator().HasValue("WeightedProduct");
                });

            modelBuilder.Entity("Godsend.Models.SimpleSupplier", b =>
                {
                    b.HasBaseType("Godsend.Models.Supplier");


                    b.ToTable("SimpleSupplier");

                    b.HasDiscriminator().HasValue("SimpleSupplier");
                });

            modelBuilder.Entity("Godsend.Models.Article", b =>
                {
                    b.HasOne("Godsend.Models.Information", "EntityInformation")
                        .WithMany()
                        .HasForeignKey("EntityInformationId");

                    b.HasOne("Godsend.Models.ArticleInformation", "Info")
                        .WithMany()
                        .HasForeignKey("InfoId");
                });

            modelBuilder.Entity("Godsend.Models.Column", b =>
                {
                    b.HasOne("Godsend.Models.Directory")
                        .WithMany("Columns")
                        .HasForeignKey("DirectoryId");
                });

            modelBuilder.Entity("Godsend.Models.Data", b =>
                {
                    b.HasOne("Godsend.Models.Column")
                        .WithMany("Cells")
                        .HasForeignKey("ColumnId");

                    b.HasOne("Godsend.Models.PrimitiveData", "RealData")
                        .WithOne("Cell")
                        .HasForeignKey("Godsend.Models.Data", "RealDataId");
                });

            modelBuilder.Entity("Godsend.Models.Directory", b =>
                {
                    b.HasOne("Godsend.Models.Directory", "Base")
                        .WithMany()
                        .HasForeignKey("BaseId");
                });

            modelBuilder.Entity("Godsend.Models.LinkProductsSuppliers", b =>
                {
                    b.HasOne("Godsend.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Godsend.Models.Supplier", "Supplier")
                        .WithMany()
                        .HasForeignKey("SupplierId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Godsend.Models.Order", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "EFCustomer")
                        .WithMany()
                        .HasForeignKey("EFCustomerId");
                });

            modelBuilder.Entity("Godsend.Models.OrderPartDiscrete", b =>
                {
                    b.HasOne("Godsend.Models.Order")
                        .WithMany("DiscreteItems")
                        .HasForeignKey("OrderId");

                    b.HasOne("Godsend.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Godsend.Models.Supplier", "Supplier")
                        .WithMany()
                        .HasForeignKey("SupplierId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Godsend.Models.OrderPartWeighted", b =>
                {
                    b.HasOne("Godsend.Models.Order")
                        .WithMany("WeightedItems")
                        .HasForeignKey("OrderId");

                    b.HasOne("Godsend.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Godsend.Models.Supplier", "Supplier")
                        .WithMany()
                        .HasForeignKey("SupplierId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Godsend.Models.Product", b =>
                {
                    b.HasOne("Godsend.Models.Information", "EntityInformation")
                        .WithMany()
                        .HasForeignKey("EntityInformationId");

                    b.HasOne("Godsend.Models.ProductInformation", "Info")
                        .WithMany()
                        .HasForeignKey("InfoId");
                });

            modelBuilder.Entity("Godsend.Models.StringWrapper", b =>
                {
                    b.HasOne("Godsend.Models.ArticleInformation")
                        .WithMany("EFTags")
                        .HasForeignKey("ArticleInformationId");
                });

            modelBuilder.Entity("Godsend.Models.Supplier", b =>
                {
                    b.HasOne("Godsend.Models.Information", "EntityInformation")
                        .WithMany()
                        .HasForeignKey("EntityInformationId");

                    b.HasOne("Godsend.Models.SupplierInformation", "Info")
                        .WithMany()
                        .HasForeignKey("InfoId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Godsend.Models.ArticleInformation", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "EFAuthor")
                        .WithMany()
                        .HasForeignKey("EFAuthorId");
                });

            modelBuilder.Entity("Godsend.Models.SupplierInformation", b =>
                {
                    b.HasOne("Godsend.Models.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId");
                });
#pragma warning restore 612, 618
        }
    }
}
