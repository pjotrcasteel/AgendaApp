using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AgendaApp.API.Migrations
{
    public partial class appointmentstatusaddded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("ae8b373b-4c51-479b-a174-118953b7f282"));

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Appointments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Appointments");

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "FirstName", "LastName", "UpdatedOn" },
                values: new object[] { new Guid("ae8b373b-4c51-479b-a174-118953b7f282"), "Pjotr", "Casteel", new DateTime(2020, 4, 26, 0, 0, 0, 0, DateTimeKind.Local) });
        }
    }
}
