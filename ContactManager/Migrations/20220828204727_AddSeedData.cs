using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContactManager.Migrations
{
    public partial class AddSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Id", "Address", "Company", "Name", "Notes", "Phone" },
                values: new object[,]
                {
                    { new Guid("0f243a4b-207c-4eb6-b4d5-53d5b1fddfad"), "3555 Farnam Street, Omaha, NE", "Berkshire Hathaway", "Warren Buffett", "Deep pockets.", "(510)555-8164" },
                    { new Guid("360f55f3-b7b8-4e4e-9535-1772e1047c9b"), "1 Amphitheater Parkway, Mountain View, CA", "Google", "Scottie Scheffler", "Interested in ddPCR.", "(510)555-3282" },
                    { new Guid("520778df-8a95-436c-b7d6-619735d10918"), "13101 Harold Green Road, Austin, Texas", "Tesla", "Elon Musk", "Also deep pockets.", "(510)555-8912" }
                });

            migrationBuilder.InsertData(
                table: "Vendor",
                columns: new[] { "Id", "Address", "Company", "Name", "Phone" },
                values: new object[,]
                {
                    { new Guid("a0420642-1599-46a3-9c52-6b6215346972"), "111 A Street, Benicia, CA", "Radical Reagents", "George W. Salesman", "(510)555-1234" },
                    { new Guid("b32e3a41-6dca-4730-9449-55784dcd72dc"), "333 C Street, Benicia, CA", "ACME Acids", "Jane Doe", "(510)555-5439" },
                    { new Guid("cc0f7096-1635-47e1-8c29-74196c5bd6bd"), "222 B Street, Benicia, CA", "Berenstain Biology", "Stephanie Saleswoman", "(510)555-3289" }
                });

            migrationBuilder.InsertData(
                table: "VendorMasterList",
                columns: new[] { "Id", "CompanyName", "VendorCode" },
                values: new object[,]
                {
                    { new Guid("0ab5eb65-958b-4b7c-9e7f-8b3657f4426a"), "Flick’s Fluidics", "A003" },
                    { new Guid("13671055-baaa-420a-8341-c0e47a071bb6"), "Berenstain Biology", "A002" },
                    { new Guid("68242db4-6242-47be-94dd-b83ec163c0dc"), "Radical Reagents", "D004" },
                    { new Guid("8c8dccc4-5e41-4012-b390-3b661524763f"), "BBST Paper Products", "G065" },
                    { new Guid("f2794e3b-7972-483f-88fa-54b1865e0776"), "ACME Acids", "A001" }
                });

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData("VendorMasterList", "Id", "0ab5eb65-958b-4b7c-9e7f-8b3657f4426a");
            migrationBuilder.DeleteData("VendorMasterList", "Id", "13671055-baaa-420a-8341-c0e47a071bb6");
            migrationBuilder.DeleteData("VendorMasterList", "Id", "68242db4-6242-47be-94dd-b83ec163c0dc");
            migrationBuilder.DeleteData("VendorMasterList", "Id", "8c8dccc4-5e41-4012-b390-3b661524763f");
            migrationBuilder.DeleteData("VendorMasterList", "Id", "f2794e3b-7972-483f-88fa-54b1865e0776");

            migrationBuilder.DeleteData("Vendor", "Id", "a0420642-1599-46a3-9c52-6b6215346972");
            migrationBuilder.DeleteData("Vendor", "Id", "b32e3a41-6dca-4730-9449-55784dcd72dc");
            migrationBuilder.DeleteData("Vendor", "Id", "cc0f7096-1635-47e1-8c29-74196c5bd6bd");

            migrationBuilder.DeleteData("Customer", "Id", "0f243a4b-207c-4eb6-b4d5-53d5b1fddfad");
            migrationBuilder.DeleteData("Customer", "Id", "360f55f3-b7b8-4e4e-9535-1772e1047c9b");
            migrationBuilder.DeleteData("Customer", "Id", "520778df-8a95-436c-b7d6-619735d10918");
            
        }
    }
}
