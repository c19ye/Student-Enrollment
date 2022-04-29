using Microsoft.EntityFrameworkCore.Migrations;

namespace Okul.Migrations
{
    public partial class Yapilandir : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "kullanicilar",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Kadi = table.Column<string>(nullable: false),
                    Sifre = table.Column<string>(nullable: false),
                    Turu = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kullanicilar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "siniflar",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Adi = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_siniflar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ogrenciler",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Adi = table.Column<string>(nullable: true),
                    Soyadi = table.Column<string>(nullable: true),
                    Okulno = table.Column<int>(nullable: false),
                    sınıfid = table.Column<int>(nullable: false),
                    Resim = table.Column<string>(nullable: true),
                    Aciklama = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ogrenciler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ogrenciler_siniflar_sınıfid",
                        column: x => x.sınıfid,
                        principalTable: "siniflar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ogrenciler_sınıfid",
                table: "ogrenciler",
                column: "sınıfid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "kullanicilar");

            migrationBuilder.DropTable(
                name: "ogrenciler");

            migrationBuilder.DropTable(
                name: "siniflar");
        }
    }
}
