﻿// <auto-generated />
using System;
using BorrowBuddy.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BorrowBuddy.Migrations
{
    [DbContext(typeof(BorrowBuddyContext))]
    partial class BorrowBuddyContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.0-rtm-30799")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BorrowBuddy.Domain.Currency", b =>
                {
                    b.Property<string>("Code")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(3);

                    b.Property<int>("Scale")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(100);

                    b.Property<string>("Symbol")
                        .HasMaxLength(10);

                    b.HasKey("Code");

                    b.ToTable("Currencies");
                });

            modelBuilder.Entity("BorrowBuddy.Domain.Flow", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Comment");

                    b.Property<Guid?>("LendeeId")
                        .IsRequired();

                    b.Property<Guid?>("LenderId")
                        .IsRequired();

                    b.Property<DateTimeOffset>("Timestamp");

                    b.HasKey("Id");

                    b.HasIndex("LendeeId");

                    b.HasIndex("LenderId");

                    b.ToTable("Flows");
                });

            modelBuilder.Entity("BorrowBuddy.Domain.Participant", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(36);

                    b.Property<string>("LastName")
                        .HasMaxLength(36);

                    b.Property<string>("MiddleName")
                        .HasMaxLength(36);

                    b.HasKey("Id");

                    b.ToTable("Participants");
                });

            modelBuilder.Entity("BorrowBuddy.Domain.Flow", b =>
                {
                    b.HasOne("BorrowBuddy.Domain.Participant", "Lendee")
                        .WithMany("Borrowed")
                        .HasForeignKey("LendeeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BorrowBuddy.Domain.Participant", "Lender")
                        .WithMany("Lended")
                        .HasForeignKey("LenderId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.OwnsOne("BorrowBuddy.Domain.Money", "Amount", b1 =>
                        {
                            b1.Property<Guid?>("FlowId");

                            b1.Property<string>("CurrencyCode");

                            b1.Property<long>("Value");

                            b1.HasIndex("CurrencyCode");

                            b1.ToTable("Flows");

                            b1.HasOne("BorrowBuddy.Domain.Currency", "Currency")
                                .WithMany()
                                .HasForeignKey("CurrencyCode");

                            b1.HasOne("BorrowBuddy.Domain.Flow")
                                .WithOne("Amount")
                                .HasForeignKey("BorrowBuddy.Domain.Money", "FlowId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
