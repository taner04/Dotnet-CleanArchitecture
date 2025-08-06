using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatetAtNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "User",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Product",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "OrderItem",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Order",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Jwt",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.InsertData(
                table: "Order",
                columns: new[] { "Id", "Amount", "CreatedAt", "IsDeleted", "OrderDate", "OrderStatus", "PaymentMethod", "TrackingNumber", "UpdatedAt", "UserId" },
                values: new object[] { 1, 2399.98m, new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, 3, "CD587DB4-C345-4D86-9D9C-F9C0A0BF50D3", null, 1 });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "CreatedAt", "Description", "ImageUrl", "IsDeleted", "Name", "Price", "Stock", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), "A powerful laptop", "https://example.com/laptop.jpg", false, "Laptop", 1499.99m, 20, null },
                    { 2, new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), "A modern smartphone", "https://example.com/phone.jpg", false, "Smartphone", 899.99m, 50, null },
                    { 3, new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), "A lightweight tablet", "https://example.com/tablet.jpg", false, "Tablet", 499.99m, 35, null },
                    { 4, new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), "27-inch 4K monitor", "https://example.com/monitor.jpg", false, "Monitor", 329.99m, 15, null },
                    { 5, new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Mechanical keyboard", "https://example.com/keyboard.jpg", false, "Keyboard", 89.99m, 60, null },
                    { 6, new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Wireless mouse", "https://example.com/mouse.jpg", false, "Mouse", 49.99m, 80, null },
                    { 7, new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Noise-cancelling headphones", "https://example.com/headphones.jpg", false, "Headphones", 199.99m, 40, null },
                    { 8, new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), "HD webcam", "https://example.com/webcam.jpg", false, "Webcam", 69.99m, 30, null },
                    { 9, new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Laser printer", "https://example.com/printer.jpg", false, "Printer", 159.99m, 10, null },
                    { 10, new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Wi-Fi 6 router", "https://example.com/router.jpg", false, "Router", 129.99m, 25, null },
                    { 11, new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), "1TB portable SSD", "https://example.com/ssd.jpg", false, "External SSD", 119.99m, 45, null },
                    { 12, new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Fitness smartwatch", "https://example.com/smartwatch.jpg", false, "Smartwatch", 249.99m, 22, null },
                    { 13, new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Bluetooth speakers", "https://example.com/speakers.jpg", false, "Speakers", 79.99m, 55, null },
                    { 14, new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Ergonomic gaming chair", "https://example.com/chair.jpg", false, "Gaming Chair", 299.99m, 12, null },
                    { 15, new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), "USB condenser microphone", "https://example.com/microphone.jpg", false, "Microphone", 109.99m, 18, null },
                    { 16, new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), "High-end graphics card", "https://example.com/gpu.jpg", false, "Graphics Card", 799.99m, 8, null },
                    { 17, new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), "20000mAh power bank", "https://example.com/powerbank.jpg", false, "Power Bank", 39.99m, 70, null },
                    { 18, new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Virtual reality headset", "https://example.com/vr.jpg", false, "VR Headset", 399.99m, 6, null },
                    { 19, new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Voice-controlled smart hub", "https://example.com/hub.jpg", false, "Smart Home Hub", 99.99m, 28, null },
                    { 20, new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Camera drone", "https://example.com/drone.jpg", false, "Drone", 599.99m, 5, null }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "CreatedAt", "Email", "Firstname", "IsDeleted", "Lastname", "Password", "UpdatedAt" },
                values: new object[] { 1, new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), "doe@mail.com", "John", false, "Doe", "$2a$12$Eju2KmPviy2UCJIUAlTtr.LFZ/DbdsFOOlN3YEoP5p30HLxwe1YXG", null });

            migrationBuilder.InsertData(
                table: "Jwt",
                columns: new[] { "Id", "CreatedAt", "Expiration", "IsDeleted", "RefreshToken", "RefreshTokenExpiration", "Token", "UpdatedAt", "UserId" },
                values: new object[] { 1, new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJ0ZXN0QG1haWwuY29tIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvZW1haWxhZGRyZXNzIjoidGVzdEBtYWlsLmNvbSIsIm5iZiI6MTc1Mzg4NTQyOSwiZXhwIjoxNzU0NDkwMjI5LCJpc3MiOiJodHRwczovL3d3dy5lU2hvcC1hcGkuY29tIiwiYXVkIjoiZVNob3AtQXBpIn0.6byhPpfHXqbF2lDjOUgWoQ8v8O45Bnbh_R8W0pib2oA", new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJ0ZXN0QG1haWwuY29tIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvZW1haWxhZGRyZXNzIjoidGVzdEBtYWlsLmNvbSIsIm5iZiI6MTc1Mzg4NTQyOSwiZXhwIjoxNzUzODkyNjI5LCJpc3MiOiJodHRwczovL3d3dy5lU2hvcC1hcGkuY29tIiwiYXVkIjoiZVNob3AtQXBpIn0.LJf4HNEJE8KLwnfBaVcO-MbJI_vXNAg_ZDdfkIwUoZ4", null, 1 });

            migrationBuilder.InsertData(
                table: "OrderItem",
                columns: new[] { "Id", "Amount", "CreatedAt", "IsDeleted", "OrderId", "ProductId", "Quantity", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 1499.99m, new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 1, 1, 1, null },
                    { 2, 899.99m, new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 1, 2, 1, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Jwt",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "OrderItem",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "OrderItem",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "User",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Product",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "OrderItem",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Order",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Jwt",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);
        }
    }
}
