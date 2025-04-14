using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Basic_Appointment_Management.Migrations
{
    /// <inheritdoc />
    public partial class AddedRolesToDb2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6b4e4238-32ea-4adb-83a0-3557f27235b2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f9776bde-7b06-4e17-aaed-dacb52130fae");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "398b03a0-f776-4c6d-9e3c-882108c4c2cd", null, "Manager", "MANAGER" },
                    { "db38f100-1232-40ff-b48f-d23ba4d25614", null, "Administrator", "ADMINISTRATOR" },
                    { "f577494f-af9b-49a9-b29a-4e3bf1d9ae92", null, "General_User", "GENERAL_USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "398b03a0-f776-4c6d-9e3c-882108c4c2cd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "db38f100-1232-40ff-b48f-d23ba4d25614");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f577494f-af9b-49a9-b29a-4e3bf1d9ae92");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6b4e4238-32ea-4adb-83a0-3557f27235b2", null, "Manager", "MANAGER" },
                    { "f9776bde-7b06-4e17-aaed-dacb52130fae", null, "Administrator", "ADMINISTRATOR" }
                });
        }
    }
}
