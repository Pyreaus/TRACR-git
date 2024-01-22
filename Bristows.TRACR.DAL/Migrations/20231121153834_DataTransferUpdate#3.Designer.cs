﻿// <auto-generated />
using System;
using Bristows.TRACR.Model.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Bristows.TRACR.DAL.Migrations
{
    [DbContext(typeof(TRACRContext))]
    [Migration("20231121153834_DataTransferUpdate#3")]
    partial class DataTransferUpdate3
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Bristows.TRACR.Model.Models.Entities.Admin", b =>
                {
                    b.Property<int>("AID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AID"));

                    b.Property<string>("ACTIVE_USER")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ADMIN")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("EMAIL")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("FULL_NAME")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ROLE")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("WINUSER")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("AID");

                    b.ToTable("ADMINS", "dbo");
                });

            modelBuilder.Entity("Bristows.TRACR.Model.Models.Entities.Diary", b =>
                {
                    b.Property<int>("DIARY_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DIARY_ID"));

                    b.Property<string>("LEARNING_POINTS")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PFID")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PRACTICE_AREA")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("PROFESSIONAL_CONDUCT_ISSUES")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PROFESSIONAL_DEVELOPMENT_UNDERTAKEN")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SHOW")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("SIGNED_OFF_BY")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime?>("SIGNED_OFF_TIMESTAMP")
                        .HasColumnType("datetime2");

                    b.Property<string>("SIGN_OFF_SUBMITTED")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("TIMESTAMP")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("WEEK_BEGINNING")
                        .HasColumnType("datetime2");

                    b.HasKey("DIARY_ID");

                    b.ToTable("DIARY", "dbo");
                });

            modelBuilder.Entity("Bristows.TRACR.Model.Models.Entities.DiaryTask", b =>
                {
                    b.Property<int>("DIARY_TASK_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DIARY_TASK_ID"));

                    b.Property<int>("DIARY_ID")
                        .HasColumnType("int");

                    b.Property<string>("FEE_EARNERS")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MATTER")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("SHOW")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("SKILLS")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TASK_DESCRIPTION")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("TIMESTAMP")
                        .HasColumnType("datetime2");

                    b.HasKey("DIARY_TASK_ID");

                    b.ToTable("DIARY_TASKS", "dbo");
                });

            modelBuilder.Entity("Bristows.TRACR.Model.Models.Entities.Employees.Employee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<DateTime?>("EntryCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LastUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<bool?>("Show")
                        .IsRequired()
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Employee", "dbo");
                });

            modelBuilder.Entity("Bristows.TRACR.Model.Models.Entities.PeopleFinderUser", b =>
                {
                    b.Property<string>("About")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("ActiveUser")
                        .HasColumnType("bit");

                    b.Property<int?>("CreatorID")
                        .HasColumnType("int");

                    b.Property<int?>("DepartmentID")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("FeeEarner")
                        .HasColumnType("bit");

                    b.Property<decimal?>("FeeEarnerChargeOutRate")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("Human")
                        .HasColumnType("bit");

                    b.Property<string>("Initials")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("JobTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("JoinDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LeaveDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("LocationID")
                        .HasColumnType("int");

                    b.Property<DateTime?>("MaternityDate")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("MaternityDirection")
                        .HasColumnType("bit");

                    b.Property<string>("Mobile")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PFID")
                        .HasMaxLength(3)
                        .HasColumnType("int");

                    b.Property<bool?>("Partner")
                        .HasColumnType("bit");

                    b.Property<string>("Photo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PronouncedAs")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RoomID")
                        .HasColumnType("int");

                    b.Property<DateTime?>("SabbaticalBeginDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("SabbaticalEndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("SecondmentCompanyName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("SecondmentDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("SecretaryID")
                        .HasColumnType("int");

                    b.Property<bool?>("Show")
                        .HasColumnType("bit");

                    b.Property<string>("Telephone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Timestamped")
                        .HasColumnType("datetime2");

                    b.Property<string>("Upn")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WinUser")
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("PFUser", "dbo");
                });

            modelBuilder.Entity("Bristows.TRACR.Model.Models.Entities.Skill", b =>
                {
                    b.Property<int?>("SKILL_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("SKILL_ID"));

                    b.Property<string>("COLOUR")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SHOW")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SKILL_DESCRIPTION")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("SKILL_NAME")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("SKILL_ID");

                    b.ToTable("SKILLS", "dbo");
                });

            modelBuilder.Entity("Bristows.TRACR.Model.Models.Entities.Trainee", b =>
                {
                    b.Property<int>("TRAINEE_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TRAINEE_ID"));

                    b.Property<string>("ACTIVE")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("OTHER_PFID")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("REVIEWER_PFID")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("SHOW")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("TRAINEE_PFID")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("TRAINEE_ID");

                    b.ToTable("TRAINEES", "dbo");
                });
#pragma warning restore 612, 618
        }
    }
}
