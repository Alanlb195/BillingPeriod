﻿// <auto-generated />
using System;
using BillingPeriod.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BillingPeriod.Migrations
{
    [DbContext(typeof(DefaultDBContext))]
    [Migration("20230713165343_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("BillingPeriod.Models.Activity", b =>
                {
                    b.Property<int>("IdActivity")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdActivity"), 1L, 1);

                    b.Property<DateTime>("FinalDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("InitialDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdActivity");

                    b.ToTable("Activity");
                });

            modelBuilder.Entity("BillingPeriod.Models.Note", b =>
                {
                    b.Property<int>("IdNote")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdNote"), 1L, 1);

                    b.Property<int>("IdActivity")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdNote");

                    b.HasIndex("IdActivity");

                    b.ToTable("Note");
                });

            modelBuilder.Entity("BillingPeriod.Models.Note", b =>
                {
                    b.HasOne("BillingPeriod.Models.Activity", "Activitie")
                        .WithMany("Notes")
                        .HasForeignKey("IdActivity")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Activitie");
                });

            modelBuilder.Entity("BillingPeriod.Models.Activity", b =>
                {
                    b.Navigation("Notes");
                });
#pragma warning restore 612, 618
        }
    }
}