using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PaymentService.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Pricing",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    PriceTitle = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PriceType = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ValueName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ValueDescription = table.Column<double>(type: "double", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pricing", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Pricing",
                columns: new[] { "Id", "PriceTitle", "PriceType", "ValueDescription", "ValueName" },
                values: new object[,]
                {
                    { new Guid("08744e09-bc13-4d36-9fd7-1a2872a4c8ea"), "vehicleClassification", "modifier", 1.2, "N2" },
                    { new Guid("0883a3cb-51fb-48f3-b9a4-c7c66437b935"), "vehicleClassification", "modifier", 1.1000000000000001, "N" },
                    { new Guid("12caf2cb-5134-4840-a9ce-fc0d66b5d492"), "vehicleClassification", "modifier", 1.25, "M3" },
                    { new Guid("1328698f-6b2c-425a-a699-415260d18d70"), "vehicleClassification", "modifier", 1.1000000000000001, "G" },
                    { new Guid("19a96643-4227-4f17-b742-1bd2372a0eb6"), "boundary", "modifier", 1.1000000000000001, "tertiary" },
                    { new Guid("280bdbe2-930f-4db1-a3a6-edd7dbd48318"), "vehicleClassification", "modifier", 1.25, "N3" },
                    { new Guid("2cae3bdb-e585-47d2-bedc-3b38f38c104d"), "highway", "modifier", 1.1000000000000001, "tertiary" },
                    { new Guid("3148778d-6c6d-46ec-82b9-5bb4f4f27d5f"), "rushPrice", "modifier", 1.2, "7" },
                    { new Guid("3b333747-c307-4354-92e0-e857d2ed1f65"), "vehicleClassification", "modifier", 1.1499999999999999, "M1" },
                    { new Guid("552c09b1-4fa3-4cfd-b1d5-860887584f80"), "vehicleClassification", "modifier", 1.2, "M2" },
                    { new Guid("58eb1118-e434-4905-8bba-d1b896a932d8"), "vehicleClassification", "modifier", 1.1000000000000001, "O" },
                    { new Guid("658b15e7-fae4-480a-bfea-b550f1a6e4a0"), "vehicleClassification", "modifier", 1.1000000000000001, "L" },
                    { new Guid("6a2da48a-322d-48c3-a536-6904ba4fb031"), "vehicleClassification", "modifier", 1.2, "O2" },
                    { new Guid("6d36fe48-463f-494d-bcb9-3f3ed3b47789"), "rushPrice", "modifier", 1.2, "8" },
                    { new Guid("6f1f3859-57d4-42e7-bcb9-32b62487c2da"), "fuelType", "modifier", 1.1000000000000001, "Diesel" },
                    { new Guid("77f23aa5-ba42-46e5-b071-427acbd692df"), "highway", "modifier", 1.2, "secondary" },
                    { new Guid("809d484a-e8ad-4c65-adbb-c5bc91ebc7e3"), "rushPrice", "modifier", 1.2, "18" },
                    { new Guid("8b61c8da-bb12-4684-9c2f-c268a917b553"), "vehicleClassification", "modifier", 1.1000000000000001, "R" },
                    { new Guid("8c19a10a-9b90-4d07-8047-5ada8319dcb3"), "vehicleClassification", "modifier", 1.1000000000000001, "M" },
                    { new Guid("91e556ac-49a0-4a0c-b796-71eac1b19db7"), "vehicleClassification", "modifier", 1.3500000000000001, "L5" },
                    { new Guid("92e23192-6b44-48a4-b006-b6724ff8a4fc"), "vehicleClassification", "modifier", 1.25, "O4" },
                    { new Guid("94167264-a926-49fc-9e17-def1d4843a26"), "vehicleClassification", "modifier", 1.1499999999999999, "O1" },
                    { new Guid("954fe08c-032b-411b-8a62-3f3b687f0e30"), "highway", "modifier", 1.25, "primary" },
                    { new Guid("971de403-4d02-4165-80f7-5d9b275db6a9"), "vehicleClassification", "modifier", 1.1000000000000001, "T" },
                    { new Guid("a0e3b01a-793f-4204-ab41-73e6509619a2"), "boundary", "modifier", 1.05, "administrative" },
                    { new Guid("a5c9ee75-9d55-4ac0-873f-dc7bd8144055"), "fuelType", "modifier", 1.05, "Elektriciteit" },
                    { new Guid("a71f3a9c-7fb7-4fe9-989d-0902efe9bfdc"), "boundary", "modifier", 1.05, "suburb" },
                    { new Guid("afbf2564-bd36-4339-ac0c-5acc337cb3d7"), "vehicleClassification", "modifier", 1.1499999999999999, "L1" },
                    { new Guid("b4eedf22-c6ae-45a8-96b3-683869edf5a2"), "vehicleClassification", "modifier", 1.25, "L3" },
                    { new Guid("b555e1de-ea94-4b05-b255-5ef630535d07"), "vehicleClassification", "modifier", 1.1499999999999999, "N1" },
                    { new Guid("b953e1b6-3848-4024-a888-be952294ab0b"), "vehicleClassification", "modifier", 1.1000000000000001, "S" },
                    { new Guid("b957f434-8533-4efa-b1db-28266b496d7e"), "fuelType", "modifier", 1.2, "Brandstof" },
                    { new Guid("b9777bb2-c9dd-4634-a4e8-9f2ee6f4f2d6"), "vehicleClassification", "modifier", 1.3999999999999999, "L6" },
                    { new Guid("bdf1c2df-31b4-4068-8a57-382201013e76"), "highway", "modifier", 1.2, "residential" },
                    { new Guid("c4882398-f89e-4016-ab95-08a35fc46aa7"), "rushPrice", "modifier", 1.2, "16" },
                    { new Guid("d039f5b4-1b24-46a5-b1d5-5bf2d57fdd71"), "rushPrice", "modifier", 1.2, "17" },
                    { new Guid("d917c25f-a947-4c51-8f2f-88c80116fa5e"), "vehicleClassification", "modifier", 1.25, "O3" },
                    { new Guid("f7f365dc-8fd2-4395-a6fa-3aff345a1b89"), "vehicleClassification", "modifier", 1.2, "L2" },
                    { new Guid("fb92ae6d-2fb2-4f98-94cc-d9950cc6307c"), "vehicleClassification", "modifier", 1.3, "L4" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pricing");
        }
    }
}
