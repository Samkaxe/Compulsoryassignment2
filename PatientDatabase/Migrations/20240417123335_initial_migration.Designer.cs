﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PatientDatabase;

#nullable disable

namespace PatientDatabase.Migrations
{
    [DbContext(typeof(PatientDbContext))]
    [Migration("20240417123335_initial_migration")]
    partial class initial_migration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.18");

            modelBuilder.Entity("Core.Measurements", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<int>("Diastolic")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PatientSSN")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Systolic")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("PatientSSN");

                    b.ToTable("Measurements");
                });

            modelBuilder.Entity("Core.Patient", b =>
                {
                    b.Property<string>("SSN")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("SSN");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("Core.Measurements", b =>
                {
                    b.HasOne("Core.Patient", null)
                        .WithMany("Measurements")
                        .HasForeignKey("PatientSSN")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Core.Patient", b =>
                {
                    b.Navigation("Measurements");
                });
#pragma warning restore 612, 618
        }
    }
}
