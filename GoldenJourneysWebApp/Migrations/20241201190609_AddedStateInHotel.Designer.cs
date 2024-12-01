﻿// <auto-generated />
using System;
using GoldenJourneysWebApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GoldenJourneysWebApp.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    [Migration("20241201190609_AddedStateInHotel")]
    partial class AddedStateInHotel
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GoldenJourneysWebApp.Data.Entities.BookingHotel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("SpecialRequest")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("TotalPrice")
                        .HasColumnType("float");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("BookingHotels");
                });

            modelBuilder.Entity("GoldenJourneysWebApp.Data.Entities.BookingTour", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("NumberOfPeople")
                        .HasColumnType("int");

                    b.Property<string>("SpecialRequest")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("TotalPrice")
                        .HasColumnType("float");

                    b.Property<int>("TourAvailabilityId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TourAvailabilityId");

                    b.HasIndex("UserId");

                    b.ToTable("BookingTours");
                });

            modelBuilder.Entity("GoldenJourneysWebApp.Data.Entities.Hotels", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateOnly>("Created")
                        .HasColumnType("date");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Stars")
                        .HasColumnType("int");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ThumbnailImageURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Hotels");
                });

            modelBuilder.Entity("GoldenJourneysWebApp.Data.Entities.RoomAvailability", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateOnly>("AvailableDate")
                        .HasColumnType("date");

                    b.Property<int>("AvailableQty")
                        .HasColumnType("int");

                    b.Property<int>("OriginalQty")
                        .HasColumnType("int");

                    b.Property<int>("RoomId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoomId");

                    b.ToTable("RoomsAvailability");
                });

            modelBuilder.Entity("GoldenJourneysWebApp.Data.Entities.RoomBook", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BookingHotelId")
                        .HasColumnType("int");

                    b.Property<int>("RoomAvailabilityId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BookingHotelId");

                    b.HasIndex("RoomAvailabilityId");

                    b.ToTable("BookingBook");
                });

            modelBuilder.Entity("GoldenJourneysWebApp.Data.Entities.RoomMediaContent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("MediaPathURL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoomId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("RoomId");

                    b.ToTable("RoomsMediaContents");
                });

            modelBuilder.Entity("GoldenJourneysWebApp.Data.Entities.Rooms", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<DateOnly>("Created")
                        .HasColumnType("date");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("HotelId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("HotelId");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("GoldenJourneysWebApp.Data.Entities.TourAvailability", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AvailableCapacity")
                        .HasColumnType("int");

                    b.Property<DateOnly>("AvailableDate")
                        .HasColumnType("date");

                    b.Property<int>("OriginalCapacity")
                        .HasColumnType("int");

                    b.Property<int>("TourId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TourId");

                    b.ToTable("TourAvailability");
                });

            modelBuilder.Entity("GoldenJourneysWebApp.Data.Entities.Tours", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateOnly>("Created")
                        .HasColumnType("date");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Tours");
                });

            modelBuilder.Entity("GoldenJourneysWebApp.Data.Entities.ToursMediaContent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("MediaPathURL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TourId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TourId");

                    b.ToTable("ToursMediaContents");
                });

            modelBuilder.Entity("GoldenJourneysWebApp.Data.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateOnly>("CreatedAt")
                        .HasColumnType("date");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("UserTypeId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("GoldenJourneysWebApp.Data.Entities.UserType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("UserTypes");
                });

            modelBuilder.Entity("GoldenJourneysWebApp.Data.Entities.BookingHotel", b =>
                {
                    b.HasOne("GoldenJourneysWebApp.Data.Entities.User", "User")
                        .WithMany("BookingHotel")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("GoldenJourneysWebApp.Data.Entities.BookingTour", b =>
                {
                    b.HasOne("GoldenJourneysWebApp.Data.Entities.TourAvailability", "TourAvailability")
                        .WithMany("BookingTour")
                        .HasForeignKey("TourAvailabilityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GoldenJourneysWebApp.Data.Entities.User", "User")
                        .WithMany("BookingTour")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TourAvailability");

                    b.Navigation("User");
                });

            modelBuilder.Entity("GoldenJourneysWebApp.Data.Entities.RoomAvailability", b =>
                {
                    b.HasOne("GoldenJourneysWebApp.Data.Entities.Rooms", "Rooms")
                        .WithMany("RoomAvailability")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Rooms");
                });

            modelBuilder.Entity("GoldenJourneysWebApp.Data.Entities.RoomBook", b =>
                {
                    b.HasOne("GoldenJourneysWebApp.Data.Entities.BookingHotel", "BookingHotel")
                        .WithMany("RoomBook")
                        .HasForeignKey("BookingHotelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GoldenJourneysWebApp.Data.Entities.RoomAvailability", "RoomAvailability")
                        .WithMany("RoomBook")
                        .HasForeignKey("RoomAvailabilityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BookingHotel");

                    b.Navigation("RoomAvailability");
                });

            modelBuilder.Entity("GoldenJourneysWebApp.Data.Entities.RoomMediaContent", b =>
                {
                    b.HasOne("GoldenJourneysWebApp.Data.Entities.Rooms", "Rooms")
                        .WithMany("RoomMediaContent")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Rooms");
                });

            modelBuilder.Entity("GoldenJourneysWebApp.Data.Entities.Rooms", b =>
                {
                    b.HasOne("GoldenJourneysWebApp.Data.Entities.Hotels", "Hotels")
                        .WithMany("Rooms")
                        .HasForeignKey("HotelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hotels");
                });

            modelBuilder.Entity("GoldenJourneysWebApp.Data.Entities.TourAvailability", b =>
                {
                    b.HasOne("GoldenJourneysWebApp.Data.Entities.Tours", "Tours")
                        .WithMany("TourAvailability")
                        .HasForeignKey("TourId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tours");
                });

            modelBuilder.Entity("GoldenJourneysWebApp.Data.Entities.ToursMediaContent", b =>
                {
                    b.HasOne("GoldenJourneysWebApp.Data.Entities.Tours", "Tours")
                        .WithMany("ToursMediaContent")
                        .HasForeignKey("TourId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tours");
                });

            modelBuilder.Entity("GoldenJourneysWebApp.Data.Entities.User", b =>
                {
                    b.HasOne("GoldenJourneysWebApp.Data.Entities.UserType", "UserType")
                        .WithMany("Users")
                        .HasForeignKey("UserTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserType");
                });

            modelBuilder.Entity("GoldenJourneysWebApp.Data.Entities.BookingHotel", b =>
                {
                    b.Navigation("RoomBook");
                });

            modelBuilder.Entity("GoldenJourneysWebApp.Data.Entities.Hotels", b =>
                {
                    b.Navigation("Rooms");
                });

            modelBuilder.Entity("GoldenJourneysWebApp.Data.Entities.RoomAvailability", b =>
                {
                    b.Navigation("RoomBook");
                });

            modelBuilder.Entity("GoldenJourneysWebApp.Data.Entities.Rooms", b =>
                {
                    b.Navigation("RoomAvailability");

                    b.Navigation("RoomMediaContent");
                });

            modelBuilder.Entity("GoldenJourneysWebApp.Data.Entities.TourAvailability", b =>
                {
                    b.Navigation("BookingTour");
                });

            modelBuilder.Entity("GoldenJourneysWebApp.Data.Entities.Tours", b =>
                {
                    b.Navigation("TourAvailability");

                    b.Navigation("ToursMediaContent");
                });

            modelBuilder.Entity("GoldenJourneysWebApp.Data.Entities.User", b =>
                {
                    b.Navigation("BookingHotel");

                    b.Navigation("BookingTour");
                });

            modelBuilder.Entity("GoldenJourneysWebApp.Data.Entities.UserType", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
