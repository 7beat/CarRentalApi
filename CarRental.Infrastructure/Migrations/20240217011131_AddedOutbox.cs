using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRental.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedOutbox : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("c6ffb378-937d-4f75-9be3-4b344a9aefee"));

            migrationBuilder.DeleteData(
                table: "Motorcycles",
                keyColumn: "Id",
                keyValue: new Guid("c5a0e954-3fac-482c-a435-1550542c7c12"));

            migrationBuilder.CreateTable(
                name: "InboxState",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MessageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConsumerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LockId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Received = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReceiveCount = table.Column<int>(type: "int", nullable: false),
                    ExpirationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Consumed = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Delivered = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastSequenceNumber = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InboxState", x => x.Id);
                    table.UniqueConstraint("AK_InboxState_MessageId_ConsumerId", x => new { x.MessageId, x.ConsumerId });
                });

            migrationBuilder.CreateTable(
                name: "OutboxMessage",
                columns: table => new
                {
                    SequenceNumber = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnqueueTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SentTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Headers = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Properties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InboxMessageId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    InboxConsumerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OutboxId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MessageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    MessageType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConversationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CorrelationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    InitiatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SourceAddress = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    DestinationAddress = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ResponseAddress = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    FaultAddress = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ExpirationTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutboxMessage", x => x.SequenceNumber);
                });

            migrationBuilder.CreateTable(
                name: "OutboxState",
                columns: table => new
                {
                    OutboxId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LockId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Delivered = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastSequenceNumber = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutboxState", x => x.OutboxId);
                });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "ApplicationUserId", "Brand", "CreatedAt", "CreatedBy", "DateOfProduction", "EngineId", "Model", "NumberOfDoors" },
                values: new object[] { new Guid("9d36f4e1-e22a-43fd-bbe5-8a3aaba2dc2a"), null, "Ford", new DateTime(2024, 2, 17, 2, 11, 31, 266, DateTimeKind.Local).AddTicks(9908), null, new DateTime(2019, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e449eae3-e5b5-41d6-b89f-fecfc0dc9676"), "Mondeo", 5 });

            migrationBuilder.UpdateData(
                table: "Engines",
                keyColumn: "Id",
                keyValue: new Guid("25a79fdb-b76a-45bf-bc1b-e18487d51212"),
                column: "CreatedAt",
                value: new DateTime(2024, 2, 17, 2, 11, 31, 267, DateTimeKind.Local).AddTicks(2014));

            migrationBuilder.UpdateData(
                table: "Engines",
                keyColumn: "Id",
                keyValue: new Guid("5a6d7c68-b19b-4c82-94c5-43c084048092"),
                column: "CreatedAt",
                value: new DateTime(2024, 2, 17, 2, 11, 31, 267, DateTimeKind.Local).AddTicks(2033));

            migrationBuilder.UpdateData(
                table: "Engines",
                keyColumn: "Id",
                keyValue: new Guid("e449eae3-e5b5-41d6-b89f-fecfc0dc9676"),
                column: "CreatedAt",
                value: new DateTime(2024, 2, 17, 2, 11, 31, 267, DateTimeKind.Local).AddTicks(2030));

            migrationBuilder.InsertData(
                table: "Motorcycles",
                columns: new[] { "Id", "ApplicationUserId", "Brand", "CreatedAt", "CreatedBy", "DateOfProduction", "EngineId", "Model", "NumberOfWheels" },
                values: new object[] { new Guid("d26ff4cb-5759-4f5e-9d16-f531e63a6b7d"), null, "Kawasaki", new DateTime(2024, 2, 17, 2, 11, 31, 267, DateTimeKind.Local).AddTicks(2920), null, new DateTime(2016, 3, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("5a6d7c68-b19b-4c82-94c5-43c084048092"), "Ninja", 2 });

            migrationBuilder.CreateIndex(
                name: "IX_InboxState_Delivered",
                table: "InboxState",
                column: "Delivered");

            migrationBuilder.CreateIndex(
                name: "IX_OutboxMessage_EnqueueTime",
                table: "OutboxMessage",
                column: "EnqueueTime");

            migrationBuilder.CreateIndex(
                name: "IX_OutboxMessage_ExpirationTime",
                table: "OutboxMessage",
                column: "ExpirationTime");

            migrationBuilder.CreateIndex(
                name: "IX_OutboxMessage_InboxMessageId_InboxConsumerId_SequenceNumber",
                table: "OutboxMessage",
                columns: new[] { "InboxMessageId", "InboxConsumerId", "SequenceNumber" },
                unique: true,
                filter: "[InboxMessageId] IS NOT NULL AND [InboxConsumerId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_OutboxMessage_OutboxId_SequenceNumber",
                table: "OutboxMessage",
                columns: new[] { "OutboxId", "SequenceNumber" },
                unique: true,
                filter: "[OutboxId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_OutboxState_Created",
                table: "OutboxState",
                column: "Created");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InboxState");

            migrationBuilder.DropTable(
                name: "OutboxMessage");

            migrationBuilder.DropTable(
                name: "OutboxState");

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("9d36f4e1-e22a-43fd-bbe5-8a3aaba2dc2a"));

            migrationBuilder.DeleteData(
                table: "Motorcycles",
                keyColumn: "Id",
                keyValue: new Guid("d26ff4cb-5759-4f5e-9d16-f531e63a6b7d"));

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "ApplicationUserId", "Brand", "CreatedAt", "CreatedBy", "DateOfProduction", "EngineId", "Model", "NumberOfDoors" },
                values: new object[] { new Guid("c6ffb378-937d-4f75-9be3-4b344a9aefee"), null, "Ford", new DateTime(2024, 1, 20, 11, 17, 5, 322, DateTimeKind.Local).AddTicks(5290), null, new DateTime(2019, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e449eae3-e5b5-41d6-b89f-fecfc0dc9676"), "Mondeo", 5 });

            migrationBuilder.UpdateData(
                table: "Engines",
                keyColumn: "Id",
                keyValue: new Guid("25a79fdb-b76a-45bf-bc1b-e18487d51212"),
                column: "CreatedAt",
                value: new DateTime(2024, 1, 20, 11, 17, 5, 322, DateTimeKind.Local).AddTicks(8501));

            migrationBuilder.UpdateData(
                table: "Engines",
                keyColumn: "Id",
                keyValue: new Guid("5a6d7c68-b19b-4c82-94c5-43c084048092"),
                column: "CreatedAt",
                value: new DateTime(2024, 1, 20, 11, 17, 5, 322, DateTimeKind.Local).AddTicks(8533));

            migrationBuilder.UpdateData(
                table: "Engines",
                keyColumn: "Id",
                keyValue: new Guid("e449eae3-e5b5-41d6-b89f-fecfc0dc9676"),
                column: "CreatedAt",
                value: new DateTime(2024, 1, 20, 11, 17, 5, 322, DateTimeKind.Local).AddTicks(8530));

            migrationBuilder.InsertData(
                table: "Motorcycles",
                columns: new[] { "Id", "ApplicationUserId", "Brand", "CreatedAt", "CreatedBy", "DateOfProduction", "EngineId", "Model", "NumberOfWheels" },
                values: new object[] { new Guid("c5a0e954-3fac-482c-a435-1550542c7c12"), null, "Kawasaki", new DateTime(2024, 1, 20, 11, 17, 5, 322, DateTimeKind.Local).AddTicks(9568), null, new DateTime(2016, 3, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("5a6d7c68-b19b-4c82-94c5-43c084048092"), "Ninja", 2 });
        }
    }
}
