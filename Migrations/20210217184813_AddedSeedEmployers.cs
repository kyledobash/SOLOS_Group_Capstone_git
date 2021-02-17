using Microsoft.EntityFrameworkCore.Migrations;

namespace SOLOS_Group_Capstone.Migrations
{
    public partial class AddedSeedEmployers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "84ad108e-eed7-4560-b12a-08416fa65461");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a69542ae-0403-468c-a3b1-07d324a46e21");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "00d5ab82-1975-45a5-9833-88a3e1176f83", "f4299173-69fa-487c-be2b-5eda71883994", "Developer", "DEVELOPER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ab9e86a5-c935-42f0-8a15-061f476f56ab", "b71110e0-c662-408e-93e0-046d9822cc0d", "Employer", "EMPLOYER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
                values: new object[] { "84ad108e-eed7-4560-b12a-08416fa65461", "823fab59-9c23-4599-ac83-4d0f7975fe02", "Developer", "DEVELOPER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a69542ae-0403-468c-a3b1-07d324a46e21", "2e461ef3-e5f2-4507-ad6d-45bd51a2f30c", "Employer", "EMPLOYER" });
        }
    }
}
