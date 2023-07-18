﻿// <auto-generated />
using System;
using Bristows.TRACR.Model.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Bristows.TRACR.DAL.Migrations
{
    [DbContext(typeof(TRACRContext))]
    partial class TRACRContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Bristows.TRACR.Model.Models.Entities.Admin", b =>
                {
                    b.Property<Guid?>("LocalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool?>("Active")
                        .HasColumnType("bit");

                    b.Property<int>("AdminId")
                        .HasMaxLength(15)
                        .HasColumnType("int");

                    b.Property<DateTime?>("EntryCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LastUpdated")
                        .HasColumnType("datetime2");

                    b.Property<int?>("OtherPfid")
                        .HasColumnType("int");

                    b.Property<int>("ReviewerPfid")
                        .HasMaxLength(15)
                        .HasColumnType("int");

                    b.Property<bool?>("Show")
                        .HasColumnType("bit");

                    b.HasKey("LocalId");

                    b.ToTable("Admin", "dbo");
                });

            modelBuilder.Entity("Bristows.TRACR.Model.Models.Entities.Diary", b =>
                {
                    b.Property<Guid?>("LocalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("DiaryId")
                        .HasMaxLength(5)
                        .HasColumnType("int");

                    b.Property<string>("LearningPoints")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Pfid")
                        .HasMaxLength(5)
                        .HasColumnType("int");

                    b.Property<string>("ProfessionalConductIssues")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfessionalDevelopmentUndertaken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("Show")
                        .HasColumnType("bit");

                    b.Property<bool?>("SignOffSubmitted")
                        .HasColumnType("bit");

                    b.Property<string>("SignedOffBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("SignedOffTimestamp")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Timestamp")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("WeekBeginning")
                        .HasColumnType("datetime2");

                    b.HasKey("LocalId");

                    b.ToTable("Diary", "dbo");
                });

            modelBuilder.Entity("Bristows.TRACR.Model.Models.Entities.DiaryTask", b =>
                {
                    b.Property<Guid?>("LocalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("DiaryId")
                        .HasMaxLength(5)
                        .HasColumnType("int");

                    b.Property<int>("DiaryTaskId")
                        .HasMaxLength(5)
                        .HasColumnType("int");

                    b.Property<string>("Matter")
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<bool?>("Show")
                        .HasColumnType("bit");

                    b.Property<string>("TaskDescription")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("Timestamp")
                        .HasColumnType("datetime2");

                    b.HasKey("LocalId");

                    b.ToTable("DiaryTask", "dbo");
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
                    b.Property<Guid?>("LocalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(35)
                        .HasColumnType("nvarchar(35)");

                    b.Property<int?>("OtherPfid")
                        .HasMaxLength(30)
                        .HasColumnType("int");

                    b.Property<int>("PfId")
                        .HasMaxLength(5)
                        .HasColumnType("int");

                    b.Property<string>("Photo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telephone")
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("WinUser")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("LocalId");

                    b.ToTable("PeopleFinderUser", "dbo");
                });

            modelBuilder.Entity("Bristows.TRACR.Model.Models.Entities.Skill", b =>
                {
                    b.Property<Guid?>("LocalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Colour")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("DiaryTaskLocalId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool?>("Show")
                        .HasColumnType("bit");

                    b.Property<string>("SkillDescription")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("SkillId")
                        .HasMaxLength(5)
                        .HasColumnType("int");

                    b.Property<string>("SkillName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("LocalId");

                    b.HasIndex("DiaryTaskLocalId");

                    b.ToTable("Skill", "dbo");
                });

            modelBuilder.Entity("Bristows.TRACR.Model.Models.Entities.Trainee", b =>
                {
                    b.Property<Guid?>("LocalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool?>("Active")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("EntryCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LastUpdated")
                        .HasColumnType("datetime2");

                    b.Property<int?>("OtherPfid")
                        .HasColumnType("int");

                    b.Property<int>("ReviewerPfid")
                        .HasMaxLength(15)
                        .HasColumnType("int");

                    b.Property<bool?>("Show")
                        .HasColumnType("bit");

                    b.Property<int>("TraineeId")
                        .HasMaxLength(15)
                        .HasColumnType("int");

                    b.Property<int>("TraineePfid")
                        .HasMaxLength(15)
                        .HasColumnType("int");

                    b.HasKey("LocalId");

                    b.ToTable("Trainee", "dbo");
                });

            modelBuilder.Entity("Bristows.TRACR.Model.Models.Entities.Skill", b =>
                {
                    b.HasOne("Bristows.TRACR.Model.Models.Entities.DiaryTask", null)
                        .WithMany("Skills")
                        .HasForeignKey("DiaryTaskLocalId");
                });

            modelBuilder.Entity("Bristows.TRACR.Model.Models.Entities.DiaryTask", b =>
                {
                    b.Navigation("Skills");
                });
#pragma warning restore 612, 618
        }
    }
}
