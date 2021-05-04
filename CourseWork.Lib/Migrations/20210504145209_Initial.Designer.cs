﻿// <auto-generated />
using CourseWork.Lib;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CourseWork.Lib.Migrations
{
    [DbContext(typeof(CWContext))]
    [Migration("20210504145209_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CourseWork.Lib.Entities.Course", b =>
                {
                    b.Property<int>("ID")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Hours")
                        .HasColumnType("int");

                    b.Property<float>("Rating")
                        .HasColumnType("real");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("CourseWork.Lib.Entities.Course", b =>
                {
                    b.OwnsOne("CourseWork.Lib.Entities.PriceDetail", "PriceDetail", b1 =>
                        {
                            b1.Property<int>("CourseID")
                                .HasColumnType("int");

                            b1.Property<int>("Amount")
                                .HasColumnType("int");

                            b1.Property<string>("Currency")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("CurrencySymbol")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("PriceString")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("CourseID");

                            b1.ToTable("Courses");

                            b1.WithOwner()
                                .HasForeignKey("CourseID");
                        });

                    b.Navigation("PriceDetail");
                });
#pragma warning restore 612, 618
        }
    }
}