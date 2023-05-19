using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class DeleteRestrictBehavior : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PreviousLessons_Lessons_LessonId",
                table: "PreviousLessons");

            migrationBuilder.AddForeignKey(
                name: "FK_PreviousLessons_Lessons_LessonId",
                table: "PreviousLessons",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PreviousLessons_Lessons_LessonId",
                table: "PreviousLessons");

            migrationBuilder.AddForeignKey(
                name: "FK_PreviousLessons_Lessons_LessonId",
                table: "PreviousLessons",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
