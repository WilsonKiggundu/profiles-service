﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ProfileService.Repositories;

namespace ProfileService.Migrations
{
    [DbContext(typeof(ProfileServiceContext))]
    [Migration("20201127150920_UpdatedPersonAwardModel_StartEndYearToString")]
    partial class UpdatedPersonAwardModel_StartEndYearToString
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("ProfileService.Models.Business.Business", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("DateCreated")
                        .HasColumnType("text");

                    b.Property<string>("DateLastUpdated")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int?>("EmployeeCount")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("IncorporationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Website")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasAlternateKey("Name")
                        .HasName("AlternateKey_Name");

                    b.ToTable("Businesses");
                });

            modelBuilder.Entity("ProfileService.Models.Business.BusinessAddress", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Building")
                        .HasColumnType("text");

                    b.Property<Guid>("BusinessId")
                        .HasColumnType("uuid");

                    b.Property<string>("City")
                        .HasColumnType("text");

                    b.Property<string>("Country")
                        .HasColumnType("text");

                    b.Property<string>("DateCreated")
                        .HasColumnType("text");

                    b.Property<string>("DateLastUpdated")
                        .HasColumnType("text");

                    b.Property<string>("District")
                        .HasColumnType("text");

                    b.Property<string>("Floor")
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Postal")
                        .HasColumnType("text");

                    b.Property<string>("Street")
                        .HasColumnType("text");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("BusinessId");

                    b.ToTable("BusinessAddresses");
                });

            modelBuilder.Entity("ProfileService.Models.Business.BusinessContact", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("BusinessId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ContactId")
                        .HasColumnType("uuid");

                    b.Property<string>("DateCreated")
                        .HasColumnType("text");

                    b.Property<string>("DateLastUpdated")
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("BusinessId");

                    b.HasIndex("ContactId");

                    b.ToTable("BusinessContacts");
                });

            modelBuilder.Entity("ProfileService.Models.Business.BusinessInterest", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("BusinessId")
                        .HasColumnType("uuid");

                    b.Property<string>("DateCreated")
                        .HasColumnType("text");

                    b.Property<string>("DateLastUpdated")
                        .HasColumnType("text");

                    b.Property<Guid>("InterestId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("BusinessId");

                    b.HasIndex("InterestId");

                    b.ToTable("BusinessInterests");
                });

            modelBuilder.Entity("ProfileService.Models.Business.BusinessNeed", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("BusinessId")
                        .HasColumnType("uuid");

                    b.Property<string>("DateCreated")
                        .HasColumnType("text");

                    b.Property<string>("DateLastUpdated")
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<Guid>("NeedId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("BusinessId");

                    b.HasIndex("NeedId");

                    b.ToTable("BusinessNeeds");
                });

            modelBuilder.Entity("ProfileService.Models.Business.BusinessProduct", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("BusinessId")
                        .HasColumnType("uuid");

                    b.Property<string>("DateCreated")
                        .HasColumnType("text");

                    b.Property<string>("DateLastUpdated")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("character varying(280)")
                        .HasMaxLength(280);

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("BusinessId");

                    b.ToTable("BusinessProducts");
                });

            modelBuilder.Entity("ProfileService.Models.Business.BusinessRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("BusinessId")
                        .HasColumnType("uuid");

                    b.Property<string>("DateCreated")
                        .HasColumnType("text");

                    b.Property<string>("DateLastUpdated")
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<Guid>("PersonId")
                        .HasColumnType("uuid");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("BusinessId");

                    b.HasIndex("PersonId");

                    b.ToTable("BusinessRoles");
                });

            modelBuilder.Entity("ProfileService.Models.Common.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("DateCreated")
                        .HasColumnType("text");

                    b.Property<string>("DateLastUpdated")
                        .HasColumnType("text");

                    b.Property<Guid?>("IconId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("IconId");

                    b.ToTable("LookupCategories");
                });

            modelBuilder.Entity("ProfileService.Models.Common.Contact", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("BelongsTo")
                        .HasColumnType("uuid");

                    b.Property<int>("Category")
                        .HasColumnType("integer");

                    b.Property<string>("DateCreated")
                        .HasColumnType("text");

                    b.Property<string>("DateLastUpdated")
                        .HasColumnType("text");

                    b.Property<string>("Details")
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Contacts");
                });

            modelBuilder.Entity("ProfileService.Models.Common.Interest", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("DateCreated")
                        .HasColumnType("text");

                    b.Property<string>("DateLastUpdated")
                        .HasColumnType("text");

                    b.Property<Guid?>("IconId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<Guid?>("PersonId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("IconId");

                    b.HasIndex("PersonId");

                    b.ToTable("LookupInterests");
                });

            modelBuilder.Entity("ProfileService.Models.Common.Need", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Category")
                        .HasColumnType("text");

                    b.Property<string>("DateCreated")
                        .HasColumnType("text");

                    b.Property<string>("DateLastUpdated")
                        .HasColumnType("text");

                    b.Property<Guid?>("IconId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("IconId");

                    b.ToTable("LookupNeeds");
                });

            modelBuilder.Entity("ProfileService.Models.Common.Upload", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ContentType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("DateCreated")
                        .HasColumnType("text");

                    b.Property<string>("DateLastUpdated")
                        .HasColumnType("text");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("FileSize")
                        .HasColumnType("integer");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uuid");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("LookupUploads");
                });

            modelBuilder.Entity("ProfileService.Models.Investor.Investor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("DateCreated")
                        .HasColumnType("text");

                    b.Property<string>("DateLastUpdated")
                        .HasColumnType("text");

                    b.Property<string>("InvestmentRange")
                        .HasColumnType("text");

                    b.Property<string>("InvestmentStage")
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsVerified")
                        .HasColumnType("boolean");

                    b.Property<Guid>("PersonId")
                        .HasColumnType("uuid");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.Property<string>("Website")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.ToTable("Investors");
                });

            modelBuilder.Entity("ProfileService.Models.Investor.InvestorAddress", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Building")
                        .HasColumnType("text");

                    b.Property<string>("City")
                        .HasColumnType("text");

                    b.Property<string>("Country")
                        .HasColumnType("text");

                    b.Property<string>("DateCreated")
                        .HasColumnType("text");

                    b.Property<string>("DateLastUpdated")
                        .HasColumnType("text");

                    b.Property<string>("District")
                        .HasColumnType("text");

                    b.Property<string>("Floor")
                        .HasColumnType("text");

                    b.Property<Guid>("InvestorId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Postal")
                        .HasColumnType("text");

                    b.Property<string>("Street")
                        .HasColumnType("text");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("InvestorId");

                    b.ToTable("InvestorAddresses");
                });

            modelBuilder.Entity("ProfileService.Models.Investor.InvestorContact", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ContactId")
                        .HasColumnType("uuid");

                    b.Property<string>("DateCreated")
                        .HasColumnType("text");

                    b.Property<string>("DateLastUpdated")
                        .HasColumnType("text");

                    b.Property<Guid>("InvestorId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("ContactId");

                    b.HasIndex("InvestorId");

                    b.ToTable("InvestorContacts");
                });

            modelBuilder.Entity("ProfileService.Models.Investor.InvestorInterest", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("DateCreated")
                        .HasColumnType("text");

                    b.Property<string>("DateLastUpdated")
                        .HasColumnType("text");

                    b.Property<Guid>("InterestId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("InvestorId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("InterestId");

                    b.HasIndex("InvestorId");

                    b.ToTable("InvestorInterests");
                });

            modelBuilder.Entity("ProfileService.Models.Investor.InvestorPortfolio", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("DateCreated")
                        .HasColumnType("text");

                    b.Property<string>("DateLastUpdated")
                        .HasColumnType("text");

                    b.Property<string>("Details")
                        .HasColumnType("text");

                    b.Property<Guid>("InvestorId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("InvestorId");

                    b.ToTable("InvestorPortfolios");
                });

            modelBuilder.Entity("ProfileService.Models.Person.Person", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Bio")
                        .HasColumnType("text");

                    b.Property<string>("DateCreated")
                        .HasColumnType("text");

                    b.Property<string>("DateLastUpdated")
                        .HasColumnType("text");

                    b.Property<string>("DateOfBirth")
                        .HasColumnType("text");

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Gender")
                        .HasColumnType("integer");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("UploadId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UploadId");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("ProfileService.Models.Person.PersonAward", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Activities")
                        .HasColumnType("text");

                    b.Property<string>("AwardedBy")
                        .HasColumnType("text");

                    b.Property<string>("DateCreated")
                        .HasColumnType("text");

                    b.Property<string>("DateLastUpdated")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("EndYear")
                        .HasColumnType("text");

                    b.Property<string>("FieldOfStudy")
                        .HasColumnType("text");

                    b.Property<string>("Grade")
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<Guid>("PersonId")
                        .HasColumnType("uuid");

                    b.Property<string>("StartYear")
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.ToTable("PersonAwards");
                });

            modelBuilder.Entity("ProfileService.Models.Person.PersonCategory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uuid");

                    b.Property<string>("DateCreated")
                        .HasColumnType("text");

                    b.Property<string>("DateLastUpdated")
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<Guid>("PersonId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("PersonId");

                    b.ToTable("PersonCategories");
                });

            modelBuilder.Entity("ProfileService.Models.Person.PersonInterest", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("DateCreated")
                        .HasColumnType("text");

                    b.Property<string>("DateLastUpdated")
                        .HasColumnType("text");

                    b.Property<Guid>("InterestId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<Guid>("PersonId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("InterestId");

                    b.HasIndex("PersonId");

                    b.ToTable("PersonInterests");
                });

            modelBuilder.Entity("ProfileService.Models.Person.PersonSkill", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("DateCreated")
                        .HasColumnType("text");

                    b.Property<string>("DateLastUpdated")
                        .HasColumnType("text");

                    b.Property<string>("Details")
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<Guid>("PersonId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.ToTable("PersonSkills");
                });

            modelBuilder.Entity("ProfileService.Models.Business.BusinessAddress", b =>
                {
                    b.HasOne("ProfileService.Models.Business.Business", "Business")
                        .WithMany()
                        .HasForeignKey("BusinessId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProfileService.Models.Business.BusinessContact", b =>
                {
                    b.HasOne("ProfileService.Models.Business.Business", "Business")
                        .WithMany()
                        .HasForeignKey("BusinessId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProfileService.Models.Common.Contact", "Contact")
                        .WithMany()
                        .HasForeignKey("ContactId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProfileService.Models.Business.BusinessInterest", b =>
                {
                    b.HasOne("ProfileService.Models.Business.Business", "Business")
                        .WithMany()
                        .HasForeignKey("BusinessId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProfileService.Models.Common.Interest", "Interest")
                        .WithMany()
                        .HasForeignKey("InterestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProfileService.Models.Business.BusinessNeed", b =>
                {
                    b.HasOne("ProfileService.Models.Business.Business", "Business")
                        .WithMany()
                        .HasForeignKey("BusinessId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProfileService.Models.Common.Need", "Need")
                        .WithMany()
                        .HasForeignKey("NeedId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProfileService.Models.Business.BusinessProduct", b =>
                {
                    b.HasOne("ProfileService.Models.Business.Business", "Business")
                        .WithMany()
                        .HasForeignKey("BusinessId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProfileService.Models.Business.BusinessRole", b =>
                {
                    b.HasOne("ProfileService.Models.Business.Business", "Business")
                        .WithMany()
                        .HasForeignKey("BusinessId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProfileService.Models.Person.Person", "Person")
                        .WithMany()
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProfileService.Models.Common.Category", b =>
                {
                    b.HasOne("ProfileService.Models.Common.Upload", "Icon")
                        .WithMany()
                        .HasForeignKey("IconId");
                });

            modelBuilder.Entity("ProfileService.Models.Common.Interest", b =>
                {
                    b.HasOne("ProfileService.Models.Common.Upload", "Icon")
                        .WithMany()
                        .HasForeignKey("IconId");

                    b.HasOne("ProfileService.Models.Person.Person", null)
                        .WithMany("Interests")
                        .HasForeignKey("PersonId");
                });

            modelBuilder.Entity("ProfileService.Models.Common.Need", b =>
                {
                    b.HasOne("ProfileService.Models.Common.Upload", "Icon")
                        .WithMany()
                        .HasForeignKey("IconId");
                });

            modelBuilder.Entity("ProfileService.Models.Investor.Investor", b =>
                {
                    b.HasOne("ProfileService.Models.Person.Person", "Person")
                        .WithMany()
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProfileService.Models.Investor.InvestorAddress", b =>
                {
                    b.HasOne("ProfileService.Models.Investor.Investor", "Investor")
                        .WithMany()
                        .HasForeignKey("InvestorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProfileService.Models.Investor.InvestorContact", b =>
                {
                    b.HasOne("ProfileService.Models.Common.Contact", "Contact")
                        .WithMany()
                        .HasForeignKey("ContactId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProfileService.Models.Investor.Investor", "Investor")
                        .WithMany()
                        .HasForeignKey("InvestorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProfileService.Models.Investor.InvestorInterest", b =>
                {
                    b.HasOne("ProfileService.Models.Common.Interest", "Interest")
                        .WithMany()
                        .HasForeignKey("InterestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProfileService.Models.Investor.Investor", "Investor")
                        .WithMany()
                        .HasForeignKey("InvestorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProfileService.Models.Investor.InvestorPortfolio", b =>
                {
                    b.HasOne("ProfileService.Models.Investor.Investor", "Investor")
                        .WithMany()
                        .HasForeignKey("InvestorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProfileService.Models.Person.Person", b =>
                {
                    b.HasOne("ProfileService.Models.Common.Upload", "Upload")
                        .WithMany()
                        .HasForeignKey("UploadId");
                });

            modelBuilder.Entity("ProfileService.Models.Person.PersonAward", b =>
                {
                    b.HasOne("ProfileService.Models.Person.Person", "Person")
                        .WithMany("Awards")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProfileService.Models.Person.PersonCategory", b =>
                {
                    b.HasOne("ProfileService.Models.Common.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProfileService.Models.Person.Person", "Person")
                        .WithMany("Categories")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProfileService.Models.Person.PersonInterest", b =>
                {
                    b.HasOne("ProfileService.Models.Common.Interest", "Interest")
                        .WithMany()
                        .HasForeignKey("InterestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProfileService.Models.Person.Person", "Person")
                        .WithMany()
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProfileService.Models.Person.PersonSkill", b =>
                {
                    b.HasOne("ProfileService.Models.Person.Person", "Person")
                        .WithMany("Skills")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
