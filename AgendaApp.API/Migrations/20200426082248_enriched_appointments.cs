using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AgendaApp.API.Migrations
{
    public partial class enriched_appointments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("3ed128fd-fa81-44e5-aa35-5b6dd39a6586"));

            migrationBuilder.AddColumn<bool>(
                name: "IsFullDay",
                table: "Appointments",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Appointments",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "FirstName", "LastName", "UpdatedOn" },
                values: new object[] { new Guid("ae8b373b-4c51-479b-a174-118953b7f282"), "Pjotr", "Casteel", new DateTime(2020, 4, 26, 0, 0, 0, 0, DateTimeKind.Local) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("ae8b373b-4c51-479b-a174-118953b7f282"));

            migrationBuilder.DropColumn(
                name: "IsFullDay",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Appointments");

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "FirstName", "LastName", "UpdatedOn" },
                values: new object[] { new Guid("3ed128fd-fa81-44e5-aa35-5b6dd39a6586"), "Pjotr", "Casteel", new DateTime(2020, 4, 23, 0, 0, 0, 0, DateTimeKind.Local) });
        }
    }
}
