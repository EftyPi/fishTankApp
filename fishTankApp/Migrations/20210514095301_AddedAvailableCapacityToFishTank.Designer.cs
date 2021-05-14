﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using fishTankApp.Database.Context;

namespace fishTankApp.Migrations
{
    [DbContext(typeof(FishTankContext))]
    [Migration("20210514095301_AddedAvailableCapacityToFishTank")]
    partial class AddedAvailableCapacityToFishTank
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("fishTankApp.Models.Breed", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("Size")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Breed");
                });

            modelBuilder.Entity("fishTankApp.Models.Decoration", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Colour")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("FishTankId")
                        .HasColumnType("int");

                    b.Property<int>("Size")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("FishTankId");

                    b.ToTable("Decoration");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Decoration");
                });

            modelBuilder.Entity("fishTankApp.Models.Fish", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BreedId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<int?>("FishTankId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("Size")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BreedId");

                    b.HasIndex("FishTankId");

                    b.ToTable("Fish");
                });

            modelBuilder.Entity("fishTankApp.Models.FishTank", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AvailableCapacity")
                        .HasColumnType("int");

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("FishTank");
                });

            modelBuilder.Entity("fishTankApp.Models.Plant", b =>
                {
                    b.HasBaseType("fishTankApp.Models.Decoration");

                    b.HasDiscriminator().HasValue("Plant");
                });

            modelBuilder.Entity("fishTankApp.Models.Decoration", b =>
                {
                    b.HasOne("fishTankApp.Models.FishTank", null)
                        .WithMany("Items")
                        .HasForeignKey("FishTankId");
                });

            modelBuilder.Entity("fishTankApp.Models.Fish", b =>
                {
                    b.HasOne("fishTankApp.Models.Breed", "Breed")
                        .WithMany()
                        .HasForeignKey("BreedId");

                    b.HasOne("fishTankApp.Models.FishTank", null)
                        .WithMany("Fishes")
                        .HasForeignKey("FishTankId");

                    b.Navigation("Breed");
                });

            modelBuilder.Entity("fishTankApp.Models.FishTank", b =>
                {
                    b.Navigation("Fishes");

                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
