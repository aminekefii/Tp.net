using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MovieApplication.Migrations
{
    /// <inheritdoc />
    public partial class FluentApiAndMembership : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Genres_GenreId",
                table: "Movies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Movies",
                table: "Movies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Genres",
                table: "Genres");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customers",
                table: "Customers");

            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.RenameTable(
                name: "Movies",
                newName: "T_Movie",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "Genres",
                newName: "T_Genre",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "Customers",
                newName: "T_Customer",
                newSchema: "dbo");

            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "dbo",
                table: "T_Movie",
                newName: "MovieName");

            migrationBuilder.RenameIndex(
                name: "IX_Movies_GenreId",
                schema: "dbo",
                table: "T_Movie",
                newName: "IX_T_Movie_GenreId");

            migrationBuilder.AlterColumn<Guid>(
                name: "GenreId",
                schema: "dbo",
                table: "T_Movie",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "MovieName",
                schema: "dbo",
                table: "T_Movie",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateAdded",
                schema: "dbo",
                table: "T_Movie",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()");

            migrationBuilder.AddColumn<string>(
                name: "PhotoPath",
                schema: "dbo",
                table: "T_Movie",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "dbo",
                table: "T_Customer",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<Guid>(
                name: "MembershiptypeID",
                schema: "dbo",
                table: "T_Customer",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_T_Movie",
                schema: "dbo",
                table: "T_Movie",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_T_Genre",
                schema: "dbo",
                table: "T_Genre",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_T_Customer",
                schema: "dbo",
                table: "T_Customer",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "CustomerMovie",
                schema: "dbo",
                columns: table => new
                {
                    CustomersId = table.Column<int>(type: "int", nullable: false),
                    MoviesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerMovie", x => new { x.CustomersId, x.MoviesId });
                    table.ForeignKey(
                        name: "FK_CustomerMovie_T_Customer_CustomersId",
                        column: x => x.CustomersId,
                        principalSchema: "dbo",
                        principalTable: "T_Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerMovie_T_Movie_MoviesId",
                        column: x => x.MoviesId,
                        principalSchema: "dbo",
                        principalTable: "T_Movie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MemberShipTypes",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DurationInMonth = table.Column<int>(type: "int", nullable: false),
                    DiscountRate = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberShipTypes", x => x.Id);
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "MemberShipTypes",
                columns: new[] { "Id", "DiscountRate", "DurationInMonth", "Name" },
                values: new object[,]
                {
                    { new Guid("8f1c2a51-3d6e-4b1a-9c7d-1a2b3c4d5e01"), 0, 0, "Pay as You Go" },
                    { new Guid("8f1c2a51-3d6e-4b1a-9c7d-1a2b3c4d5e02"), 10, 1, "Monthly" },
                    { new Guid("8f1c2a51-3d6e-4b1a-9c7d-1a2b3c4d5e03"), 15, 3, "Quarterly" },
                    { new Guid("8f1c2a51-3d6e-4b1a-9c7d-1a2b3c4d5e04"), 20, 12, "Annual" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_T_Customer_MembershiptypeID",
                schema: "dbo",
                table: "T_Customer",
                column: "MembershiptypeID");

            migrationBuilder.CreateIndex(
                name: "IX_T_Customer_Name",
                schema: "dbo",
                table: "T_Customer",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerMovie_MoviesId",
                schema: "dbo",
                table: "CustomerMovie",
                column: "MoviesId");

            migrationBuilder.AddForeignKey(
                name: "FK_T_Customer_MemberShipTypes_MembershiptypeID",
                schema: "dbo",
                table: "T_Customer",
                column: "MembershiptypeID",
                principalSchema: "dbo",
                principalTable: "MemberShipTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_T_Movie_T_Genre_GenreId",
                schema: "dbo",
                table: "T_Movie",
                column: "GenreId",
                principalSchema: "dbo",
                principalTable: "T_Genre",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_T_Customer_MemberShipTypes_MembershiptypeID",
                schema: "dbo",
                table: "T_Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_T_Movie_T_Genre_GenreId",
                schema: "dbo",
                table: "T_Movie");

            migrationBuilder.DropTable(
                name: "CustomerMovie",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "MemberShipTypes",
                schema: "dbo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_T_Movie",
                schema: "dbo",
                table: "T_Movie");

            migrationBuilder.DropPrimaryKey(
                name: "PK_T_Genre",
                schema: "dbo",
                table: "T_Genre");

            migrationBuilder.DropPrimaryKey(
                name: "PK_T_Customer",
                schema: "dbo",
                table: "T_Customer");

            migrationBuilder.DropIndex(
                name: "IX_T_Customer_MembershiptypeID",
                schema: "dbo",
                table: "T_Customer");

            migrationBuilder.DropIndex(
                name: "IX_T_Customer_Name",
                schema: "dbo",
                table: "T_Customer");

            migrationBuilder.DropColumn(
                name: "DateAdded",
                schema: "dbo",
                table: "T_Movie");

            migrationBuilder.DropColumn(
                name: "PhotoPath",
                schema: "dbo",
                table: "T_Movie");

            migrationBuilder.DropColumn(
                name: "MembershiptypeID",
                schema: "dbo",
                table: "T_Customer");

            migrationBuilder.RenameTable(
                name: "T_Movie",
                schema: "dbo",
                newName: "Movies");

            migrationBuilder.RenameTable(
                name: "T_Genre",
                schema: "dbo",
                newName: "Genres");

            migrationBuilder.RenameTable(
                name: "T_Customer",
                schema: "dbo",
                newName: "Customers");

            migrationBuilder.RenameColumn(
                name: "MovieName",
                table: "Movies",
                newName: "Name");

            migrationBuilder.RenameIndex(
                name: "IX_T_Movie_GenreId",
                table: "Movies",
                newName: "IX_Movies_GenreId");

            migrationBuilder.AlterColumn<Guid>(
                name: "GenreId",
                table: "Movies",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Movies",
                table: "Movies",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Genres",
                table: "Genres",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customers",
                table: "Customers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Genres_GenreId",
                table: "Movies",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
