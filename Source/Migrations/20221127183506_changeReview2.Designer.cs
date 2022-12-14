// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VladimirTripAdvisor.Data;

#nullable disable

namespace VladimirTripAdvisor.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20221127183506_changeReview2")]
    partial class changeReview2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("VladimirTripAdvisor.Models.Image", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<byte[]>("ImageByte")
                        .IsRequired()
                        .HasColumnType("longblob")
                        .HasColumnName("image_byte");

                    b.Property<long>("ObjectId")
                        .HasColumnType("bigint")
                        .HasColumnName("id_object");

                    b.HasKey("Id");

                    b.HasIndex("ObjectId");

                    b.ToTable("image_object");
                });

            modelBuilder.Entity("VladimirTripAdvisor.Models.ObjectOfVisit", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("AdditionalAddressInfo")
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)")
                        .HasColumnName("additional_address_info");

                    b.Property<long?>("IdOwner")
                        .HasColumnType("bigint")
                        .HasColumnName("id_owner");

                    b.Property<string>("Latitude")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Longitude")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.Property<string>("PlaceDescription")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("varchar(1000)")
                        .HasColumnName("place_description");

                    b.Property<int>("PlaceType")
                        .HasColumnType("int")
                        .HasColumnName("place_type");

                    b.Property<string>("StreetAddress")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)")
                        .HasColumnName("street_address");

                    b.HasKey("Id");

                    b.HasIndex("IdOwner");

                    b.ToTable("object_of_visit");
                });

            modelBuilder.Entity("VladimirTripAdvisor.Models.Review", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long>("ObjectId")
                        .HasColumnType("bigint")
                        .HasColumnName("id_object");

                    b.Property<string>("ReviewDescription")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)")
                        .HasColumnName("review_description");

                    b.Property<int>("Score")
                        .HasColumnType("int")
                        .HasColumnName("score");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("id_user");

                    b.HasKey("Id");

                    b.HasIndex("ObjectId");

                    b.HasIndex("UserId");

                    b.ToTable("Review");
                });

            modelBuilder.Entity("VladimirTripAdvisor.Models.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<int>("AccountType")
                        .HasColumnType("int");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("VladimirTripAdvisor.Models.Image", b =>
                {
                    b.HasOne("VladimirTripAdvisor.Models.ObjectOfVisit", "ObjectOfVisit")
                        .WithMany()
                        .HasForeignKey("ObjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ObjectOfVisit");
                });

            modelBuilder.Entity("VladimirTripAdvisor.Models.ObjectOfVisit", b =>
                {
                    b.HasOne("VladimirTripAdvisor.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("IdOwner");

                    b.Navigation("User");
                });

            modelBuilder.Entity("VladimirTripAdvisor.Models.Review", b =>
                {
                    b.HasOne("VladimirTripAdvisor.Models.ObjectOfVisit", "ObjectOfVisit")
                        .WithMany()
                        .HasForeignKey("ObjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VladimirTripAdvisor.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ObjectOfVisit");

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
