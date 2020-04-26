using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AgendaApp.API.Migrations
{
    public partial class IEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("6647331f-8cc6-4877-82e0-4907046a9aa9"));

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "FirstName", "LastName", "UpdatedOn" },
                values: new object[] { new Guid("3ed128fd-fa81-44e5-aa35-5b6dd39a6586"), "Pjotr", "Casteel", new DateTime(2020, 4, 23, 0, 0, 0, 0, DateTimeKind.Local) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("3ed128fd-fa81-44e5-aa35-5b6dd39a6586"));

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "FirstName", "LastName", "UpdatedOn" },
                values: new object[] { new Guid("6647331f-8cc6-4877-82e0-4907046a9aa9"), "Pjotr", "Casteel", new DateTime(2020, 4, 23, 0, 0, 0, 0, DateTimeKind.Local) });
        }
    }
}
