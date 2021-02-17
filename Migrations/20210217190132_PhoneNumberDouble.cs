using Microsoft.EntityFrameworkCore.Migrations;

namespace SOLOS_Group_Capstone.Migrations
{
    public partial class PhoneNumberDouble : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "620d0c4e-3140-4dcc-8b04-e14d45edcfd8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ed25f25d-f4fb-41d3-8a8e-6c18e8b20f3e");

            migrationBuilder.AlterColumn<double>(
                name: "PhoneNumber",
                table: "Employer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6722f6b4-2260-41fb-9d38-5b20447d1cbf", "0b24042f-4033-4db0-bcd5-652763c6330a", "Developer", "DEVELOPER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3c3185a0-dc08-451a-a492-bb3818469746", "d3f21850-8317-49de-8fc2-a6a2348ce6a1", "Employer", "EMPLOYER" });

            migrationBuilder.UpdateData(
                table: "Employer",
                keyColumn: "EmpId",
                keyValue: 1,
                column: "PhoneNumber",
                value: 6029994298.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3c3185a0-dc08-451a-a492-bb3818469746");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6722f6b4-2260-41fb-9d38-5b20447d1cbf");

            migrationBuilder.AlterColumn<int>(
                name: "PhoneNumber",
                table: "Employer",
                type: "int",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "620d0c4e-3140-4dcc-8b04-e14d45edcfd8", "bfe18643-55d5-41ea-8d4e-f13e7b029d6f", "Developer", "DEVELOPER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ed25f25d-f4fb-41d3-8a8e-6c18e8b20f3e", "9337560a-1092-4fd8-ae47-e4d60cb1c870", "Employer", "EMPLOYER" });

            migrationBuilder.UpdateData(
                table: "Employer",
                keyColumn: "EmpId",
                keyValue: 1,
                column: "PhoneNumber",
                value: 12);
        }
    }
}
