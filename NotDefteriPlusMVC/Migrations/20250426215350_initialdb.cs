using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NotDefteriPlusMVC.Migrations
{
    /// <inheritdoc />
    public partial class initialdb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Ad = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Soyad = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dersler",
                columns: table => new
                {
                    DersId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DersAdi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dersler", x => x.DersId);
                });

            migrationBuilder.CreateTable(
                name: "Fakulteler",
                columns: table => new
                {
                    FakulteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FakulteAdi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fakulteler", x => x.FakulteId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bolumler",
                columns: table => new
                {
                    BolumId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BolumAdi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FakulteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bolumler", x => x.BolumId);
                    table.ForeignKey(
                        name: "FK_Bolumler_Fakulteler_FakulteId",
                        column: x => x.FakulteId,
                        principalTable: "Fakulteler",
                        principalColumn: "FakulteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BolumDers",
                columns: table => new
                {
                    BolumId = table.Column<int>(type: "int", nullable: false),
                    DersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BolumDers", x => new { x.BolumId, x.DersId });
                    table.ForeignKey(
                        name: "FK_BolumDers_Bolumler_BolumId",
                        column: x => x.BolumId,
                        principalTable: "Bolumler",
                        principalColumn: "BolumId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BolumDers_Dersler_DersId",
                        column: x => x.DersId,
                        principalTable: "Dersler",
                        principalColumn: "DersId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KullaniciBolum",
                columns: table => new
                {
                    KullaniciId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BolumId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KullaniciBolum", x => new { x.BolumId, x.KullaniciId });
                    table.ForeignKey(
                        name: "FK_KullaniciBolum_AspNetUsers_KullaniciId",
                        column: x => x.KullaniciId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KullaniciBolum_Bolumler_BolumId",
                        column: x => x.BolumId,
                        principalTable: "Bolumler",
                        principalColumn: "BolumId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notlar",
                columns: table => new
                {
                    NotId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Baslik = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Icerik = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DosyaYolu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OlusturulmaTarihi = table.Column<DateTime>(type: "date", nullable: false),
                    GuncellenmeTarihi = table.Column<DateTime>(type: "date", nullable: false),
                    KullaniciId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BolumId = table.Column<int>(type: "int", nullable: false),
                    DersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notlar", x => x.NotId);
                    table.ForeignKey(
                        name: "FK_Notlar_AspNetUsers_KullaniciId",
                        column: x => x.KullaniciId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notlar_Bolumler_BolumId",
                        column: x => x.BolumId,
                        principalTable: "Bolumler",
                        principalColumn: "BolumId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notlar_Dersler_DersId",
                        column: x => x.DersId,
                        principalTable: "Dersler",
                        principalColumn: "DersId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Dersler",
                columns: new[] { "DersId", "DersAdi" },
                values: new object[,]
                {
                    { 1, "Veri Yapıları" },
                    { 2, "Algoritmalar" },
                    { 3, "İstatistik" },
                    { 4, "Mikroekonomi" },
                    { 5, "Ceza Hukuku" },
                    { 6, "Medeni Hukuk" },
                    { 7, "Psikolojiye Giriş" },
                    { 8, "Sosyolojiye Giriş" },
                    { 9, "Biyolojiye Giriş" },
                    { 10, "Kimyaya Giriş" },
                    { 11, "Fizik" },
                    { 12, "Matematik" }
                });

            migrationBuilder.InsertData(
                table: "Fakulteler",
                columns: new[] { "FakulteId", "FakulteAdi" },
                values: new object[,]
                {
                    { 1, "Mühendislik Fakültesi" },
                    { 2, "İktisadi ve İdari Bilimler Fakültesi" },
                    { 3, "Edebiyat Fakültesi" },
                    { 4, "Teknoloji Fakültesi" },
                    { 5, "Güzel Sanatlar Fakültesi" }
                });

            migrationBuilder.InsertData(
                table: "Bolumler",
                columns: new[] { "BolumId", "BolumAdi", "FakulteId" },
                values: new object[,]
                {
                    { 1, "Bilgisayar Mühendisliği", 1 },
                    { 2, "Endüstri Mühendisliği", 1 },
                    { 3, "Elektrik Elektronik Mühendisliği", 1 },
                    { 4, "Makine Mühendisliği", 1 },
                    { 5, "İşletme", 2 },
                    { 6, "İktisat", 2 },
                    { 7, "Hukuk", 3 },
                    { 8, "Psikoloji", 4 },
                    { 9, "Sosyoloji", 4 },
                    { 10, "Biyoloji", 5 },
                    { 11, "Kimya", 5 }
                });

            migrationBuilder.InsertData(
                table: "BolumDers",
                columns: new[] { "BolumId", "DersId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 1, 11 },
                    { 1, 12 },
                    { 2, 3 },
                    { 2, 4 },
                    { 2, 11 },
                    { 2, 12 },
                    { 3, 5 },
                    { 3, 6 },
                    { 3, 11 },
                    { 3, 12 },
                    { 4, 7 },
                    { 4, 8 },
                    { 4, 11 },
                    { 4, 12 },
                    { 5, 9 },
                    { 5, 10 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BolumDers_DersId",
                table: "BolumDers",
                column: "DersId");

            migrationBuilder.CreateIndex(
                name: "IX_Bolumler_BolumAdi",
                table: "Bolumler",
                column: "BolumAdi",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bolumler_FakulteId",
                table: "Bolumler",
                column: "FakulteId");

            migrationBuilder.CreateIndex(
                name: "IX_KullaniciBolum_KullaniciId",
                table: "KullaniciBolum",
                column: "KullaniciId");

            migrationBuilder.CreateIndex(
                name: "IX_Notlar_BolumId",
                table: "Notlar",
                column: "BolumId");

            migrationBuilder.CreateIndex(
                name: "IX_Notlar_DersId",
                table: "Notlar",
                column: "DersId");

            migrationBuilder.CreateIndex(
                name: "IX_Notlar_KullaniciId",
                table: "Notlar",
                column: "KullaniciId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "BolumDers");

            migrationBuilder.DropTable(
                name: "KullaniciBolum");

            migrationBuilder.DropTable(
                name: "Notlar");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Bolumler");

            migrationBuilder.DropTable(
                name: "Dersler");

            migrationBuilder.DropTable(
                name: "Fakulteler");
        }
    }
}
