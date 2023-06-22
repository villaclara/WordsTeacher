using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WordsTeacher.DB.Migrations
{
    /// <inheritdoc />
    public partial class migr1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NickName",
                table: "Words",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "UserId",
                table: "Words",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "nvarchar(max)");
        }
    }
}
