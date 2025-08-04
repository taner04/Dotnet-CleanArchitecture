using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Order",
                columns: new[] { "Id", "Amount", "CreatedAt", "IsDeleted", "OrderDate", "OrderStatus", "PaymentMethod", "UpdatedAt", "UserId" },
                values: new object[] { 1, 2399.98m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, 3, null, 1 });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "CreatedAt", "Description", "ImageUrl", "IsDeleted", "Name", "Price", "Stock", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A powerful laptop", "https://example.com/laptop.jpg", false, "Laptop", 1499.99m, 20, null },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A modern smartphone", "https://example.com/phone.jpg", false, "Smartphone", 899.99m, 50, null }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "CreatedAt", "Email", "Firstname", "IsDeleted", "Lastname", "Password", "UpdatedAt" },
                values: new object[] { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "doe@mail.com", "John", false, "Doe", "$2a$12$Eju2KmPviy2UCJIUAlTtr.LFZ/DbdsFOOlN3YEoP5p30HLxwe1YXG", null });

            migrationBuilder.InsertData(
                table: "Jwt",
                columns: new[] { "Id", "CreatedAt", "Expiration", "IsDeleted", "RefreshToken", "RefreshTokenExpiration", "Token", "UpdatedAt", "UserId" },
                values: new object[] { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJ0ZXN0QG1haWwuY29tIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvZW1haWxhZGRyZXNzIjoidGVzdEBtYWlsLmNvbSIsIm5iZiI6MTc1Mzg4NTQyOSwiZXhwIjoxNzU0NDkwMjI5LCJpc3MiOiJodHRwczovL3d3dy5lU2hvcC1hcGkuY29tIiwiYXVkIjoiZVNob3AtQXBpIn0.6byhPpfHXqbF2lDjOUgWoQ8v8O45Bnbh_R8W0pib2oA", new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJ0ZXN0QG1haWwuY29tIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvZW1haWxhZGRyZXNzIjoidGVzdEBtYWlsLmNvbSIsIm5iZiI6MTc1Mzg4NTQyOSwiZXhwIjoxNzUzODkyNjI5LCJpc3MiOiJodHRwczovL3d3dy5lU2hvcC1hcGkuY29tIiwiYXVkIjoiZVNob3AtQXBpIn0.LJf4HNEJE8KLwnfBaVcO-MbJI_vXNAg_ZDdfkIwUoZ4", null, 1 });

            migrationBuilder.InsertData(
                table: "OrderItem",
                columns: new[] { "Id", "Amount", "CreatedAt", "IsDeleted", "OrderId", "ProductId", "Quantity", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 1499.99m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 1, 1, 1, null },
                    { 2, 899.99m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 1, 2, 1, null }
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
        }
    }
}
