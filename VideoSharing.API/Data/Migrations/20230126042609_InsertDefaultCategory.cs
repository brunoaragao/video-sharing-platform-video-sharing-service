// <copyright file="20230126042609_InsertDefaultCategory.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VideoSharing.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class InsertDefaultCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "category",
                columns: new[] { "id", "color", "name" },
                values: new object[] { 1, "#000000", "Default" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "category",
                keyColumn: "id",
                keyValue: 1);
        }
    }
}