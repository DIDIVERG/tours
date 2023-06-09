﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Second;

#nullable disable

namespace Second.Migrations
{
    [DbContext(typeof(ToursContext))]
    [Migration("20230501190452_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0-preview.3.23174.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Second.Models.Payment", b =>
                {
                    b.Property<int>("PaymentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("payment_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("PaymentId"));

                    b.Property<DateTime>("PaymentDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("payment_date");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric")
                        .HasColumnName("price");

                    b.Property<int>("VoucherId")
                        .HasColumnType("integer")
                        .HasColumnName("voucher_id");

                    b.HasKey("PaymentId")
                        .HasName("pk_payments");

                    b.HasIndex("VoucherId")
                        .HasDatabaseName("ix_payments_voucher_id");

                    b.ToTable("payments", (string)null);
                });

            modelBuilder.Entity("Second.Models.Season", b =>
                {
                    b.Property<int>("SeasonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("season_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("SeasonId"));

                    b.Property<bool>("Closed")
                        .HasColumnType("boolean")
                        .HasColumnName("closed");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("end_date");

                    b.Property<int>("SeatAmount")
                        .HasColumnType("integer")
                        .HasColumnName("seat_amount");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("start_date");

                    b.Property<int>("TourId")
                        .HasColumnType("integer")
                        .HasColumnName("tour_id");

                    b.HasKey("SeasonId")
                        .HasName("pk_seasons");

                    b.HasIndex("TourId")
                        .HasDatabaseName("ix_seasons_tour_id");

                    b.ToTable("seasons", (string)null);
                });

            modelBuilder.Entity("Second.Models.Tour", b =>
                {
                    b.Property<int>("TourId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("tour_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("TourId"));

                    b.Property<string>("Info")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("info");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric")
                        .HasColumnName("price");

                    b.HasKey("TourId")
                        .HasName("pk_tours");

                    b.ToTable("tours", (string)null);
                });

            modelBuilder.Entity("Second.Models.Tourist", b =>
                {
                    b.Property<int>("TouristId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("tourist_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("TouristId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("Patronymic")
                        .HasColumnType("text")
                        .HasColumnName("patronymic");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("surname");

                    b.HasKey("TouristId")
                        .HasName("pk_tourists");

                    b.ToTable("tourists", (string)null);
                });

            modelBuilder.Entity("Second.Models.TouristInfo", b =>
                {
                    b.Property<int>("TouristInfoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("tourist_info_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("TouristInfoId"));

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("city");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("country");

                    b.Property<string>("Passport")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("passport");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("phone");

                    b.Property<int>("TouristId")
                        .HasColumnType("integer")
                        .HasColumnName("tourist_id");

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("zip_code");

                    b.HasKey("TouristInfoId")
                        .HasName("pk_tourist_infos");

                    b.HasIndex("TouristId")
                        .HasDatabaseName("ix_tourist_infos_tourist_id");

                    b.ToTable("tourist_infos", (string)null);
                });

            modelBuilder.Entity("Second.Models.Voucher", b =>
                {
                    b.Property<int>("VoucherId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("voucher_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("VoucherId"));

                    b.Property<int>("SeasonId")
                        .HasColumnType("integer")
                        .HasColumnName("season_id");

                    b.Property<int>("TouristId")
                        .HasColumnType("integer")
                        .HasColumnName("tourist_id");

                    b.HasKey("VoucherId")
                        .HasName("pk_vouchers");

                    b.HasIndex("SeasonId")
                        .HasDatabaseName("ix_vouchers_season_id");

                    b.HasIndex("TouristId")
                        .HasDatabaseName("ix_vouchers_tourist_id");

                    b.ToTable("vouchers", (string)null);
                });

            modelBuilder.Entity("Second.Models.Payment", b =>
                {
                    b.HasOne("Second.Models.Voucher", "Voucher")
                        .WithMany("Payments")
                        .HasForeignKey("VoucherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_payments_vouchers_voucher_id");

                    b.Navigation("Voucher");
                });

            modelBuilder.Entity("Second.Models.Season", b =>
                {
                    b.HasOne("Second.Models.Tour", "Tour")
                        .WithMany("Seasons")
                        .HasForeignKey("TourId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_seasons_tours_tour_id");

                    b.Navigation("Tour");
                });

            modelBuilder.Entity("Second.Models.TouristInfo", b =>
                {
                    b.HasOne("Second.Models.Tourist", "Tourist")
                        .WithMany("TouristInfos")
                        .HasForeignKey("TouristId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_tourist_infos_tourists_tourist_id");

                    b.Navigation("Tourist");
                });

            modelBuilder.Entity("Second.Models.Voucher", b =>
                {
                    b.HasOne("Second.Models.Season", "Season")
                        .WithMany("Vouchers")
                        .HasForeignKey("SeasonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_vouchers_seasons_season_id");

                    b.HasOne("Second.Models.TouristInfo", "TouristInfo")
                        .WithMany("Vouchers")
                        .HasForeignKey("TouristId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_vouchers_tourist_infos_tourist_id");

                    b.Navigation("Season");

                    b.Navigation("TouristInfo");
                });

            modelBuilder.Entity("Second.Models.Season", b =>
                {
                    b.Navigation("Vouchers");
                });

            modelBuilder.Entity("Second.Models.Tour", b =>
                {
                    b.Navigation("Seasons");
                });

            modelBuilder.Entity("Second.Models.Tourist", b =>
                {
                    b.Navigation("TouristInfos");
                });

            modelBuilder.Entity("Second.Models.TouristInfo", b =>
                {
                    b.Navigation("Vouchers");
                });

            modelBuilder.Entity("Second.Models.Voucher", b =>
                {
                    b.Navigation("Payments");
                });
#pragma warning restore 612, 618
        }
    }
}
