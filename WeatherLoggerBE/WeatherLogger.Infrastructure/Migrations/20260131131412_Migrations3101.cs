using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WeatherLogger.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Migrations3101 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApiLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Endpoint = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Method = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequestBody = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResponseBody = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusCode = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GoogleId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GameCityWeathers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AverageTemperatureCelsius = table.Column<double>(type: "float", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameCityWeathers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WeatherRecordHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TemperatureCelsius = table.Column<double>(type: "float", nullable: false),
                    ObservationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherRecordHistory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WeatherRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TemperatureCelsius = table.Column<double>(type: "float", nullable: false),
                    Humidity = table.Column<int>(type: "int", nullable: false),
                    WindSpeed = table.Column<double>(type: "float", nullable: false),
                    WeatherDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ObservationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherRecords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserCityHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CheckedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AppUserId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCityHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserCityHistories_AppUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "GameCityWeathers",
                columns: new[] { "Id", "AverageTemperatureCelsius", "CityName", "CountryName", "CreatedAt" },
                values: new object[,]
                {
                    { 1, 12.0, "Sarajevo", "BA", new DateTime(2026, 1, 31, 14, 14, 12, 462, DateTimeKind.Local).AddTicks(3255) },
                    { 2, 10.0, "London", "UK", new DateTime(2026, 1, 31, 14, 14, 12, 462, DateTimeKind.Local).AddTicks(3317) },
                    { 3, 28.0, "Cairo", "EG", new DateTime(2026, 1, 31, 14, 14, 12, 462, DateTimeKind.Local).AddTicks(3319) },
                    { 4, 4.0, "Oslo", "NO", new DateTime(2026, 1, 31, 14, 14, 12, 462, DateTimeKind.Local).AddTicks(3321) },
                    { 5, 18.0, "Madrid", "ES", new DateTime(2026, 1, 31, 14, 14, 12, 462, DateTimeKind.Local).AddTicks(3323) },
                    { 6, 12.0, "Paris", "FR", new DateTime(2026, 1, 31, 14, 14, 12, 462, DateTimeKind.Local).AddTicks(3327) },
                    { 7, 10.0, "Berlin", "DE", new DateTime(2026, 1, 31, 14, 14, 12, 462, DateTimeKind.Local).AddTicks(3329) },
                    { 8, 16.0, "Rome", "IT", new DateTime(2026, 1, 31, 14, 14, 12, 462, DateTimeKind.Local).AddTicks(3331) },
                    { 9, 20.0, "Athens", "GR", new DateTime(2026, 1, 31, 14, 14, 12, 462, DateTimeKind.Local).AddTicks(3333) },
                    { 10, 5.0, "Moscow", "RU", new DateTime(2026, 1, 31, 14, 14, 12, 462, DateTimeKind.Local).AddTicks(3336) },
                    { 11, 15.0, "Tokyo", "JP", new DateTime(2026, 1, 31, 14, 14, 12, 462, DateTimeKind.Local).AddTicks(3338) },
                    { 12, 22.0, "Sydney", "AU", new DateTime(2026, 1, 31, 14, 14, 12, 462, DateTimeKind.Local).AddTicks(3340) },
                    { 13, 13.0, "Beijing", "CN", new DateTime(2026, 1, 31, 14, 14, 12, 462, DateTimeKind.Local).AddTicks(3342) },
                    { 14, 12.0, "New York", "US", new DateTime(2026, 1, 31, 14, 14, 12, 462, DateTimeKind.Local).AddTicks(3344) },
                    { 15, 19.0, "Los Angeles", "US", new DateTime(2026, 1, 31, 14, 14, 12, 462, DateTimeKind.Local).AddTicks(3345) },
                    { 16, 7.0, "Toronto", "CA", new DateTime(2026, 1, 31, 14, 14, 12, 462, DateTimeKind.Local).AddTicks(3347) },
                    { 17, 10.0, "Vancouver", "CA", new DateTime(2026, 1, 31, 14, 14, 12, 462, DateTimeKind.Local).AddTicks(3349) },
                    { 18, 17.0, "Mexico City", "MX", new DateTime(2026, 1, 31, 14, 14, 12, 462, DateTimeKind.Local).AddTicks(3352) },
                    { 19, 26.0, "Rio de Janeiro", "BR", new DateTime(2026, 1, 31, 14, 14, 12, 462, DateTimeKind.Local).AddTicks(3354) },
                    { 20, 18.0, "Buenos Aires", "AR", new DateTime(2026, 1, 31, 14, 14, 12, 462, DateTimeKind.Local).AddTicks(3356) },
                    { 21, 17.0, "Cape Town", "ZA", new DateTime(2026, 1, 31, 14, 14, 12, 462, DateTimeKind.Local).AddTicks(3358) },
                    { 22, 16.0, "Johannesburg", "ZA", new DateTime(2026, 1, 31, 14, 14, 12, 462, DateTimeKind.Local).AddTicks(3359) },
                    { 23, 15.0, "Istanbul", "TR", new DateTime(2026, 1, 31, 14, 14, 12, 462, DateTimeKind.Local).AddTicks(3361) },
                    { 24, 30.0, "Dubai", "AE", new DateTime(2026, 1, 31, 14, 14, 12, 462, DateTimeKind.Local).AddTicks(3363) },
                    { 25, 28.0, "Singapore", "SG", new DateTime(2026, 1, 31, 14, 14, 12, 462, DateTimeKind.Local).AddTicks(3365) },
                    { 26, 29.0, "Bangkok", "TH", new DateTime(2026, 1, 31, 14, 14, 12, 462, DateTimeKind.Local).AddTicks(3367) },
                    { 27, 25.0, "Delhi", "IN", new DateTime(2026, 1, 31, 14, 14, 12, 462, DateTimeKind.Local).AddTicks(3369) },
                    { 28, 13.0, "Seoul", "KR", new DateTime(2026, 1, 31, 14, 14, 12, 462, DateTimeKind.Local).AddTicks(3371) },
                    { 29, 3.0, "Helsinki", "FI", new DateTime(2026, 1, 31, 14, 14, 12, 462, DateTimeKind.Local).AddTicks(3373) },
                    { 30, 5.0, "Stockholm", "SE", new DateTime(2026, 1, 31, 14, 14, 12, 462, DateTimeKind.Local).AddTicks(3375) },
                    { 31, 8.0, "Warsaw", "PL", new DateTime(2026, 1, 31, 14, 14, 12, 462, DateTimeKind.Local).AddTicks(3376) },
                    { 32, 16.0, "Lisbon", "PT", new DateTime(2026, 1, 31, 14, 14, 12, 462, DateTimeKind.Local).AddTicks(3378) },
                    { 33, 11.0, "Budapest", "HU", new DateTime(2026, 1, 31, 14, 14, 12, 462, DateTimeKind.Local).AddTicks(3380) },
                    { 34, 9.0, "Prague", "CZ", new DateTime(2026, 1, 31, 14, 14, 12, 462, DateTimeKind.Local).AddTicks(3383) },
                    { 35, 28.0, "Kuala Lumpur", "MY", new DateTime(2026, 1, 31, 14, 14, 12, 462, DateTimeKind.Local).AddTicks(3385) },
                    { 36, 27.0, "Lagos", "NG", new DateTime(2026, 1, 31, 14, 14, 12, 462, DateTimeKind.Local).AddTicks(3387) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserCityHistories_AppUserId",
                table: "UserCityHistories",
                column: "AppUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApiLogs");

            migrationBuilder.DropTable(
                name: "GameCityWeathers");

            migrationBuilder.DropTable(
                name: "UserCityHistories");

            migrationBuilder.DropTable(
                name: "WeatherRecordHistory");

            migrationBuilder.DropTable(
                name: "WeatherRecords");

            migrationBuilder.DropTable(
                name: "AppUsers");
        }
    }
}
