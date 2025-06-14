﻿// <auto-generated />
using System;
using MiBibliotecaApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MiBibliotecaApi.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250606155048_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("AutorLibro", b =>
                {
                    b.Property<int>("AutoresId")
                        .HasColumnType("integer");

                    b.Property<int>("LibrosId")
                        .HasColumnType("integer");

                    b.HasKey("AutoresId", "LibrosId");

                    b.HasIndex("LibrosId");

                    b.ToTable("AutorLibro");
                });

            modelBuilder.Entity("MiBibliotecaApi.Models.Autor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("Id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Nacionalidad")
                        .HasColumnType("text")
                        .HasColumnName("Nacionalidad");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Nombre");

                    b.HasKey("Id");

                    b.ToTable("Autores");
                });

            modelBuilder.Entity("MiBibliotecaApi.Models.Libro", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("Id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Estado");

                    b.Property<DateTime?>("FechaPublicacion")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("FechaPublicacion");

                    b.Property<string>("ISBN")
                        .HasColumnType("text")
                        .HasColumnName("ISBN");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Titulo");

                    b.HasKey("Id");

                    b.ToTable("Libros");
                });

            modelBuilder.Entity("AutorLibro", b =>
                {
                    b.HasOne("MiBibliotecaApi.Models.Autor", null)
                        .WithMany()
                        .HasForeignKey("AutoresId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MiBibliotecaApi.Models.Libro", null)
                        .WithMany()
                        .HasForeignKey("LibrosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
