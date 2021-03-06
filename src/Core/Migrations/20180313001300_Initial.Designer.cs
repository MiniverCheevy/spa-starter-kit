﻿// <auto-generated />
using Core.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace Core.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20180313001300_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Core.Models.ApplicationSetting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .IsUnicode(false);

                    b.Property<string>("Value")
                        .HasMaxLength(128)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.ToTable("ApplicationSettings");
                });

            modelBuilder.Entity("Core.Models.Identity.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .IsUnicode(false);

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Core.Models.Identity.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FirstName")
                        .HasMaxLength(128)
                        .IsUnicode(false);

                    b.Property<DateTime?>("LastAuthentication");

                    b.Property<string>("LastName")
                        .HasMaxLength(128)
                        .IsUnicode(false);

                    b.Property<string>("LastUserAgent")
                        .HasMaxLength(128)
                        .IsUnicode(false);

                    b.Property<bool>("LockoutEnabled");

                    b.Property<string>("PasswordHash")
                        .HasMaxLength(128)
                        .IsUnicode(false);

                    b.Property<string>("Salt")
                        .HasMaxLength(128)
                        .IsUnicode(false);

                    b.Property<string>("UserName")
                        .HasMaxLength(128)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Core.Models.Logging.Error", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("CreationDate");

                    b.Property<string>("Details")
                        .HasColumnName("Detail");

                    b.Property<int?>("ErrorHash");

                    b.Property<string>("FullJson");

                    b.Property<string>("Host")
                        .HasMaxLength(200);

                    b.Property<string>("HttpMethod")
                        .HasMaxLength(200);

                    b.Property<string>("IpAddress")
                        .HasMaxLength(200);

                    b.Property<string>("MachineName")
                        .HasMaxLength(200);

                    b.Property<string>("Message")
                        .HasMaxLength(200);

                    b.Property<Guid?>("RequestId");

                    b.Property<string>("Source")
                        .HasMaxLength(200);

                    b.Property<int?>("StatusCode");

                    b.Property<string>("Type")
                        .HasMaxLength(200);

                    b.Property<string>("Url")
                        .HasMaxLength(200);

                    b.Property<string>("User")
                        .HasMaxLength(128)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.ToTable("Exceptions");
                });

            modelBuilder.Entity("Core.Models.Scratch.BlobOfText", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("MemberId");

                    b.Property<string>("Text")
                        .HasMaxLength(128)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.HasIndex("MemberId");

                    b.ToTable("BlobOfText","scratch");
                });

            modelBuilder.Entity("Core.Models.Scratch.Member", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ManagerId");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .IsUnicode(false);

                    b.Property<DateTime?>("OptionalDate");

                    b.Property<DateTimeOffset?>("OptionalDateTimeOffset");

                    b.Property<decimal?>("OptionalDecimal");

                    b.Property<int?>("OptionalInt");

                    b.Property<DateTime>("RequiredDate");

                    b.Property<DateTimeOffset>("RequiredDateTimeOffset");

                    b.Property<decimal>("RequiredDecimal");

                    b.Property<int>("RequiredInt");

                    b.Property<string>("Title")
                        .HasMaxLength(128)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.HasIndex("ManagerId");

                    b.ToTable("Members","scratch");
                });

            modelBuilder.Entity("Core.Models.Scratch.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .IsUnicode(false);

                    b.Property<int>("TeamId");

                    b.HasKey("Id");

                    b.HasIndex("TeamId");

                    b.ToTable("Projects","scratch");
                });

            modelBuilder.Entity("Core.Models.Scratch.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("MemberId");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.HasIndex("MemberId");

                    b.ToTable("Teams","scratch");
                });

            modelBuilder.Entity("Voodoo.CodeGeneration.Models.TestClass", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.ToTable("TestClasses");
                });

            modelBuilder.Entity("Core.Models.Identity.Role", b =>
                {
                    b.HasOne("Core.Models.Identity.User")
                        .WithMany("Roles")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Core.Models.Scratch.BlobOfText", b =>
                {
                    b.HasOne("Core.Models.Scratch.Member", "Member")
                        .WithMany("BlobsOfText")
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Core.Models.Scratch.Member", b =>
                {
                    b.HasOne("Core.Models.Scratch.Member", "Manager")
                        .WithMany()
                        .HasForeignKey("ManagerId");
                });

            modelBuilder.Entity("Core.Models.Scratch.Project", b =>
                {
                    b.HasOne("Core.Models.Scratch.Team", "Team")
                        .WithMany()
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Core.Models.Scratch.Team", b =>
                {
                    b.HasOne("Core.Models.Scratch.Member")
                        .WithMany("Teams")
                        .HasForeignKey("MemberId");
                });
#pragma warning restore 612, 618
        }
    }
}
