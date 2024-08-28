using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObjects.Migrations
{
    public partial class RoomInsert : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "RoomID", "Amenities", "Floor", "Price", "RoomNumber", "RoomType", "Status" },
                values: new object[,]
                {
                    { 1, "Wi-Fi, TV", 1, 100.00m, "101", "Standard", "Available" },
                    { 2, "Wi-Fi, TV, Mini-bar", 2, 150.00m, "201", "Deluxe", "Occupied" },
                    { 3, "Wi-Fi, TV, Mini-bar, Jacuzzi", 3, 200.00m, "301", "Suite", "Available" },
                    { 4, "Wi-Fi, TV", 1, 100.00m, "104", "Standard", "Available" },
                    { 5, "Wi-Fi, TV, Mini-bar", 2, 150.00m, "205", "Deluxe", "Cleaning" },
                    { 6, "Wi-Fi, TV, Mini-bar, Jacuzzi", 3, 200.00m, "306", "Suite", "Occupied" },
                    { 7, "Wi-Fi, TV", 1, 100.00m, "107", "Standard", "Available" },
                    { 8, "Wi-Fi, TV, Mini-bar", 2, 150.00m, "208", "Deluxe", "Available" },
                    { 9, "Wi-Fi, TV, Mini-bar, Jacuzzi", 3, 200.00m, "309", "Suite", "Maintenance" },
                    { 10, "Wi-Fi, TV", 1, 100.00m, "110", "Standard", "Available" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "RoomID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "RoomID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "RoomID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "RoomID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "RoomID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "RoomID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "RoomID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "RoomID",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "RoomID",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "RoomID",
                keyValue: 10);
        }
    }
}
