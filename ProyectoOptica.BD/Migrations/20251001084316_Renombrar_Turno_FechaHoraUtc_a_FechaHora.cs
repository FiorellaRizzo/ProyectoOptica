using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoOptica.BD.Migrations
{
    /// <inheritdoc />
    public partial class Renombrar_Turno_FechaHoraUtc_a_FechaHora : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "Turno_UQ_FechaHora",
                table: "Turnos");

            migrationBuilder.RenameColumn(
                name: "FechaHoraUtc",
                table: "Turnos",
                newName: "FechaHora");

            migrationBuilder.AddColumn<DateTime>(
                name: "Creada",
                table: "Turnos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Creada",
                table: "Turnos");

            migrationBuilder.RenameColumn(
                name: "FechaHora",
                table: "Turnos",
                newName: "FechaHoraUtc");

            migrationBuilder.CreateIndex(
                name: "Turno_UQ_FechaHora",
                table: "Turnos",
                column: "FechaHoraUtc",
                unique: true);
        }
    }
}
