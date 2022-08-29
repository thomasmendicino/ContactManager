using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContactManager.SqliteMigrations
{
    public partial class AddSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Id", "Address", "Company", "Name", "Notes", "Phone" },
                values: new object[] { new Guid("62e66cb8-a6b9-4a97-92cf-8569fc7493a0"), "3555 Farnam Street, Omaha, NE", "Berkshire Hathaway", "Warren Buffett", "Deep pockets.", "(510)555-8164" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Id", "Address", "Company", "Name", "Notes", "Phone" },
                values: new object[] { new Guid("a8886244-3763-4a53-bcb1-12eb3220db51"), "1 Amphitheater Parkway, Mountain View, CA", "Google", "Scottie Scheffler", "Interested in ddPCR.", "(510)555-3282" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Id", "Address", "Company", "Name", "Notes", "Phone" },
                values: new object[] { new Guid("a8ed1a80-943c-4745-aa10-3d2b8561f2b9"), "13101 Harold Green Road, Austin, Texas", "Tesla", "Elon Musk", "Also deep pockets.", "(510)555-8912" });

            migrationBuilder.InsertData(
                table: "Vendor",
                columns: new[] { "Id", "Address", "Company", "Name", "Phone" },
                values: new object[] { new Guid("3c07c8ff-ebb3-454d-b86d-d6b7a98b599c"), "333 C Street, Benicia, CA", "ACME Acids", "Jane Doe", "(510)555-5439" });

            migrationBuilder.InsertData(
                table: "Vendor",
                columns: new[] { "Id", "Address", "Company", "Name", "Phone" },
                values: new object[] { new Guid("a8864005-ce08-4983-b3c4-4b16d132a5ae"), "222 B Street, Benicia, CA", "Berenstain Biology", "Stephanie Saleswoman", "(510)555-3289" });

            migrationBuilder.InsertData(
                table: "Vendor",
                columns: new[] { "Id", "Address", "Company", "Name", "Phone" },
                values: new object[] { new Guid("ff1926fb-30f1-4d44-aea9-fa82d1e120cf"), "111 A Street, Benicia, CA", "Radical Reagents", "George W. Salesman", "(510)555-1234" });

            migrationBuilder.InsertData(
                table: "VendorMasterList",
                columns: new[] { "Id", "CompanyName", "VendorCode" },
                values: new object[] { new Guid("67e0b7da-0c37-47ea-bad4-d7654ca90205"), "Radical Reagents", "D004" });

            migrationBuilder.InsertData(
                table: "VendorMasterList",
                columns: new[] { "Id", "CompanyName", "VendorCode" },
                values: new object[] { new Guid("7d64d5e9-c21a-4170-8509-c86c16390df9"), "BBST Paper Products", "G065" });

            migrationBuilder.InsertData(
                table: "VendorMasterList",
                columns: new[] { "Id", "CompanyName", "VendorCode" },
                values: new object[] { new Guid("a11c0b31-5833-4df5-b57a-dbad990934d9"), "Flick’s Fluidics", "A003" });

            migrationBuilder.InsertData(
                table: "VendorMasterList",
                columns: new[] { "Id", "CompanyName", "VendorCode" },
                values: new object[] { new Guid("d56bdab0-bed0-4983-a80f-390a641a8376"), "ACME Acids", "A001" });

            migrationBuilder.InsertData(
                table: "VendorMasterList",
                columns: new[] { "Id", "CompanyName", "VendorCode" },
                values: new object[] { new Guid("eb7619b2-11b4-4780-9a3f-dda3ab10f1ed"), "Berenstain Biology", "A002" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customer",
                keyColumn: "Id",
                keyValue: new Guid("62e66cb8-a6b9-4a97-92cf-8569fc7493a0"));

            migrationBuilder.DeleteData(
                table: "Customer",
                keyColumn: "Id",
                keyValue: new Guid("a8886244-3763-4a53-bcb1-12eb3220db51"));

            migrationBuilder.DeleteData(
                table: "Customer",
                keyColumn: "Id",
                keyValue: new Guid("a8ed1a80-943c-4745-aa10-3d2b8561f2b9"));

            migrationBuilder.DeleteData(
                table: "Vendor",
                keyColumn: "Id",
                keyValue: new Guid("3c07c8ff-ebb3-454d-b86d-d6b7a98b599c"));

            migrationBuilder.DeleteData(
                table: "Vendor",
                keyColumn: "Id",
                keyValue: new Guid("a8864005-ce08-4983-b3c4-4b16d132a5ae"));

            migrationBuilder.DeleteData(
                table: "Vendor",
                keyColumn: "Id",
                keyValue: new Guid("ff1926fb-30f1-4d44-aea9-fa82d1e120cf"));

            migrationBuilder.DeleteData(
                table: "VendorMasterList",
                keyColumn: "Id",
                keyValue: new Guid("67e0b7da-0c37-47ea-bad4-d7654ca90205"));

            migrationBuilder.DeleteData(
                table: "VendorMasterList",
                keyColumn: "Id",
                keyValue: new Guid("7d64d5e9-c21a-4170-8509-c86c16390df9"));

            migrationBuilder.DeleteData(
                table: "VendorMasterList",
                keyColumn: "Id",
                keyValue: new Guid("a11c0b31-5833-4df5-b57a-dbad990934d9"));

            migrationBuilder.DeleteData(
                table: "VendorMasterList",
                keyColumn: "Id",
                keyValue: new Guid("d56bdab0-bed0-4983-a80f-390a641a8376"));

            migrationBuilder.DeleteData(
                table: "VendorMasterList",
                keyColumn: "Id",
                keyValue: new Guid("eb7619b2-11b4-4780-9a3f-dda3ab10f1ed"));
        }
    }
}
