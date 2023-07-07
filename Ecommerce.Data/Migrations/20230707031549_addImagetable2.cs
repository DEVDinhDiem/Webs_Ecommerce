using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Data.Migrations
{
    /// <inheritdoc />
    public partial class addImagetable2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "6b4174db-1319-4791-82ba-d5a282eee92a");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "c3834df4-c222-452f-8234-8c7986b52329", "AQAAAAEAACcQAAAAEFVowuJKwhlb6FV/OMlLE3/UXq0Fvz6SpTzgE5LmD4GfV/KUQlNc/zyQjU697O24Nw==" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2023, 7, 7, 10, 15, 49, 377, DateTimeKind.Local).AddTicks(2431));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "851a59ae-803b-4f02-a684-3ac714332cca");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "5278cffe-43b1-4c5b-b990-49a4770b2f89", "AQAAAAEAACcQAAAAEKGc2uGsECpR9n6W6h5TYfmMfVs81/5TbGBYCZc4IvgRzRc+nzz88+XX6Zw+ZACEhQ==" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2023, 7, 6, 17, 3, 51, 355, DateTimeKind.Local).AddTicks(7711));
        }
    }
}
