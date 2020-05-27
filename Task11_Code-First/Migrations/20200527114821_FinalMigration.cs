using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Task3.Migrations
{
    public partial class FinalMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Prescription",
                keyColumn: "IdPrescription",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2020, 5, 27, 13, 48, 21, 500, DateTimeKind.Local).AddTicks(7340));

            migrationBuilder.UpdateData(
                table: "Prescription",
                keyColumn: "IdPrescription",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2020, 5, 27, 13, 48, 21, 503, DateTimeKind.Local).AddTicks(1640));

            migrationBuilder.UpdateData(
                table: "Prescription",
                keyColumn: "IdPrescription",
                keyValue: 3,
                column: "Date",
                value: new DateTime(2020, 5, 27, 13, 48, 21, 503, DateTimeKind.Local).AddTicks(1720));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Prescription",
                keyColumn: "IdPrescription",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2020, 5, 27, 13, 4, 23, 544, DateTimeKind.Local).AddTicks(1950));

            migrationBuilder.UpdateData(
                table: "Prescription",
                keyColumn: "IdPrescription",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2020, 5, 27, 13, 4, 23, 545, DateTimeKind.Local).AddTicks(8610));

            migrationBuilder.UpdateData(
                table: "Prescription",
                keyColumn: "IdPrescription",
                keyValue: 3,
                column: "Date",
                value: new DateTime(2020, 5, 27, 13, 4, 23, 545, DateTimeKind.Local).AddTicks(8690));
        }
    }
}
