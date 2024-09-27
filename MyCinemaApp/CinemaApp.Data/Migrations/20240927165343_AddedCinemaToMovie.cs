using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CinemaApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedCinemaToMovie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("8d344c0f-37bc-44b4-8533-91161ca672d7"));

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("a40806f4-a633-4c55-89fe-73faf23f348f"));

            migrationBuilder.CreateTable(
                name: "Cinemas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(85)", maxLength: 85, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cinemas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CinemaMovies",
                columns: table => new
                {
                    CinemaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MovieId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CinemaMovies", x => new { x.MovieId, x.CinemaId });
                    table.ForeignKey(
                        name: "FK_CinemaMovies_Cinemas_CinemaId",
                        column: x => x.CinemaId,
                        principalTable: "Cinemas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CinemaMovies_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Cinemas",
                columns: new[] { "Id", "Location", "Name" },
                values: new object[,]
                {
                    { new Guid("15979078-1398-4e1c-93ed-50047125bcc5"), "Burgas", "Cinema City" },
                    { new Guid("7f27c5c6-97d9-4156-a8ff-8b92078bdfb2"), "Plovdiv", "Imax" },
                    { new Guid("e308d9c0-2915-4ed0-87c6-6558b98b1ff1"), "Sofia", "Cinema City" }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Description", "Director", "Duration", "Genre", "ReleaseDate", "Title" },
                values: new object[,]
                {
                    { new Guid("500fbf8b-8808-4feb-960b-34f20aa756b3"), "The Fall Guy is a 2024 American action comedy film directed by David Leitch and written by Drew Pearce, loosely based on the 1980s TV series. The plot follows a stuntman (Ryan Gosling) working on his ex-girlfriend's (Emily Blunt) directorial debut action film, only to find himself involved in a conspiracy surrounding the film's lead actor (Aaron Taylor-Johnson). ", "David Leech", 125, "Action", new DateTime(2024, 5, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Fall Guy" },
                    { new Guid("d8e95ae7-d2f3-419d-be3e-7f36d243873e"), "Deadpool & Wolverine is a 2024 American superhero film based on Marvel Comics featuring the characters Deadpool and Wolverine. Produced by Marvel Studios, Maximum Effort, and 21 Laps Entertainment, and distributed by Walt Disney Studios Motion Pictures, it is the 34th film in the Marvel Cinematic Universe (MCU) and the sequel to Deadpool (2016) and Deadpool 2 (2018). ", "Shaun Levi", 127, "Action", new DateTime(2024, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Deadpool & Wolverine" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CinemaMovies_CinemaId",
                table: "CinemaMovies",
                column: "CinemaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CinemaMovies");

            migrationBuilder.DropTable(
                name: "Cinemas");

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("500fbf8b-8808-4feb-960b-34f20aa756b3"));

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("d8e95ae7-d2f3-419d-be3e-7f36d243873e"));

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Description", "Director", "Duration", "Genre", "ReleaseDate", "Title" },
                values: new object[,]
                {
                    { new Guid("8d344c0f-37bc-44b4-8533-91161ca672d7"), "The Fall Guy is a 2024 American action comedy film directed by David Leitch and written by Drew Pearce, loosely based on the 1980s TV series. The plot follows a stuntman (Ryan Gosling) working on his ex-girlfriend's (Emily Blunt) directorial debut action film, only to find himself involved in a conspiracy surrounding the film's lead actor (Aaron Taylor-Johnson). ", "David Leech", 125, "Action", new DateTime(2024, 5, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Fall Guy" },
                    { new Guid("a40806f4-a633-4c55-89fe-73faf23f348f"), "Deadpool & Wolverine is a 2024 American superhero film based on Marvel Comics featuring the characters Deadpool and Wolverine. Produced by Marvel Studios, Maximum Effort, and 21 Laps Entertainment, and distributed by Walt Disney Studios Motion Pictures, it is the 34th film in the Marvel Cinematic Universe (MCU) and the sequel to Deadpool (2016) and Deadpool 2 (2018). ", "Shaun Levi", 127, "Action", new DateTime(2024, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Deadpool & Wolverine" }
                });
        }
    }
}
