using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class PublicIdAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2fee48b9-f7e4-49b3-83f9-22164e2cf086");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4af85d66-9a1c-4a98-8e45-f303b005e8e3");

            migrationBuilder.AddColumn<string>(
                name: "PublicId",
                table: "Tests",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PublicId",
                table: "Options",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PracticePublicId",
                table: "Lessons",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TheoryPublicId",
                table: "Lessons",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImgUrl",
                table: "Courses",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PublicId",
                table: "Courses",
                type: "text",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "b9600d23-6e54-4922-859c-fd47ec81dc03", null, "Member", "MEMBER" },
                    { "fe0ece13-6061-49bd-9333-a745b9f63f0b", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b9600d23-6e54-4922-859c-fd47ec81dc03");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fe0ece13-6061-49bd-9333-a745b9f63f0b");

            migrationBuilder.DropColumn(
                name: "PublicId",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "PublicId",
                table: "Options");

            migrationBuilder.DropColumn(
                name: "PracticePublicId",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "TheoryPublicId",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "ImgUrl",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "PublicId",
                table: "Courses");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2fee48b9-f7e4-49b3-83f9-22164e2cf086", null, "Member", "MEMBER" },
                    { "4af85d66-9a1c-4a98-8e45-f303b005e8e3", null, "Admin", "ADMIN" }
                });
        }
    }
}
