﻿// <auto-generated />
using AspnetNote.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace AspnetNote.Migrations
{
    [DbContext(typeof(AspnetNoteDbContext))]
    partial class AspnetNoteDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AspnetNote.Models.Note", b =>
                {
                    b.Property<int>("NoteNo")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("NoteContents")
                        .IsRequired();

                    b.Property<string>("NoteTitle")
                        .IsRequired();

                    b.Property<int>("UserNo");

                    b.HasKey("NoteNo");

                    b.HasIndex("UserNo");

                    b.ToTable("Notes");
                });

            modelBuilder.Entity("AspnetNote.Models.User", b =>
                {
                    b.Property<int>("UserNo")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.Property<string>("UserName")
                        .IsRequired();

                    b.Property<string>("UserPassword")
                        .IsRequired();

                    b.HasKey("UserNo");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("AspnetNote.Models.Note", b =>
                {
                    b.HasOne("AspnetNote.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserNo")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
