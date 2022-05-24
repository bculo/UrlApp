using Microsoft.EntityFrameworkCore.Migrations;

namespace UrlUtility.API.Repository.Sql.Migrations
{
    public partial class ShortUrlEntityModified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UniqueShortUrl",
                table: "ShortUrls");

            migrationBuilder.AlterColumn<string>(
                name: "PageUrl",
                table: "ShortUrls",
                type: "nvarchar(2500)",
                maxLength: 2500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldMaxLength: 5000);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PageUrl",
                table: "ShortUrls",
                type: "nvarchar(max)",
                maxLength: 5000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(2500)",
                oldMaxLength: 2500);

            migrationBuilder.AddColumn<string>(
                name: "UniqueShortUrl",
                table: "ShortUrls",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");
        }
    }
}
