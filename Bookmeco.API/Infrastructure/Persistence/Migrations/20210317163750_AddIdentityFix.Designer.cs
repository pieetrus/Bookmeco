﻿// <auto-generated />
using System;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20210317163750_AddIdentityFix")]
    partial class AddIdentityFix
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.4");

            modelBuilder.Entity("CompanyCompanyCategory", b =>
                {
                    b.Property<int>("CategoriesId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CompaniesId")
                        .HasColumnType("INTEGER");

                    b.HasKey("CategoriesId", "CompaniesId");

                    b.HasIndex("CompaniesId");

                    b.ToTable("CompanyCompanyCategory");
                });

            modelBuilder.Entity("Domain.Entities.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<double>("Latitude")
                        .HasColumnType("REAL");

                    b.Property<double>("Longitude")
                        .HasColumnType("REAL");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("Domain.Entities.CompanyCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("TEXT");

                    b.Property<int?>("SuperCompanyCategoryId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("SuperCompanyCategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Domain.Entities.CompanyContent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CompanyId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId")
                        .IsUnique();

                    b.ToTable("CompanyContents");
                });

            modelBuilder.Entity("Domain.Entities.Opinion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<int?>("RateValue")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ReservationId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("SuperOpinionId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ReservationId");

                    b.HasIndex("SuperOpinionId");

                    b.HasIndex("UserId");

                    b.ToTable("Opinions");
                });

            modelBuilder.Entity("Domain.Entities.PersonData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.Property<int?>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("PersonsData");
                });

            modelBuilder.Entity("Domain.Entities.Reservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<int?>("PersonDataId")
                        .HasColumnType("INTEGER");

                    b.Property<float>("Prize")
                        .HasColumnType("REAL");

                    b.Property<int>("ReservationDuration")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ScheduleId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ServiceCategoryId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("PersonDataId");

                    b.HasIndex("ScheduleId");

                    b.HasIndex("ServiceCategoryId");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("Domain.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AccessLevel")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Domain.Entities.Schedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("TEXT");

                    b.Property<int?>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Schedules");
                });

            modelBuilder.Entity("Domain.Entities.ScheduleDay", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("BeginTime")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("Date")
                        .HasColumnType("TEXT");

                    b.Property<int>("DayOfWeek")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsRegular")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("MaxClients")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ScheduleId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ScheduleId")
                        .IsUnique();

                    b.ToTable("ScheduleDays");
                });

            modelBuilder.Entity("Domain.Entities.ServiceCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("TEXT");

                    b.Property<float>("Prize")
                        .HasColumnType("REAL");

                    b.Property<int>("ServiceDuration")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("ServiceCategory");
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CompanyId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("TEXT");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("TEXT");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Domain.Entities.UserCompany", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("AccessTypeId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("TEXT");

                    b.Property<int?>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("AccessTypeId");

                    b.HasIndex("UserId");

                    b.ToTable("UserCompanies");
                });

            modelBuilder.Entity("Domain.Entities.UserCompanyAccessType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AccessLevel")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("UserCompanyAccessTypes");
                });

            modelBuilder.Entity("RoleUser", b =>
                {
                    b.Property<int>("RolesId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UsersId")
                        .HasColumnType("INTEGER");

                    b.HasKey("RolesId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("RoleUser");
                });

            modelBuilder.Entity("ServiceCategoryUser", b =>
                {
                    b.Property<int>("ServiceCategoriesId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UsersId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ServiceCategoriesId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("ServiceCategoryUser");
                });

            modelBuilder.Entity("CompanyCompanyCategory", b =>
                {
                    b.HasOne("Domain.Entities.CompanyCategory", null)
                        .WithMany()
                        .HasForeignKey("CategoriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Company", null)
                        .WithMany()
                        .HasForeignKey("CompaniesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.CompanyCategory", b =>
                {
                    b.HasOne("Domain.Entities.CompanyCategory", "SuperCompanyCategory")
                        .WithMany()
                        .HasForeignKey("SuperCompanyCategoryId");

                    b.Navigation("SuperCompanyCategory");
                });

            modelBuilder.Entity("Domain.Entities.CompanyContent", b =>
                {
                    b.HasOne("Domain.Entities.Company", "Company")
                        .WithOne("Content")
                        .HasForeignKey("Domain.Entities.CompanyContent", "CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("Domain.Entities.Opinion", b =>
                {
                    b.HasOne("Domain.Entities.Reservation", "Reservation")
                        .WithMany("Opinions")
                        .HasForeignKey("ReservationId");

                    b.HasOne("Domain.Entities.Opinion", "SuperOpinion")
                        .WithMany()
                        .HasForeignKey("SuperOpinionId");

                    b.HasOne("Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("Reservation");

                    b.Navigation("SuperOpinion");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entities.PersonData", b =>
                {
                    b.HasOne("Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entities.Reservation", b =>
                {
                    b.HasOne("Domain.Entities.PersonData", "PersonData")
                        .WithMany()
                        .HasForeignKey("PersonDataId");

                    b.HasOne("Domain.Entities.Schedule", "Schedule")
                        .WithMany("Reservations")
                        .HasForeignKey("ScheduleId");

                    b.HasOne("Domain.Entities.ServiceCategory", "ServiceCategory")
                        .WithMany("Reservations")
                        .HasForeignKey("ServiceCategoryId");

                    b.Navigation("PersonData");

                    b.Navigation("Schedule");

                    b.Navigation("ServiceCategory");
                });

            modelBuilder.Entity("Domain.Entities.Schedule", b =>
                {
                    b.HasOne("Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entities.ScheduleDay", b =>
                {
                    b.HasOne("Domain.Entities.Schedule", "Schedule")
                        .WithOne("ScheduleDay")
                        .HasForeignKey("Domain.Entities.ScheduleDay", "ScheduleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Schedule");
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.HasOne("Domain.Entities.Company", null)
                        .WithMany("Users")
                        .HasForeignKey("CompanyId");
                });

            modelBuilder.Entity("Domain.Entities.UserCompany", b =>
                {
                    b.HasOne("Domain.Entities.UserCompanyAccessType", "AccessType")
                        .WithMany("UserCompanies")
                        .HasForeignKey("AccessTypeId");

                    b.HasOne("Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("AccessType");

                    b.Navigation("User");
                });

            modelBuilder.Entity("RoleUser", b =>
                {
                    b.HasOne("Domain.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RolesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ServiceCategoryUser", b =>
                {
                    b.HasOne("Domain.Entities.ServiceCategory", null)
                        .WithMany()
                        .HasForeignKey("ServiceCategoriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.Company", b =>
                {
                    b.Navigation("Content");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("Domain.Entities.Reservation", b =>
                {
                    b.Navigation("Opinions");
                });

            modelBuilder.Entity("Domain.Entities.Schedule", b =>
                {
                    b.Navigation("Reservations");

                    b.Navigation("ScheduleDay");
                });

            modelBuilder.Entity("Domain.Entities.ServiceCategory", b =>
                {
                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("Domain.Entities.UserCompanyAccessType", b =>
                {
                    b.Navigation("UserCompanies");
                });
#pragma warning restore 612, 618
        }
    }
}