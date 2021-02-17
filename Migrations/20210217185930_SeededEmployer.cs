using Microsoft.EntityFrameworkCore.Migrations;

namespace SOLOS_Group_Capstone.Migrations
{
    public partial class SeededEmployer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "00d5ab82-1975-45a5-9833-88a3e1176f83");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ab9e86a5-c935-42f0-8a15-061f476f56ab");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "620d0c4e-3140-4dcc-8b04-e14d45edcfd8", "bfe18643-55d5-41ea-8d4e-f13e7b029d6f", "Developer", "DEVELOPER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ed25f25d-f4fb-41d3-8a8e-6c18e8b20f3e", "9337560a-1092-4fd8-ae47-e4d60cb1c870", "Employer", "EMPLOYER" });

            migrationBuilder.InsertData(
                table: "Employer",
                columns: new[] { "EmpId", "City", "Email", "FirstName", "IdentityUserId", "LastName", "PhoneNumber", "State" },
                values: new object[] { 1, "PHX", "kyledobash@yahoo.com", "Kyle", null, "Dobash", 12, "AZ" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "620d0c4e-3140-4dcc-8b04-e14d45edcfd8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ed25f25d-f4fb-41d3-8a8e-6c18e8b20f3e");

            migrationBuilder.DeleteData(
                table: "Employer",
                keyColumn: "EmpId",
                keyValue: 1);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "00d5ab82-1975-45a5-9833-88a3e1176f83", "f4299173-69fa-487c-be2b-5eda71883994", "Developer", "DEVELOPER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ab9e86a5-c935-42f0-8a15-061f476f56ab", "b71110e0-c662-408e-93e0-046d9822cc0d", "Employer", "EMPLOYER" });
        }
    }
}
