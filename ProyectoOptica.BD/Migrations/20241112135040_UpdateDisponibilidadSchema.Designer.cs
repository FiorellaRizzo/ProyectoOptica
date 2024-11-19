﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProyectoOptica.BD.Data;

#nullable disable

namespace ProyectoOptica.BD.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20241112135040_UpdateDisponibilidadSchema")]
    partial class UpdateDisponibilidadSchema
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ProyectoOptica.BD.Data.Entity.Cita", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ClienteId")
                        .HasColumnType("int");

                    b.Property<int>("DisponibilidadId")
                        .HasColumnType("int");

                    b.Property<bool>("Estado")
                        .HasColumnType("bit");

                    b.Property<DateTime>("FechaDisponibilidad")
                        .HasColumnType("datetime2");

                    b.Property<TimeSpan>("HoraDisponible")
                        .HasColumnType("time");

                    b.Property<int?>("OptometristaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DisponibilidadId");

                    b.HasIndex("OptometristaId");

                    b.HasIndex(new[] { "ClienteId", "DisponibilidadId" }, "Cita_UQ")
                        .IsUnique();

                    b.ToTable("Citas");
                });

            modelBuilder.Entity("ProyectoOptica.BD.Data.Entity.Cliente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("ProyectoOptica.BD.Data.Entity.Disponibilidad", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("ClienteId")
                        .HasColumnType("int");

                    b.Property<bool>("Estado")
                        .HasColumnType("bit");

                    b.Property<DateTime>("FechaDisponibilidad")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("HoraDisponible")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdDisponibilidad")
                        .HasColumnType("int");

                    b.Property<int>("OptometristaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.HasIndex("OptometristaId");

                    b.ToTable("Disponibilidades");
                });

            modelBuilder.Entity("ProyectoOptica.BD.Data.Entity.Optometrista", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Optometristas");
                });

            modelBuilder.Entity("ProyectoOptica.BD.Data.Entity.Persona", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Correo")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("nvarchar(45)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("NumDoc")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.Property<int>("TDocumentoId")
                        .HasColumnType("int");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Apellido", "Nombre" }, "Persona_Apellido_Nombre");

                    b.HasIndex(new[] { "TDocumentoId", "NumDoc" }, "Persona_UQ")
                        .IsUnique();

                    b.ToTable("Personas");
                });

            modelBuilder.Entity("ProyectoOptica.BD.Data.Entity.TDocumento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasMaxLength(4)
                        .HasColumnType("nvarchar(4)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Codigo" }, "TDocumento_UQ")
                        .IsUnique();

                    b.ToTable("TDocumentos");
                });

            modelBuilder.Entity("ProyectoOptica.BD.Data.Entity.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Contrasena")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("PersonaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PersonaId");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("ProyectoOptica.BD.Data.Entity.Cita", b =>
                {
                    b.HasOne("ProyectoOptica.BD.Data.Entity.Cliente", "Clientes")
                        .WithMany()
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ProyectoOptica.BD.Data.Entity.Disponibilidad", "Disponibilidades")
                        .WithMany()
                        .HasForeignKey("DisponibilidadId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ProyectoOptica.BD.Data.Entity.Optometrista", null)
                        .WithMany("Citas")
                        .HasForeignKey("OptometristaId");

                    b.Navigation("Clientes");

                    b.Navigation("Disponibilidades");
                });

            modelBuilder.Entity("ProyectoOptica.BD.Data.Entity.Cliente", b =>
                {
                    b.HasOne("ProyectoOptica.BD.Data.Entity.Usuario", "Usuario")
                        .WithMany("Clientes")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("ProyectoOptica.BD.Data.Entity.Disponibilidad", b =>
                {
                    b.HasOne("ProyectoOptica.BD.Data.Entity.Cliente", null)
                        .WithMany("TurnosDisponibles")
                        .HasForeignKey("ClienteId");

                    b.HasOne("ProyectoOptica.BD.Data.Entity.Optometrista", "Optometristas")
                        .WithMany()
                        .HasForeignKey("OptometristaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Optometristas");
                });

            modelBuilder.Entity("ProyectoOptica.BD.Data.Entity.Optometrista", b =>
                {
                    b.HasOne("ProyectoOptica.BD.Data.Entity.Usuario", "Usuario")
                        .WithMany("Optometristas")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("ProyectoOptica.BD.Data.Entity.Persona", b =>
                {
                    b.HasOne("ProyectoOptica.BD.Data.Entity.TDocumento", "TDocumento")
                        .WithMany("Personas")
                        .HasForeignKey("TDocumentoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("TDocumento");
                });

            modelBuilder.Entity("ProyectoOptica.BD.Data.Entity.Usuario", b =>
                {
                    b.HasOne("ProyectoOptica.BD.Data.Entity.Persona", "Persona")
                        .WithMany()
                        .HasForeignKey("PersonaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Persona");
                });

            modelBuilder.Entity("ProyectoOptica.BD.Data.Entity.Cliente", b =>
                {
                    b.Navigation("TurnosDisponibles");
                });

            modelBuilder.Entity("ProyectoOptica.BD.Data.Entity.Optometrista", b =>
                {
                    b.Navigation("Citas");
                });

            modelBuilder.Entity("ProyectoOptica.BD.Data.Entity.TDocumento", b =>
                {
                    b.Navigation("Personas");
                });

            modelBuilder.Entity("ProyectoOptica.BD.Data.Entity.Usuario", b =>
                {
                    b.Navigation("Clientes");

                    b.Navigation("Optometristas");
                });
#pragma warning restore 612, 618
        }
    }
}
