using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CinemaApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class ImageUrlDefaultValue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "~/images/no-image.jfif");

            migrationBuilder.InsertData(
                table: "Cinemas",
                columns: new[] { "Id", "Location", "Name" },
                values: new object[,]
                {
                    { new Guid("6b60cc18-ff1f-4003-a936-c954f2eb562f"), "Plovdiv", "Imax" },
                    { new Guid("996896d0-9951-447e-8eec-6768ce8e0676"), "Sofia", "Cinema City" },
                    { new Guid("d3f2839c-e7d3-4b19-83aa-328367e26fb5"), "Burgas", "Cinema City" }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Description", "Director", "Duration", "Genre", "ReleaseDate", "Title" },
                values: new object[,]
                {
                    { new Guid("28f7a9fc-1ab9-4125-91ba-af533bd0125e"), "Deadpool & Wolverine is a 2024 American superhero film based on Marvel Comics featuring the characters Deadpool and Wolverine. Produced by Marvel Studios, Maximum Effort, and 21 Laps Entertainment, and distributed by Walt Disney Studios Motion Pictures, it is the 34th film in the Marvel Cinematic Universe (MCU) and the sequel to Deadpool (2016) and Deadpool 2 (2018). ", "Shaun Levi", 127, "Action", new DateTime(2024, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Deadpool & Wolverine" },
                    { new Guid("c370ab68-c08a-4d75-8770-4c3980a2fcbc"), "The Fall Guy is a 2024 American action comedy film directed by David Leitch and written by Drew Pearce, loosely based on the 1980s TV series. The plot follows a stuntman (Ryan Gosling) working on his ex-girlfriend's (Emily Blunt) directorial debut action film, only to find himself involved in a conspiracy surrounding the film's lead actor (Aaron Taylor-Johnson). ", "David Leech", 125, "Action", new DateTime(2024, 5, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Fall Guy" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: new Guid("6b60cc18-ff1f-4003-a936-c954f2eb562f"));

            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: new Guid("996896d0-9951-447e-8eec-6768ce8e0676"));

            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: new Guid("d3f2839c-e7d3-4b19-83aa-328367e26fb5"));

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("28f7a9fc-1ab9-4125-91ba-af533bd0125e"));

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("c370ab68-c08a-4d75-8770-4c3980a2fcbc"));

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Movies");

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
        }
    }
}
