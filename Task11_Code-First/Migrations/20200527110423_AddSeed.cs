using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Task3.Migrations
{
    public partial class AddSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Doctor",
                keyColumn: "IdDoctor",
                keyValue: 1,
                columns: new[] { "Email", "FirstName", "LastName" },
                values: new object[] { "marcin.polkowski@gmail.com", "Marcin", "Polkowski" });

            migrationBuilder.UpdateData(
                table: "Doctor",
                keyColumn: "IdDoctor",
                keyValue: 2,
                columns: new[] { "Email", "FirstName", "LastName" },
                values: new object[] { "mariusz.ch12@gmail.com", "Mariusz", "Chrabąszcz" });

            migrationBuilder.UpdateData(
                table: "Doctor",
                keyColumn: "IdDoctor",
                keyValue: 3,
                columns: new[] { "Email", "FirstName", "LastName" },
                values: new object[] { "cornel@gmail.com", "Konrad", "Cornel" });

            migrationBuilder.UpdateData(
                table: "Medicament",
                keyColumn: "IdMedicament",
                keyValue: 1,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Na ból gardła", "Strepsils" });

            migrationBuilder.InsertData(
                table: "Medicament",
                columns: new[] { "IdMedicament", "Description", "Name", "Type" },
                values: new object[] { 3, "Na odporność", "Rutinoskorbin", "Lek wzmacniający" });

            migrationBuilder.UpdateData(
                table: "Patient",
                keyColumn: "IdPatient",
                keyValue: 2,
                columns: new[] { "FirstName", "LastName" },
                values: new object[] { "Janek", "Witkowski" });

            migrationBuilder.InsertData(
                table: "Patient",
                columns: new[] { "IdPatient", "BirthDate", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 3, new DateTime(2000, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Maria", "Szwedo" },
                    { 4, new DateTime(1998, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Krzysztof", "Górny" },
                    { 5, new DateTime(1990, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Maciek", "Janiszewski" }
                });

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

            migrationBuilder.UpdateData(
                table: "Prescription_Medicaments",
                keyColumns: new[] { "IdMedicament", "IdPrescription" },
                keyValues: new object[] { 1, 1 },
                column: "Details",
                value: "Stosować przez 2 tygodnie");

            migrationBuilder.UpdateData(
                table: "Prescription_Medicaments",
                keyColumns: new[] { "IdMedicament", "IdPrescription" },
                keyValues: new object[] { 2, 2 },
                column: "Details",
                value: "Stosować raz na 3 godziny");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Medicament",
                keyColumn: "IdMedicament",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Patient",
                keyColumn: "IdPatient",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Patient",
                keyColumn: "IdPatient",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Patient",
                keyColumn: "IdPatient",
                keyValue: 5);

            migrationBuilder.UpdateData(
                table: "Doctor",
                keyColumn: "IdDoctor",
                keyValue: 1,
                columns: new[] { "Email", "FirstName", "LastName" },
                values: new object[] { "JerzyZieba@gmail.com", "Jerzy", "Zięba" });

            migrationBuilder.UpdateData(
                table: "Doctor",
                keyColumn: "IdDoctor",
                keyValue: 2,
                columns: new[] { "Email", "FirstName", "LastName" },
                values: new object[] { "LukaszSzumowski@gmail.com", "Łukasz", "Szumowski" });

            migrationBuilder.UpdateData(
                table: "Doctor",
                keyColumn: "IdDoctor",
                keyValue: 3,
                columns: new[] { "Email", "FirstName", "LastName" },
                values: new object[] { "DrPhil@gmail.com", "Phil", "McGraw" });

            migrationBuilder.UpdateData(
                table: "Medicament",
                keyColumn: "IdMedicament",
                keyValue: 1,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Na bóle głowy, mięśniowe", "Ibuprom" });

            migrationBuilder.UpdateData(
                table: "Patient",
                keyColumn: "IdPatient",
                keyValue: 2,
                columns: new[] { "FirstName", "LastName" },
                values: new object[] { "Nina", "Sobiczewska" });

            migrationBuilder.UpdateData(
                table: "Prescription",
                keyColumn: "IdPrescription",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2020, 5, 27, 12, 59, 26, 469, DateTimeKind.Local).AddTicks(4960));

            migrationBuilder.UpdateData(
                table: "Prescription",
                keyColumn: "IdPrescription",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2020, 5, 27, 12, 59, 26, 471, DateTimeKind.Local).AddTicks(1730));

            migrationBuilder.UpdateData(
                table: "Prescription",
                keyColumn: "IdPrescription",
                keyValue: 3,
                column: "Date",
                value: new DateTime(2020, 5, 27, 12, 59, 26, 471, DateTimeKind.Local).AddTicks(1810));

            migrationBuilder.UpdateData(
                table: "Prescription_Medicaments",
                keyColumns: new[] { "IdMedicament", "IdPrescription" },
                keyValues: new object[] { 1, 1 },
                column: "Details",
                value: "Stosować do zniknięcia objawów");

            migrationBuilder.UpdateData(
                table: "Prescription_Medicaments",
                keyColumns: new[] { "IdMedicament", "IdPrescription" },
                keyValues: new object[] { 2, 2 },
                column: "Details",
                value: "Stosować raz dziennie");
        }
    }
}
