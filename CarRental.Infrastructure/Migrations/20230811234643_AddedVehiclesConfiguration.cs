using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRental.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedVehiclesConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FuelType",
                table: "Engines",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "Engines",
                columns: new[] { "Id", "Displacement", "FuelType", "Horsepower", "Model" },
                values: new object[] { 1, 1.0, "Gasoline", 1, "TestEngine" });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "Brand", "DateOfProduction", "EngineId", "Model", "NumberOfDoors" },
                values: new object[] { 1, "Ford", new DateTime(2019, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Mondeo", 5 });

            migrationBuilder.InsertData(
                table: "Motorcycles",
                columns: new[] { "Id", "Brand", "DateOfProduction", "EngineId", "Model", "NumberOfWheels" },
                values: new object[] { 2, "Kawasaki", new DateTime(2016, 3, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Ninja", 2 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Motorcycles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Engines",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AlterColumn<int>(
                name: "FuelType",
                table: "Engines",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
