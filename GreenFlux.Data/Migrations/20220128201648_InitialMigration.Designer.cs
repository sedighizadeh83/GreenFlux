﻿// <auto-generated />
using GreenFlux.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GreenFlux.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20220128201648_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("GreenFlux.Data.Models.ChargeStation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.ToTable("ChargeStations");
                });

            modelBuilder.Entity("GreenFlux.Data.Models.Connector", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int>("ChargeStationId")
                        .HasColumnType("int");

                    b.Property<decimal>("MaxCurrent")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id", "ChargeStationId");

                    b.HasIndex("ChargeStationId");

                    b.ToTable("Connectors");
                });

            modelBuilder.Entity("GreenFlux.Data.Models.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<decimal>("Capacity")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("GreenFlux.Data.Models.ChargeStation", b =>
                {
                    b.HasOne("GreenFlux.Data.Models.Group", "Group")
                        .WithMany("ChargeStationCollection")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");
                });

            modelBuilder.Entity("GreenFlux.Data.Models.Connector", b =>
                {
                    b.HasOne("GreenFlux.Data.Models.ChargeStation", "ChargeStation")
                        .WithMany("ConnectorCollection")
                        .HasForeignKey("ChargeStationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ChargeStation");
                });

            modelBuilder.Entity("GreenFlux.Data.Models.ChargeStation", b =>
                {
                    b.Navigation("ConnectorCollection");
                });

            modelBuilder.Entity("GreenFlux.Data.Models.Group", b =>
                {
                    b.Navigation("ChargeStationCollection");
                });
#pragma warning restore 612, 618
        }
    }
}
