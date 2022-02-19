﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SV.RDW.Migrations.MySQL;

#nullable disable

namespace SV.RDW.Migrations.MySQL.Migrations
{
    [DbContext(typeof(MySQLContext))]
    [Migration("20220219112632_ImportVoertuigenFK")]
    partial class ImportVoertuigenFK
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("SV.RDW.Data.Entities.Handelsbenaming", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<int>("MerkId")
                        .HasColumnType("int")
                        .HasColumnName("merkId");

                    b.Property<string>("Naam")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("naam");

                    b.HasKey("Id");

                    b.HasIndex("MerkId");

                    b.ToTable("handelsbenamingen");
                });

            modelBuilder.Entity("SV.RDW.Data.Entities.Import", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<DateTime>("EersteToelatingDatum")
                        .HasColumnType("DATE")
                        .HasColumnName("eersteToelatingDatum");

                    b.Property<decimal>("ImportSeconden")
                        .HasColumnType("decimal(65,30)")
                        .HasColumnName("importSeconden");

                    b.Property<int>("TotaalImport")
                        .HasColumnType("int")
                        .HasColumnName("totaalImport");

                    b.HasKey("Id");

                    b.ToTable("imports");
                });

            modelBuilder.Entity("SV.RDW.Data.Entities.Merk", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<string>("Naam")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("naam");

                    b.HasKey("Id");

                    b.ToTable("merken");
                });

            modelBuilder.Entity("SV.RDW.Data.Entities.Voertuig", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<DateTime>("EersteToelating")
                        .HasColumnType("DATE")
                        .HasColumnName("eersteToelating");

                    b.Property<int>("HandelsbenamingId")
                        .HasColumnType("int")
                        .HasColumnName("handelsbenamingId");

                    b.Property<int?>("ImportId")
                        .HasColumnType("int")
                        .HasColumnName("importId");

                    b.Property<string>("Inrichting")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("inrichting");

                    b.Property<string>("Kenteken")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)")
                        .HasColumnName("kenteken");

                    b.Property<string>("Kleur")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("kleur")
                        .HasComment("Hier staat de kleur.");

                    b.Property<decimal?>("MassaLedig")
                        .HasColumnType("decimal(65,30)")
                        .HasColumnName("massaLedig");

                    b.Property<int>("MerkId")
                        .HasColumnType("int")
                        .HasColumnName("merkId");

                    b.Property<DateTime>("Tenaamstelling")
                        .HasColumnType("DATE")
                        .HasColumnName("tenaamstelling");

                    b.Property<DateTime?>("VervalDatumAPK")
                        .HasColumnType("DATE")
                        .HasColumnName("vervalDatumAPK");

                    b.Property<int>("VoertuigSoortId")
                        .HasColumnType("int")
                        .HasColumnName("voertuigSoortId");

                    b.HasKey("Id");

                    b.HasIndex("HandelsbenamingId");

                    b.HasIndex("ImportId");

                    b.HasIndex("MerkId");

                    b.HasIndex("VoertuigSoortId");

                    b.ToTable("voertuigen");
                });

            modelBuilder.Entity("SV.RDW.Data.Entities.VoertuigSoort", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<string>("Naam")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("naam");

                    b.HasKey("Id");

                    b.ToTable("voertuigSoorten");
                });

            modelBuilder.Entity("SV.RDW.Data.Entities.Handelsbenaming", b =>
                {
                    b.HasOne("SV.RDW.Data.Entities.Merk", "Merk")
                        .WithMany("Handelsbenamingen")
                        .HasForeignKey("MerkId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Merk");
                });

            modelBuilder.Entity("SV.RDW.Data.Entities.Voertuig", b =>
                {
                    b.HasOne("SV.RDW.Data.Entities.Handelsbenaming", "Handelsbenaming")
                        .WithMany("Voertuigen")
                        .HasForeignKey("HandelsbenamingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SV.RDW.Data.Entities.Import", "Import")
                        .WithMany("Voertuigen")
                        .HasForeignKey("ImportId");

                    b.HasOne("SV.RDW.Data.Entities.Merk", "Merk")
                        .WithMany("Voertuigen")
                        .HasForeignKey("MerkId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SV.RDW.Data.Entities.VoertuigSoort", "VoertuigSoort")
                        .WithMany("Voertuigen")
                        .HasForeignKey("VoertuigSoortId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Handelsbenaming");

                    b.Navigation("Import");

                    b.Navigation("Merk");

                    b.Navigation("VoertuigSoort");
                });

            modelBuilder.Entity("SV.RDW.Data.Entities.Handelsbenaming", b =>
                {
                    b.Navigation("Voertuigen");
                });

            modelBuilder.Entity("SV.RDW.Data.Entities.Import", b =>
                {
                    b.Navigation("Voertuigen");
                });

            modelBuilder.Entity("SV.RDW.Data.Entities.Merk", b =>
                {
                    b.Navigation("Handelsbenamingen");

                    b.Navigation("Voertuigen");
                });

            modelBuilder.Entity("SV.RDW.Data.Entities.VoertuigSoort", b =>
                {
                    b.Navigation("Voertuigen");
                });
#pragma warning restore 612, 618
        }
    }
}
