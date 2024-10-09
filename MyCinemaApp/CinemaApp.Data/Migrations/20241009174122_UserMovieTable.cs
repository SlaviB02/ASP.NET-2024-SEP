using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CinemaApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class UserMovieTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: new Guid("244204ac-e07d-4935-b59d-45dd21956a34"));

            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: new Guid("753275d9-463a-4edf-bed5-e12d38fe3afe"));

            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: new Guid("962cd374-fac1-4047-96ea-e0be4ffbfe78"));

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("070671dd-d1ba-4831-b538-fb68ccac13a4"));

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("d64be79a-6651-4f91-ab08-d11a4836b30d"));

            migrationBuilder.CreateTable(
                name: "UsersMovies",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MovieId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersMovies", x => new { x.UserId, x.MovieId });
                    table.ForeignKey(
                        name: "FK_UsersMovies_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersMovies_Movies_MovieId",
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
                    { new Guid("056f0004-2088-47e8-a546-6b05731d844a"), "Sofia", "Cinema City" },
                    { new Guid("bba375ca-513c-4183-a31b-3635db0797dc"), "Burgas", "Cinema City" },
                    { new Guid("c40827e8-dfea-4aef-bfe9-f5677ff105ad"), "Plovdiv", "Imax" }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Description", "Director", "Duration", "Genre", "ReleaseDate", "Title" },
                values: new object[,]
                {
                    { new Guid("29d0a26f-d6b4-41af-8750-243cc23de542"), "The Fall Guy is a 2024 American action comedy film directed by David Leitch and written by Drew Pearce, loosely based on the 1980s TV series. The plot follows a stuntman (Ryan Gosling) working on his ex-girlfriend's (Emily Blunt) directorial debut action film, only to find himself involved in a conspiracy surrounding the film's lead actor (Aaron Taylor-Johnson). ", "David Leech", 125, "Action", new DateTime(2024, 5, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Fall Guy" },
                    { new Guid("e6a8ad1b-75d5-403f-a70c-896bd80af623"), "Deadpool & Wolverine is a 2024 American superhero film based on Marvel Comics featuring the characters Deadpool and Wolverine. Produced by Marvel Studios, Maximum Effort, and 21 Laps Entertainment, and distributed by Walt Disney Studios Motion Pictures, it is the 34th film in the Marvel Cinematic Universe (MCU) and the sequel to Deadpool (2016) and Deadpool 2 (2018). ", "Shaun Levi", 127, "Action", new DateTime(2024, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Deadpool & Wolverine" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsersMovies_MovieId",
                table: "UsersMovies",
                column: "MovieId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsersMovies");

            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: new Guid("056f0004-2088-47e8-a546-6b05731d844a"));

            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: new Guid("bba375ca-513c-4183-a31b-3635db0797dc"));

            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: new Guid("c40827e8-dfea-4aef-bfe9-f5677ff105ad"));

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("29d0a26f-d6b4-41af-8750-243cc23de542"));

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("e6a8ad1b-75d5-403f-a70c-896bd80af623"));

            migrationBuilder.InsertData(
                table: "Cinemas",
                columns: new[] { "Id", "Location", "Name" },
                values: new object[,]
                {
                    { new Guid("244204ac-e07d-4935-b59d-45dd21956a34"), "Plovdiv", "Imax" },
                    { new Guid("753275d9-463a-4edf-bed5-e12d38fe3afe"), "Sofia", "Cinema City" },
                    { new Guid("962cd374-fac1-4047-96ea-e0be4ffbfe78"), "Burgas", "Cinema City" }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Description", "Director", "Duration", "Genre", "ReleaseDate", "Title" },
                values: new object[,]
                {
                    { new Guid("070671dd-d1ba-4831-b538-fb68ccac13a4"), "The Fall Guy is a 2024 American action comedy film directed by David Leitch and written by Drew Pearce, loosely based on the 1980s TV series. The plot follows a stuntman (Ryan Gosling) working on his ex-girlfriend's (Emily Blunt) directorial debut action film, only to find himself involved in a conspiracy surrounding the film's lead actor (Aaron Taylor-Johnson). ", "David Leech", 125, "Action", new DateTime(2024, 5, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Fall Guy" },
                    { new Guid("d64be79a-6651-4f91-ab08-d11a4836b30d"), "Deadpool & Wolverine is a 2024 American superhero film based on Marvel Comics featuring the characters Deadpool and Wolverine. Produced by Marvel Studios, Maximum Effort, and 21 Laps Entertainment, and distributed by Walt Disney Studios Motion Pictures, it is the 34th film in the Marvel Cinematic Universe (MCU) and the sequel to Deadpool (2016) and Deadpool 2 (2018). ", "Shaun Levi", 127, "Action", new DateTime(2024, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Deadpool & Wolverine" }
                });
        }
    }
}
