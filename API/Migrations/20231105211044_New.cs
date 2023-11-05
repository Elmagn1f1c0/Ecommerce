using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class New : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1917f4f2-a87a-474e-a9e6-118e7a7f59d9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "48a7377b-0166-4dc3-9242-c72b00f78236");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "aeec83c7-8874-49f2-a53c-a04c07878fe8", null, "Admin", "ADMIN" },
                    { "af916686-5db4-4df8-831f-0a4399131752", null, "Member", "MEMBER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aeec83c7-8874-49f2-a53c-a04c07878fe8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "af916686-5db4-4df8-831f-0a4399131752");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1917f4f2-a87a-474e-a9e6-118e7a7f59d9", null, "Member", "MEMBER" },
                    { "48a7377b-0166-4dc3-9242-c72b00f78236", null, "Admin", "ADMIN" }
                });
        }
    }
}
