﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PortfolioAPI.Database;

#nullable disable

namespace PortfolioAPI.Migrations
{
    [DbContext(typeof(APIDB))]
    partial class APIDBModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PortfolioAPI.Models.Investment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Bank_Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Investment_Amount")
                        .HasColumnType("float");

                    b.Property<DateTime>("Investment_Start_Date")
                        .HasColumnType("datetime2");

                    b.Property<double>("Maturity_Amount")
                        .HasColumnType("float");

                    b.Property<DateTime>("Maturity_Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ROI")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Time_Left_To_Mature")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UserId1")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("lastUpdate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    SqlServerKeyBuilderExtensions.IsClustered(b.HasKey("Id"), false);

                    b.HasIndex("UserId");

                    b.HasIndex("UserId1");

                    b.ToTable("investments");
                });

            modelBuilder.Entity("PortfolioAPI.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<double?>("Banking_return")
                        .HasColumnType("float");

                    b.Property<DateTime?>("DOJ")
                        .HasColumnType("datetime2");

                    b.Property<string>("EmailId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Last_update_date")
                        .HasColumnType("datetime2");

                    b.Property<double?>("Miscellaneous_return")
                        .HasColumnType("float");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("SIP_return")
                        .HasColumnType("float");

                    b.Property<double?>("Salary")
                        .HasColumnType("float");

                    b.Property<double?>("Stock_return")
                        .HasColumnType("float");

                    b.Property<double?>("Total_savings")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    SqlServerKeyBuilderExtensions.IsClustered(b.HasKey("Id"), false);

                    b.ToTable("users");
                });

            modelBuilder.Entity("PortfolioAPI.Models.Investment", b =>
                {
                    b.HasOne("PortfolioAPI.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PortfolioAPI.Models.User", null)
                        .WithMany("Investments")
                        .HasForeignKey("UserId1");
                });

            modelBuilder.Entity("PortfolioAPI.Models.User", b =>
                {
                    b.Navigation("Investments");
                });
#pragma warning restore 612, 618
        }
    }
}
