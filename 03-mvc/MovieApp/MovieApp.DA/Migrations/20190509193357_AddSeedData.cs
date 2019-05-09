﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieApp.DA.Migrations
{
    public partial class AddSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Genre",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Action" });

            migrationBuilder.InsertData(
                table: "Genre",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Drama" });

            migrationBuilder.InsertData(
                table: "Movie",
                columns: new[] { "Id", "GenreId", "ReleaseDate", "Title" },
                values: new object[] { 1, 1, new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Star Wars IV" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Genre",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Movie",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Genre",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
