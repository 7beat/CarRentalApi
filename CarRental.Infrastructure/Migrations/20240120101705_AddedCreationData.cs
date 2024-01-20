using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRental.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedCreationData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("7ee6a395-7d7a-45ff-ae3b-d92e964c3cc6"));

            migrationBuilder.DeleteData(
                table: "Motorcycles",
                keyColumn: "Id",
                keyValue: new Guid("df73853b-7241-4839-b00a-95d26c794d26"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Rentals",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Rentals",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Motorcycles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Motorcycles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Engines",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Engines",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Cars",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "ApplicationUserId", "Brand", "CreatedAt", "CreatedBy", "DateOfProduction", "EngineId", "Model", "NumberOfDoors" },
                values: new object[] { new Guid("c6ffb378-937d-4f75-9be3-4b344a9aefee"), null, "Ford", new DateTime(2024, 1, 20, 11, 17, 5, 322, DateTimeKind.Local).AddTicks(5290), null, new DateTime(2019, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e449eae3-e5b5-41d6-b89f-fecfc0dc9676"), "Mondeo", 5 });

            migrationBuilder.UpdateData(
                table: "Engines",
                keyColumn: "Id",
                keyValue: new Guid("25a79fdb-b76a-45bf-bc1b-e18487d51212"),
                columns: new[] { "CreatedAt", "CreatedBy" },
                values: new object[] { new DateTime(2024, 1, 20, 11, 17, 5, 322, DateTimeKind.Local).AddTicks(8501), null });

            migrationBuilder.UpdateData(
                table: "Engines",
                keyColumn: "Id",
                keyValue: new Guid("5a6d7c68-b19b-4c82-94c5-43c084048092"),
                columns: new[] { "CreatedAt", "CreatedBy" },
                values: new object[] { new DateTime(2024, 1, 20, 11, 17, 5, 322, DateTimeKind.Local).AddTicks(8533), null });

            migrationBuilder.UpdateData(
                table: "Engines",
                keyColumn: "Id",
                keyValue: new Guid("e449eae3-e5b5-41d6-b89f-fecfc0dc9676"),
                columns: new[] { "CreatedAt", "CreatedBy" },
                values: new object[] { new DateTime(2024, 1, 20, 11, 17, 5, 322, DateTimeKind.Local).AddTicks(8530), null });

            migrationBuilder.InsertData(
                table: "Motorcycles",
                columns: new[] { "Id", "ApplicationUserId", "Brand", "CreatedAt", "CreatedBy", "DateOfProduction", "EngineId", "Model", "NumberOfWheels" },
                values: new object[] { new Guid("c5a0e954-3fac-482c-a435-1550542c7c12"), null, "Kawasaki", new DateTime(2024, 1, 20, 11, 17, 5, 322, DateTimeKind.Local).AddTicks(9568), null, new DateTime(2016, 3, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("5a6d7c68-b19b-4c82-94c5-43c084048092"), "Ninja", 2 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("c6ffb378-937d-4f75-9be3-4b344a9aefee"));

            migrationBuilder.DeleteData(
                table: "Motorcycles",
                keyColumn: "Id",
                keyValue: new Guid("c5a0e954-3fac-482c-a435-1550542c7c12"));

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Motorcycles");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Motorcycles");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Engines");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Engines");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Cars");

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "ApplicationUserId", "Brand", "DateOfProduction", "EngineId", "Model", "NumberOfDoors" },
                values: new object[] { new Guid("7ee6a395-7d7a-45ff-ae3b-d92e964c3cc6"), null, "Ford", new DateTime(2019, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e449eae3-e5b5-41d6-b89f-fecfc0dc9676"), "Mondeo", 5 });

            migrationBuilder.InsertData(
                table: "Motorcycles",
                columns: new[] { "Id", "ApplicationUserId", "Brand", "DateOfProduction", "EngineId", "Model", "NumberOfWheels" },
                values: new object[] { new Guid("df73853b-7241-4839-b00a-95d26c794d26"), null, "Kawasaki", new DateTime(2016, 3, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("5a6d7c68-b19b-4c82-94c5-43c084048092"), "Ninja", 2 });
        }
    }
}
