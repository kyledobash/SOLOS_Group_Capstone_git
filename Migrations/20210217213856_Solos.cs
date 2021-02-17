using Microsoft.EntityFrameworkCore.Migrations;

namespace SOLOS_Group_Capstone.Migrations
{
    public partial class Solos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1c16faaa-2d61-4abc-ab98-98149910dcde");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5e53da22-13b7-4c75-822b-f3aa717e47da");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "19f32cc1-3dff-46f6-af8b-d667a66f060a", "8bbbcf2f-feae-40db-96d4-cd7f4fb65320", "Developer", "DEVELOPER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2e638e42-0ebb-4f29-940b-fe70f0a5f8ae", "90459494-08aa-4be3-9c57-db89595a0269", "Employer", "EMPLOYER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "19f32cc1-3dff-46f6-af8b-d667a66f060a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2e638e42-0ebb-4f29-940b-fe70f0a5f8ae");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5e53da22-13b7-4c75-822b-f3aa717e47da", "80751a4b-d86a-4037-a651-9a1a19de8c18", "Developer", "DEVELOPER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1c16faaa-2d61-4abc-ab98-98149910dcde", "9e222da0-703d-4737-a23e-9b27076874b5", "Employer", "EMPLOYER" });
        }
    }
}
