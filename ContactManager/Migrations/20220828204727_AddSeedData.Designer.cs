// <auto-generated />
using System;
using ContactManager.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ContactManager.Migrations
{
    [DbContext(typeof(ContactManagerDbContext))]
    [Migration("20220828204727_AddSeedData")]
    partial class AddSeedData
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ContactManager.DTOs.CompanyVendorDTO", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("VendorCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("CompanyName")
                        .IsUnique();

                    b.HasIndex("VendorCode")
                        .IsUnique();

                    b.ToTable("VendorMasterList");

                    b.HasData(
                        new
                        {
                            Id = new Guid("f2794e3b-7972-483f-88fa-54b1865e0776"),
                            CompanyName = "ACME Acids",
                            VendorCode = "A001"
                        },
                        new
                        {
                            Id = new Guid("13671055-baaa-420a-8341-c0e47a071bb6"),
                            CompanyName = "Berenstain Biology",
                            VendorCode = "A002"
                        },
                        new
                        {
                            Id = new Guid("0ab5eb65-958b-4b7c-9e7f-8b3657f4426a"),
                            CompanyName = "Flick’s Fluidics",
                            VendorCode = "A003"
                        },
                        new
                        {
                            Id = new Guid("68242db4-6242-47be-94dd-b83ec163c0dc"),
                            CompanyName = "Radical Reagents",
                            VendorCode = "D004"
                        },
                        new
                        {
                            Id = new Guid("8c8dccc4-5e41-4012-b390-3b661524763f"),
                            CompanyName = "BBST Paper Products",
                            VendorCode = "G065"
                        });
                });

            modelBuilder.Entity("ContactManager.DTOs.CustomerDTO", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Company")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Customer");

                    b.HasData(
                        new
                        {
                            Id = new Guid("360f55f3-b7b8-4e4e-9535-1772e1047c9b"),
                            Address = "1 Amphitheater Parkway, Mountain View, CA",
                            Company = "Google",
                            Name = "Scottie Scheffler",
                            Notes = "Interested in ddPCR.",
                            Phone = "(510)555-3282"
                        },
                        new
                        {
                            Id = new Guid("0f243a4b-207c-4eb6-b4d5-53d5b1fddfad"),
                            Address = "3555 Farnam Street, Omaha, NE",
                            Company = "Berkshire Hathaway",
                            Name = "Warren Buffett",
                            Notes = "Deep pockets.",
                            Phone = "(510)555-8164"
                        },
                        new
                        {
                            Id = new Guid("520778df-8a95-436c-b7d6-619735d10918"),
                            Address = "13101 Harold Green Road, Austin, Texas",
                            Company = "Tesla",
                            Name = "Elon Musk",
                            Notes = "Also deep pockets.",
                            Phone = "(510)555-8912"
                        });
                });

            modelBuilder.Entity("ContactManager.DTOs.VendorDTO", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Company")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Vendor");

                    b.HasData(
                        new
                        {
                            Id = new Guid("a0420642-1599-46a3-9c52-6b6215346972"),
                            Address = "111 A Street, Benicia, CA",
                            Company = "Radical Reagents",
                            Name = "George W. Salesman",
                            Phone = "(510)555-1234"
                        },
                        new
                        {
                            Id = new Guid("cc0f7096-1635-47e1-8c29-74196c5bd6bd"),
                            Address = "222 B Street, Benicia, CA",
                            Company = "Berenstain Biology",
                            Name = "Stephanie Saleswoman",
                            Phone = "(510)555-3289"
                        },
                        new
                        {
                            Id = new Guid("b32e3a41-6dca-4730-9449-55784dcd72dc"),
                            Address = "333 C Street, Benicia, CA",
                            Company = "ACME Acids",
                            Name = "Jane Doe",
                            Phone = "(510)555-5439"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
