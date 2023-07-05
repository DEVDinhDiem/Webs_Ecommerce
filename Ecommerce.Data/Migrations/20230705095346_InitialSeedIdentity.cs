using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialSeedIdentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "d854e25f-2696-45c5-9597-d6ea9d5ffd72");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "70b4bd1e-095b-4f9a-8c52-ccb57dc4a895", "AQAAAAEAACcQAAAAEEotPG3yXUgkAMDcrAHUL8Q76VFxW22yF4eZ1bKT6Qgyf088/WPdmsx5cfsoSmeniA==" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2023, 7, 5, 16, 53, 46, 207, DateTimeKind.Local).AddTicks(125));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "deb86b97-68c1-4a6f-92ba-5427c5262a90");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e0f2547c-dae0-4639-9a96-d0c76736412c", "AQAAAAEAACcQAAAAEDXv9UJf7f5MiTRA0j+2UtpAVBFF7JJMCy08CgMr2cFc4Y9V0/qYGuNI3QP1I4Nd3Q==" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2023, 7, 5, 16, 44, 7, 412, DateTimeKind.Local).AddTicks(4168));
        }
    }
}
