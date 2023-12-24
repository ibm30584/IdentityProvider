using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityProvider.Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class DbCreation2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ClientSecrets",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 12, 21, 1, 22, 21, 544, DateTimeKind.Utc).AddTicks(2999));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AllowedIdentityTokenSigningAlgorithms", "Created" },
                values: new object[] { "", new DateTime(2023, 12, 21, 1, 22, 21, 544, DateTimeKind.Utc).AddTicks(2743) });

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AllowedIdentityTokenSigningAlgorithms", "Created" },
                values: new object[] { "", new DateTime(2023, 12, 21, 1, 22, 21, 544, DateTimeKind.Utc).AddTicks(3110) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ClientSecrets",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 12, 21, 1, 11, 34, 698, DateTimeKind.Utc).AddTicks(9677));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AllowedIdentityTokenSigningAlgorithms", "Created" },
                values: new object[] { "System.Collections.Generic.HashSet`1[System.String]", new DateTime(2023, 12, 21, 1, 11, 34, 698, DateTimeKind.Utc).AddTicks(8100) });

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AllowedIdentityTokenSigningAlgorithms", "Created" },
                values: new object[] { "System.Collections.Generic.HashSet`1[System.String]", new DateTime(2023, 12, 21, 1, 11, 34, 698, DateTimeKind.Utc).AddTicks(9962) });
        }
    }
}
