using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLessonEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isCompleted",
                table: "Lessons",
                newName: "TestScore");

            migrationBuilder.AddColumn<bool>(
                name: "IsPracticeCompleted",
                table: "Lessons",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsTheoryCompleted",
                table: "Lessons",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPracticeCompleted",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "IsTheoryCompleted",
                table: "Lessons");

            migrationBuilder.RenameColumn(
                name: "TestScore",
                table: "Lessons",
                newName: "isCompleted");
        }
    }
}
