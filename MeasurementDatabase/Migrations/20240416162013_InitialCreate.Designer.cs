﻿// <auto-generated />
using System;
using MeasurementDatabase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MeasurementDatabase.Migrations
{
    [DbContext(typeof(MeasurementDbContext))]
    [Migration("20240416162013_InitialCreate")]
    partial class InitialCreate
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

                    b.ToTable("Measurements");
                });
#pragma warning restore 612, 618
        }
    }
}
