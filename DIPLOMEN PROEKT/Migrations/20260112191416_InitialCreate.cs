using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DIPLOMEN_PROEKT.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Brand = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    FormFactor = table.Column<string>(type: "TEXT", nullable: false),
                    SupportedFormFactors = table.Column<string>(type: "TEXT", nullable: false),
                    ImageUrl = table.Column<string>(type: "TEXT", nullable: false),
                    Url = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cases", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GPUs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    Brand = table.Column<string>(type: "TEXT", nullable: false),
                    VRAM = table.Column<int>(type: "INTEGER", nullable: false),
                    RecommendedPSU = table.Column<int>(type: "INTEGER", nullable: false),
                    ImageUrl = table.Column<string>(type: "TEXT", nullable: false),
                    Url = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GPUs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Motherboards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Brand = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    Socket = table.Column<string>(type: "TEXT", nullable: false),
                    FormFactor = table.Column<string>(type: "TEXT", nullable: false),
                    MemoryType = table.Column<string>(type: "TEXT", nullable: false),
                    ImageUrl = table.Column<string>(type: "TEXT", nullable: false),
                    Url = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Motherboards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Processors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    Brand = table.Column<string>(type: "TEXT", nullable: false),
                    Socket = table.Column<string>(type: "TEXT", nullable: false),
                    TDP = table.Column<int>(type: "INTEGER", nullable: false),
                    ImageUrl = table.Column<string>(type: "TEXT", nullable: false),
                    Url = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Processors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PSUs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Brand = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    Wattage = table.Column<int>(type: "INTEGER", nullable: false),
                    Rating = table.Column<string>(type: "TEXT", nullable: false),
                    ImageUrl = table.Column<string>(type: "TEXT", nullable: false),
                    Url = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PSUs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RAMs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Brand = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    Type = table.Column<string>(type: "TEXT", nullable: false),
                    SizeGB = table.Column<int>(type: "INTEGER", nullable: false),
                    ImageUrl = table.Column<string>(type: "TEXT", nullable: false),
                    Url = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RAMs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Storages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Brand = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    Type = table.Column<string>(type: "TEXT", nullable: false),
                    SizeGB = table.Column<int>(type: "INTEGER", nullable: false),
                    ImageUrl = table.Column<string>(type: "TEXT", nullable: false),
                    Url = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Storages", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Cases",
                columns: new[] { "Id", "Brand", "FormFactor", "ImageUrl", "Name", "Price", "SupportedFormFactors", "Url" },
                values: new object[,]
                {
                    { 1, "NZXT", "", "...", "NZXT H7 Flow (ATX)", 130m, "ATX,mATX", null },
                    { 2, "DeepCool", "", "...", "DeepCool CC560 V2 (ATX)", 65m, "ATX,mATX", null }
                });

            migrationBuilder.InsertData(
                table: "GPUs",
                columns: new[] { "Id", "Brand", "ImageUrl", "Name", "Price", "RecommendedPSU", "Url", "VRAM" },
                values: new object[,]
                {
                    { 1, "Nvidia", "https://m.media-amazon.com/images/I/81xU+7F7L7L._AC_SL1500_.jpg", "NVIDIA GeForce RTX 4090 24GB", 1950m, 1000, null, 0 },
                    { 2, "Nvidia", "https://m.media-amazon.com/images/I/71Xf+6pY90L._AC_SL1500_.jpg", "NVIDIA GeForce RTX 4080 SUPER 16GB", 1100m, 850, null, 0 },
                    { 3, "Nvidia", "https://m.media-amazon.com/images/I/71Xf+6pY90L._AC_SL1500_.jpg", "NVIDIA GeForce RTX 4080 16GB", 1050m, 850, null, 0 },
                    { 4, "Nvidia", "https://m.media-amazon.com/images/I/71XmjJ1N7RL._AC_SL1500_.jpg", "NVIDIA GeForce RTX 4070 Ti SUPER 16GB", 880m, 750, null, 0 },
                    { 5, "Nvidia", "https://m.media-amazon.com/images/I/71XmjJ1N7RL._AC_SL1500_.jpg", "NVIDIA GeForce RTX 4070 Ti 12GB", 800m, 750, null, 0 },
                    { 6, "Nvidia", "https://m.media-amazon.com/images/I/71XmjJ1N7RL._AC_SL1500_.jpg", "NVIDIA GeForce RTX 4070 SUPER 12GB", 660m, 700, null, 0 },
                    { 7, "Nvidia", "https://m.media-amazon.com/images/I/71XmjJ1N7RL._AC_SL1500_.jpg", "NVIDIA GeForce RTX 4070 12GB", 600m, 650, null, 0 },
                    { 8, "Nvidia", "https://m.media-amazon.com/images/I/716m2O2GkTL._AC_SL1500_.jpg", "NVIDIA GeForce RTX 4060 Ti 8GB", 410m, 550, null, 0 },
                    { 9, "Nvidia", "https://m.media-amazon.com/images/I/716m2O2GkTL._AC_SL1500_.jpg", "NVIDIA GeForce RTX 4060 8GB", 310m, 550, null, 0 },
                    { 10, "Nvidia", "https://m.media-amazon.com/images/I/81xU+7F7L7L._AC_SL1500_.jpg", "NVIDIA GeForce RTX 3090 Ti 24GB", 1200m, 850, null, 0 },
                    { 11, "Nvidia", "https://m.media-amazon.com/images/I/81xU+7F7L7L._AC_SL1500_.jpg", "NVIDIA GeForce RTX 3090 24GB", 1100m, 850, null, 0 },
                    { 12, "Nvidia", "...", "NVIDIA GeForce RTX 3080 Ti 12GB", 900m, 750, null, 0 },
                    { 13, "Nvidia", "...", "NVIDIA GeForce RTX 3080 10GB", 700m, 750, null, 0 },
                    { 14, "Nvidia", "...", "NVIDIA GeForce RTX 3070 Ti 8GB", 580m, 750, null, 0 },
                    { 15, "Nvidia", "...", "NVIDIA GeForce RTX 3070 8GB", 500m, 650, null, 0 },
                    { 16, "Nvidia", "...", "NVIDIA GeForce RTX 3060 Ti 8GB", 380m, 600, null, 0 },
                    { 17, "Nvidia", "...", "NVIDIA GeForce RTX 3060 12GB", 290m, 550, null, 0 },
                    { 18, "Nvidia", "...", "NVIDIA GeForce RTX 3050 8GB", 230m, 550, null, 0 },
                    { 19, "Nvidia", "...", "NVIDIA GeForce RTX 2080 Ti 11GB", 500m, 650, null, 0 },
                    { 20, "Nvidia", "...", "NVIDIA GeForce RTX 2080 Super", 450m, 650, null, 0 },
                    { 21, "Nvidia", "...", "NVIDIA GeForce RTX 2070 Super", 350m, 650, null, 0 },
                    { 22, "Nvidia", "...", "NVIDIA GeForce RTX 2060 Super", 280m, 550, null, 0 },
                    { 23, "Nvidia", "...", "NVIDIA GeForce RTX 2060 6GB", 220m, 500, null, 0 },
                    { 24, "Nvidia", "...", "NVIDIA GeForce GTX 1660 SUPER", 210m, 450, null, 0 },
                    { 25, "Nvidia", "...", "NVIDIA GeForce GTX 1650 SUPER", 160m, 350, null, 0 },
                    { 26, "Nvidia", "...", "NVIDIA GeForce GTX 1630", 130m, 300, null, 0 },
                    { 27, "Nvidia", "...", "NVIDIA GeForce GTX 1080 Ti 11GB", 300m, 600, null, 0 },
                    { 28, "Nvidia", "...", "NVIDIA GeForce GTX 1080 8GB", 250m, 500, null, 0 },
                    { 29, "Nvidia", "...", "NVIDIA GeForce GTX 1070 Ti", 220m, 500, null, 0 },
                    { 30, "Nvidia", "...", "NVIDIA GeForce GTX 1060 6GB", 150m, 400, null, 0 },
                    { 31, "Nvidia", "...", "NVIDIA GeForce GTX 1050 Ti 4GB", 110m, 300, null, 0 },
                    { 32, "Nvidia", "...", "NVIDIA GeForce GTX 1030 2GB", 80m, 300, null, 0 },
                    { 33, "Nvidia", "...", "NVIDIA GeForce GTX 980 Ti", 120m, 600, null, 0 },
                    { 34, "Nvidia", "...", "NVIDIA GeForce GTX 970", 90m, 500, null, 0 },
                    { 35, "Nvidia", "...", "NVIDIA GeForce GTX 750 Ti", 60m, 300, null, 0 },
                    { 36, "Nvidia", "...", "NVIDIA GeForce GT 730 2GB", 55m, 300, null, 0 },
                    { 37, "Nvidia", "...", "NVIDIA GeForce GT 710 2GB", 45m, 300, null, 0 },
                    { 38, "AMD", "https://m.media-amazon.com/images/I/81sh97S0tWL._AC_SL1500_.jpg", "AMD Radeon RX 7900 XTX 24GB", 1050m, 850, null, 0 },
                    { 39, "AMD", "...", "AMD Radeon RX 7900 XT 20GB", 840m, 750, null, 0 },
                    { 40, "AMD", "...", "AMD Radeon RX 7800 XT 16GB", 540m, 750, null, 0 },
                    { 41, "AMD", "...", "AMD Radeon RX 7700 XT 12GB", 460m, 700, null, 0 },
                    { 42, "AMD", "...", "AMD Radeon RX 7600 8GB", 280m, 550, null, 0 },
                    { 43, "AMD", "...", "AMD Radeon RX 6950 XT", 690m, 850, null, 0 },
                    { 44, "AMD", "...", "AMD Radeon RX 6800 XT", 500m, 750, null, 0 },
                    { 45, "AMD", "...", "AMD Radeon RX 6700 XT", 350m, 650, null, 0 },
                    { 46, "AMD", "...", "AMD Radeon RX 6600 XT", 260m, 500, null, 0 },
                    { 47, "AMD", "...", "AMD Radeon RX 6600", 220m, 450, null, 0 },
                    { 48, "AMD", "...", "AMD Radeon RX 5700 XT", 200m, 600, null, 0 },
                    { 49, "AMD", "...", "AMD Radeon RX 590 8GB", 130m, 550, null, 0 },
                    { 50, "AMD", "...", "AMD Radeon RX 580 8GB", 110m, 500, null, 0 },
                    { 51, "AMD", "...", "AMD Radeon RX 570 4GB", 90m, 450, null, 0 },
                    { 52, "AMD", "...", "AMD Radeon RX 550 4GB", 70m, 350, null, 0 },
                    { 53, "AMD", "...", "AMD Radeon Vega 64", 250m, 750, null, 0 },
                    { 54, "AMD", "...", "AMD Radeon HD 7770 1GB", 40m, 450, null, 0 }
                });

            migrationBuilder.InsertData(
                table: "Motherboards",
                columns: new[] { "Id", "Brand", "FormFactor", "ImageUrl", "MemoryType", "Name", "Price", "Socket", "Url" },
                values: new object[,]
                {
                    { 1, "Intel", "ATX", "https://m.media-amazon.com/images/I/81O58vLIDeL._AC_SL1500_.jpg", "DDR5", "ASUS ROG Strix Z790-E Gaming WiFi", 520m, "LGA1700", null },
                    { 2, "Intel", "ATX", "...", "DDR5", "GIGABYTE Z790 AORUS Elite AX", 280m, "LGA1700", null },
                    { 3, "Intel", "ATX", "...", "DDR5", "MSI MAG Z690 Tomahawk WiFi", 250m, "LGA1700", null },
                    { 4, "Intel", "mATX", "...", "DDR5", "ASUS TUF Gaming B760M-Plus WiFi", 180m, "LGA1700", null },
                    { 5, "Intel", "mATX", "...", "DDR4", "MSI PRO B760M-A WiFi", 160m, "LGA1700", null },
                    { 6, "Intel", "mATX", "...", "DDR4", "GIGABYTE B660M DS3H", 120m, "LGA1700", null },
                    { 7, "Intel", "mATX", "...", "DDR4", "ASUS PRIME H610M-K D4", 95m, "LGA1700", null },
                    { 8, "Intel", "ATX", "...", "DDR4", "ASUS Z590 Gaming WiFi", 170m, "LGA1200", null },
                    { 9, "Intel", "ATX", "...", "DDR4", "MSI Z390-A PRO", 130m, "LGA1151", null },
                    { 10, "AMD", "ATX", "...", "DDR5", "ASUS ROG Crosshair X670E Hero", 650m, "AM5", null },
                    { 11, "AMD", "ATX", "...", "DDR5", "GIGABYTE X670E AORUS Master", 480m, "AM5", null },
                    { 12, "AMD", "ATX", "...", "DDR5", "MSI MPG B650 Carbon WiFi", 300m, "AM5", null },
                    { 13, "AMD", "ATX", "...", "DDR5", "ASRock B650 Steel Legend", 240m, "AM5", null },
                    { 14, "AMD", "mATX", "...", "DDR5", "ASUS PRIME A620M-A", 110m, "AM5", null },
                    { 15, "AMD", "ATX", "...", "DDR4", "ASUS ROG Strix X570-E Gaming", 300m, "AM4", null },
                    { 16, "AMD", "ATX", "...", "DDR4", "MSI MAG B550 Tomahawk", 160m, "AM4", null },
                    { 17, "AMD", "ATX", "...", "DDR4", "ASRock B550 Phantom Gaming", 120m, "AM4", null },
                    { 18, "AMD", "mATX", "...", "DDR4", "Gigabyte B450M DS3H", 80m, "AM4", null },
                    { 19, "AMD", "mATX", "...", "DDR4", "ASUS PRIME A520M-K", 65m, "AM4", null }
                });

            migrationBuilder.InsertData(
                table: "PSUs",
                columns: new[] { "Id", "Brand", "ImageUrl", "Name", "Price", "Rating", "Url", "Wattage" },
                values: new object[,]
                {
                    { 1, "Corsair", "https://m.media-amazon.com/images/I/71Xf+6pY90L._AC_SL1500_.jpg", "Corsair RM1000x 1000W 80+ Gold", 190m, "Gold", null, 1000 },
                    { 2, "Corsair", "...", "Corsair RM850x 850W 80+ Gold", 150m, "Gold", null, 850 },
                    { 3, "Corsair", "...", "Corsair RM750x 750W 80+ Gold", 130m, "Gold", null, 750 },
                    { 4, "Corsair", "...", "Corsair CV550 550W 80+ Bronze", 60m, "Bronze", null, 550 },
                    { 5, "Seasonic", "...", "Seasonic Focus GX-850 850W Gold", 150m, "Gold", null, 850 },
                    { 6, "Seasonic", "...", "Seasonic Focus GX-650 650W Gold", 120m, "Gold", null, 650 },
                    { 7, "be quiet!", "...", "be quiet! Pure Power 11 700W Gold", 110m, "Gold", null, 700 },
                    { 8, "EVGA", "...", "EVGA SuperNOVA 850 G5 850W Gold", 145m, "Gold", null, 850 },
                    { 9, "EVGA", "...", "EVGA 600 BR 600W 80+ Bronze", 65m, "Bronze", null, 600 },
                    { 10, "Gigabyte", "...", "Gigabyte P650B 650W 80+ Bronze", 70m, "Bronze", null, 650 },
                    { 11, "Thermaltake", "...", "Thermaltake Smart RGB 600W", 55m, "Bronze", null, 600 }
                });

            migrationBuilder.InsertData(
                table: "Processors",
                columns: new[] { "Id", "Brand", "ImageUrl", "Name", "Price", "Socket", "TDP", "Url" },
                values: new object[,]
                {
                    { 1, "AMD", "https://m.media-amazon.com/images/I/71B9s66H7JL._AC_SL1500_.jpg", "AMD Ryzen 9 7950X3D (4.2GHz) TRAY", 680m, "AM5", 120, null },
                    { 2, "AMD", "https://m.media-amazon.com/images/I/71B9s66H7JL._AC_SL1500_.jpg", "AMD Ryzen 9 7950X (4.5GHz) TRAY", 570m, "AM5", 170, null },
                    { 3, "AMD", "https://m.media-amazon.com/images/I/71B9s66H7JL._AC_SL1500_.jpg", "AMD Ryzen 9 7900X3D (4.4GHz) TRAY", 510m, "AM5", 120, null },
                    { 4, "AMD", "https://m.media-amazon.com/images/I/71B9s66H7JL._AC_SL1500_.jpg", "AMD Ryzen 9 7900X (4.7GHz) TRAY", 450m, "AM5", 170, null },
                    { 5, "AMD", "https://m.media-amazon.com/images/I/71B9s66H7JL._AC_SL1500_.jpg", "AMD Ryzen 9 7900 (3.7GHz) TRAY", 420m, "AM5", 65, null },
                    { 6, "AMD", "https://m.media-amazon.com/images/I/51p6K6N5p1L._AC_SL1000_.jpg", "AMD Ryzen 7 7800X3D (4.2GHz) TRAY", 430m, "AM5", 120, null },
                    { 7, "AMD", "https://m.media-amazon.com/images/I/51p6K6N5p1L._AC_SL1000_.jpg", "AMD Ryzen 7 7700X (4.5GHz) TRAY", 350m, "AM5", 105, null },
                    { 8, "AMD", "https://m.media-amazon.com/images/I/51p6K6N5p1L._AC_SL1000_.jpg", "AMD Ryzen 7 7700 (3.8GHz) TRAY", 320m, "AM5", 65, null },
                    { 9, "AMD", "https://m.media-amazon.com/images/I/61SOfmByXjL._AC_SL1500_.jpg", "AMD Ryzen 5 7600X (4.7GHz) TRAY", 240m, "AM5", 105, null },
                    { 10, "AMD", "https://m.media-amazon.com/images/I/61SOfmByXjL._AC_SL1500_.jpg", "AMD Ryzen 5 7600 (3.8GHz) TRAY", 215m, "AM5", 65, null },
                    { 11, "AMD", "https://m.media-amazon.com/images/I/61SOfmByXjL._AC_SL1500_.jpg", "AMD Ryzen 5 5600X (3.7GHz) TRAY", 155m, "AM4", 65, null },
                    { 12, "AMD", "https://m.media-amazon.com/images/I/61SOfmByXjL._AC_SL1500_.jpg", "AMD Ryzen 5 5600 (3.5GHz) TRAY", 135m, "AM4", 65, null },
                    { 13, "AMD", "https://m.media-amazon.com/images/I/61SOfmByXjL._AC_SL1500_.jpg", "AMD Ryzen 3 4100 (3.8GHz) TRAY", 75m, "AM4", 65, null },
                    { 14, "AMD", "https://m.media-amazon.com/images/I/61SOfmByXjL._AC_SL1500_.jpg", "AMD Ryzen 3 3200G (3.6GHz) TRAY", 85m, "AM4", 65, null },
                    { 15, "Intel", "https://m.media-amazon.com/images/I/61S7Ej68L+L._AC_SL1000_.jpg", "Intel Core i9-14900K (3.2GHz) TRAY", 630m, "LGA1700", 125, null },
                    { 16, "Intel", "https://m.media-amazon.com/images/I/61S7Ej68L+L._AC_SL1000_.jpg", "Intel Core i9-14900KF (3.2GHz) TRAY", 605m, "LGA1700", 125, null },
                    { 17, "Intel", "https://m.media-amazon.com/images/I/61S7Ej68L+L._AC_SL1000_.jpg", "Intel Core i9-13900K (3.0GHz) TRAY", 575m, "LGA1700", 125, null },
                    { 18, "Intel", "https://m.media-amazon.com/images/I/61S7Ej68L+L._AC_SL1000_.jpg", "Intel Core i9-13900KF (3.0GHz) TRAY", 550m, "LGA1700", 125, null },
                    { 19, "Intel", "https://m.media-amazon.com/images/I/61fS0Xp8YpL._AC_SL1000_.jpg", "Intel Core i7-14700K (3.4GHz) TRAY", 440m, "LGA1700", 125, null },
                    { 20, "Intel", "https://m.media-amazon.com/images/I/61fS0Xp8YpL._AC_SL1000_.jpg", "Intel Core i7-14700KF (3.4GHz) TRAY", 415m, "LGA1700", 125, null },
                    { 21, "Intel", "https://m.media-amazon.com/images/I/61fS0Xp8YpL._AC_SL1000_.jpg", "Intel Core i7-13700K (3.4GHz) TRAY", 390m, "LGA1700", 125, null },
                    { 22, "Intel", "https://m.media-amazon.com/images/I/61fS0Xp8YpL._AC_SL1000_.jpg", "Intel Core i7-13700KF (3.4GHz) TRAY", 370m, "LGA1700", 125, null },
                    { 23, "Intel", "https://m.media-amazon.com/images/I/616v8WvV94L._AC_SL1000_.jpg", "Intel Core i5-14600K (3.5GHz) TRAY", 340m, "LGA1700", 125, null },
                    { 24, "Intel", "https://m.media-amazon.com/images/I/616v8WvV94L._AC_SL1000_.jpg", "Intel Core i5-14600KF (3.5GHz) TRAY", 315m, "LGA1700", 125, null },
                    { 25, "Intel", "https://m.media-amazon.com/images/I/616v8WvV94L._AC_SL1000_.jpg", "Intel Core i5-13600K (3.5GHz) TRAY", 310m, "LGA1700", 125, null },
                    { 26, "Intel", "https://m.media-amazon.com/images/I/616v8WvV94L._AC_SL1000_.jpg", "Intel Core i5-13600KF (3.5GHz) TRAY", 290m, "LGA1700", 125, null },
                    { 27, "Intel", "https://m.media-amazon.com/images/I/616v8WvV94L._AC_SL1000_.jpg", "Intel Core i5-12600K (3.7GHz) TRAY", 230m, "LGA1700", 125, null },
                    { 28, "Intel", "https://m.media-amazon.com/images/I/51p6K6N5p1L._AC_SL1000_.jpg", "Intel Core i3-14100 (3.5GHz) TRAY", 160m, "LGA1700", 60, null },
                    { 29, "Intel", "https://m.media-amazon.com/images/I/51p6K6N5p1L._AC_SL1000_.jpg", "Intel Core i3-14100F (3.5GHz) TRAY", 130m, "LGA1700", 60, null },
                    { 30, "Intel", "https://m.media-amazon.com/images/I/51p6K6N5p1L._AC_SL1000_.jpg", "Intel Core i3-13100 (3.4GHz) TRAY", 135m, "LGA1700", 60, null },
                    { 31, "Intel", "https://m.media-amazon.com/images/I/51p6K6N5p1L._AC_SL1000_.jpg", "Intel Core i3-13100F (3.4GHz) TRAY", 110m, "LGA1700", 60, null },
                    { 32, "Intel", "https://m.media-amazon.com/images/I/51p6K6N5p1L._AC_SL1000_.jpg", "Intel Core i3-12100 (3.3GHz) TRAY", 110m, "LGA1700", 60, null },
                    { 33, "Intel", "https://m.media-amazon.com/images/I/51p6K6N5p1L._AC_SL1000_.jpg", "Intel Core i3-12100F (3.3GHz) TRAY", 95m, "LGA1700", 60, null }
                });

            migrationBuilder.InsertData(
                table: "RAMs",
                columns: new[] { "Id", "Brand", "ImageUrl", "Name", "Price", "SizeGB", "Type", "Url" },
                values: new object[,]
                {
                    { 1, "Kingston", "...", "Kingston FURY Beast 32GB (2x16) DDR5 6000MHz", 130m, 32, "DDR5", null },
                    { 2, "Kingston", "...", "Kingston FURY Beast 16GB (2x8) DDR5 5200MHz", 80m, 16, "DDR5", null },
                    { 3, "Corsair", "...", "Corsair Vengeance LPX 16GB (2x8) DDR4 3200MHz", 45m, 16, "DDR4", null }
                });

            migrationBuilder.InsertData(
                table: "Storages",
                columns: new[] { "Id", "Brand", "ImageUrl", "Name", "Price", "SizeGB", "Type", "Url" },
                values: new object[,]
                {
                    { 1, "Samsung", "...", "Samsung 990 PRO 2TB NVMe", 180m, 2000, "NVMe", null },
                    { 2, "Kingston", "...", "Kingston NV2 1TB NVMe", 65m, 1000, "NVMe", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cases");

            migrationBuilder.DropTable(
                name: "GPUs");

            migrationBuilder.DropTable(
                name: "Motherboards");

            migrationBuilder.DropTable(
                name: "Processors");

            migrationBuilder.DropTable(
                name: "PSUs");

            migrationBuilder.DropTable(
                name: "RAMs");

            migrationBuilder.DropTable(
                name: "Storages");
        }
    }
}
