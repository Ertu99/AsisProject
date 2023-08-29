using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AsisProject.Migrations
{
    /// <inheritdoc />
    public partial class InitialUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Firmalar",
                columns: table => new
                {
                    FirmaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirmaIsim = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Firmalar", x => x.FirmaId);
                });

            migrationBuilder.CreateTable(
                name: "IrsaliyeNumaralari",
                columns: table => new
                {
                    IrsaliyeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IrsaliyeNo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IrsaliyeNumaralari", x => x.IrsaliyeId);
                });

            migrationBuilder.CreateTable(
                name: "Urunler",
                columns: table => new
                {
                    UrunId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Baslangic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bitis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MalinCinsi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IrsaliyeNo = table.Column<int>(type: "int", nullable: false),
                    Tonaj = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Fiyat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    KdvOran = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MalHizmetTutari = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DigerVergiler = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OdenecekTutar = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FirmaIsim = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fatura = table.Column<bool>(type: "bit", nullable: false),
                    OdendiBilgisi = table.Column<bool>(type: "bit", nullable: false),
                    IrsaliyeId = table.Column<int>(type: "int", nullable: false),
                    FirmaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Urunler", x => x.UrunId);
                    table.ForeignKey(
                        name: "FK_Urunler_Firmalar_FirmaId",
                        column: x => x.FirmaId,
                        principalTable: "Firmalar",
                        principalColumn: "FirmaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Urunler_IrsaliyeNumaralari_IrsaliyeId",
                        column: x => x.IrsaliyeId,
                        principalTable: "IrsaliyeNumaralari",
                        principalColumn: "IrsaliyeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Urunler_FirmaId",
                table: "Urunler",
                column: "FirmaId");

            migrationBuilder.CreateIndex(
                name: "IX_Urunler_IrsaliyeId",
                table: "Urunler",
                column: "IrsaliyeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Urunler");

            migrationBuilder.DropTable(
                name: "Firmalar");

            migrationBuilder.DropTable(
                name: "IrsaliyeNumaralari");
        }
    }
}
