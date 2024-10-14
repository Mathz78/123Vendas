using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UmDoisTresVendas.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDenormalizedFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BranchName",
                table: "Sales",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CustomerName",
                table: "Sales",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                table: "Sales",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Sales",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "SaleItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BranchName",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "CustomerName",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "SaleItems");
        }
    }
}
