using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebBookStore.Migrations
{
    /// <inheritdoc />
    public partial class addBillModel1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BillId",
                table: "Bills",
                newName: "Id");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateDeliver",
                table: "Bills",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ShippingFee",
                table: "Bills",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateDeliver",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "ShippingFee",
                table: "Bills");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Bills",
                newName: "BillId");
        }
    }
}
