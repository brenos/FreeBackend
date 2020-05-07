using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PersonalBudget.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "categorie",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(50)", nullable: false),
                    name = table.Column<string>(type: "varchar(45)", nullable: false),
                    userId = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categorie", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "transactionType",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(50)", nullable: false),
                    name = table.Column<string>(type: "varchar(45)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transactionType", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "transaction",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(50)", nullable: false),
                    description = table.Column<string>(type: "varchar(45)", nullable: true),
                    dtTransaction = table.Column<DateTime>(type: "datetime", nullable: false),
                    yearRef = table.Column<int>(nullable: false),
                    monthRef = table.Column<int>(nullable: false),
                    amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    userId = table.Column<string>(type: "varchar(50)", nullable: false),
                    categorieId = table.Column<string>(type: "varchar(50)", nullable: false),
                    typeId = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transaction", x => x.id);
                    table.ForeignKey(
                        name: "fk_transaction_categorie",
                        column: x => x.categorieId,
                        principalTable: "categorie",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_transaction_type",
                        column: x => x.typeId,
                        principalTable: "transactionType",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "release",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(50)", nullable: false),
                    yearRef = table.Column<int>(nullable: false),
                    monthRef = table.Column<int>(nullable: false),
                    amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    transactionId = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_release", x => x.id);
                    table.ForeignKey(
                        name: "fk_release_transaction",
                        column: x => x.transactionId,
                        principalTable: "transaction",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "id_UNIQUE",
                table: "categorie",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "userId_index",
                table: "categorie",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "ID_UNIQUE",
                table: "release",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_release_transaction_idx",
                table: "release",
                column: "transactionId");

            migrationBuilder.CreateIndex(
                name: "fields_index",
                table: "release",
                columns: new[] { "monthRef", "yearRef" });

            migrationBuilder.CreateIndex(
                name: "fk_transaction_categorie_idx",
                table: "transaction",
                column: "categorieId");

            migrationBuilder.CreateIndex(
                name: "ID_UNIQUE",
                table: "transaction",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_transaction_type_idx",
                table: "transaction",
                column: "typeId");

            migrationBuilder.CreateIndex(
                name: "fields_index",
                table: "transaction",
                columns: new[] { "monthRef", "yearRef", "userId" });

            migrationBuilder.CreateIndex(
                name: "id_UNIQUE",
                table: "transactionType",
                column: "id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "release");

            migrationBuilder.DropTable(
                name: "transaction");

            migrationBuilder.DropTable(
                name: "categorie");

            migrationBuilder.DropTable(
                name: "transactionType");
        }
    }
}
