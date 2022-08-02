﻿using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NameBandit.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NameCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NameCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NameSyncLogs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(nullable: false),
                    Log = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NameSyncLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NameCombinations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CategoryId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NameCombinations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NameCombinations_NameCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "NameCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Names",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Text = table.Column<string>(nullable: true),
                    Male = table.Column<bool>(nullable: false),
                    Female = table.Column<bool>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    Prefix = table.Column<bool>(nullable: false),
                    Suffix = table.Column<bool>(nullable: false),
                    Vibration = table.Column<int>(nullable: false),
                    NameComboId = table.Column<int>(nullable: true),
                    CategoryId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Names", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Names_NameCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "NameCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Names_NameCombinations_NameComboId",
                        column: x => x.NameComboId,
                        principalTable: "NameCombinations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NameCombinations_CategoryId",
                table: "NameCombinations",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Names_CategoryId",
                table: "Names",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Names_NameComboId",
                table: "Names",
                column: "NameComboId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Names");

            migrationBuilder.DropTable(
                name: "NameSyncLogs");

            migrationBuilder.DropTable(
                name: "NameCombinations");

            migrationBuilder.DropTable(
                name: "NameCategories");
        }
    }
}