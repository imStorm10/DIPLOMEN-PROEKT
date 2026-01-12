using Microsoft.EntityFrameworkCore;
using PCConfigurator.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace PCConfigurator.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Processor> Processors { get; set; }
        public DbSet<GPU> GPUs { get; set; }
        public DbSet<Motherboard> Motherboards { get; set; }
        public DbSet<RAM> RAMs { get; set; }
        public DbSet<Storage> Storages { get; set; }
        public DbSet<PSU> PSUs { get; set; }
        public DbSet<Case> Cases { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ===================== 1. PROCESSORS (SEED DATA) =====================
            modelBuilder.Entity<Processor>().HasData(
                // AMD Ryzen 9 (AM5)
                new Processor { Id = 1, Name = "AMD Ryzen 9 7950X3D (4.2GHz) TRAY", Price = 680, Brand = "AMD", Socket = "AM5", TDP = 120, ImageUrl = "https://m.media-amazon.com/images/I/71B9s66H7JL._AC_SL1500_.jpg" },
                new Processor { Id = 2, Name = "AMD Ryzen 9 7950X (4.5GHz) TRAY", Price = 570, Brand = "AMD", Socket = "AM5", TDP = 170, ImageUrl = "https://m.media-amazon.com/images/I/71B9s66H7JL._AC_SL1500_.jpg" },
                new Processor { Id = 3, Name = "AMD Ryzen 9 7900X3D (4.4GHz) TRAY", Price = 510, Brand = "AMD", Socket = "AM5", TDP = 120, ImageUrl = "https://m.media-amazon.com/images/I/71B9s66H7JL._AC_SL1500_.jpg" },
                new Processor { Id = 4, Name = "AMD Ryzen 9 7900X (4.7GHz) TRAY", Price = 450, Brand = "AMD", Socket = "AM5", TDP = 170, ImageUrl = "https://m.media-amazon.com/images/I/71B9s66H7JL._AC_SL1500_.jpg" },
                new Processor { Id = 5, Name = "AMD Ryzen 9 7900 (3.7GHz) TRAY", Price = 420, Brand = "AMD", Socket = "AM5", TDP = 65, ImageUrl = "https://m.media-amazon.com/images/I/71B9s66H7JL._AC_SL1500_.jpg" },

                // AMD Ryzen 7
                new Processor { Id = 6, Name = "AMD Ryzen 7 7800X3D (4.2GHz) TRAY", Price = 430, Brand = "AMD", Socket = "AM5", TDP = 120, ImageUrl = "https://m.media-amazon.com/images/I/51p6K6N5p1L._AC_SL1000_.jpg" },
                new Processor { Id = 7, Name = "AMD Ryzen 7 7700X (4.5GHz) TRAY", Price = 350, Brand = "AMD", Socket = "AM5", TDP = 105, ImageUrl = "https://m.media-amazon.com/images/I/51p6K6N5p1L._AC_SL1000_.jpg" },
                new Processor { Id = 8, Name = "AMD Ryzen 7 7700 (3.8GHz) TRAY", Price = 320, Brand = "AMD", Socket = "AM5", TDP = 65, ImageUrl = "https://m.media-amazon.com/images/I/51p6K6N5p1L._AC_SL1000_.jpg" },

                // AMD Ryzen 5 & 3
                new Processor { Id = 9, Name = "AMD Ryzen 5 7600X (4.7GHz) TRAY", Price = 240, Brand = "AMD", Socket = "AM5", TDP = 105, ImageUrl = "https://m.media-amazon.com/images/I/61SOfmByXjL._AC_SL1500_.jpg" },
                new Processor { Id = 10, Name = "AMD Ryzen 5 7600 (3.8GHz) TRAY", Price = 215, Brand = "AMD", Socket = "AM5", TDP = 65, ImageUrl = "https://m.media-amazon.com/images/I/61SOfmByXjL._AC_SL1500_.jpg" },
                new Processor { Id = 11, Name = "AMD Ryzen 5 5600X (3.7GHz) TRAY", Price = 155, Brand = "AMD", Socket = "AM4", TDP = 65, ImageUrl = "https://m.media-amazon.com/images/I/61SOfmByXjL._AC_SL1500_.jpg" },
                new Processor { Id = 12, Name = "AMD Ryzen 5 5600 (3.5GHz) TRAY", Price = 135, Brand = "AMD", Socket = "AM4", TDP = 65, ImageUrl = "https://m.media-amazon.com/images/I/61SOfmByXjL._AC_SL1500_.jpg" },
                new Processor { Id = 13, Name = "AMD Ryzen 3 4100 (3.8GHz) TRAY", Price = 75, Brand = "AMD", Socket = "AM4", TDP = 65, ImageUrl = "https://m.media-amazon.com/images/I/61SOfmByXjL._AC_SL1500_.jpg" },
                new Processor { Id = 14, Name = "AMD Ryzen 3 3200G (3.6GHz) TRAY", Price = 85, Brand = "AMD", Socket = "AM4", TDP = 65, ImageUrl = "https://m.media-amazon.com/images/I/61SOfmByXjL._AC_SL1500_.jpg" },

                // INTEL i9
                new Processor { Id = 15, Name = "Intel Core i9-14900K (3.2GHz) TRAY", Price = 630, Brand = "Intel", Socket = "LGA1700", TDP = 125, ImageUrl = "https://m.media-amazon.com/images/I/61S7Ej68L+L._AC_SL1000_.jpg" },
                new Processor { Id = 16, Name = "Intel Core i9-14900KF (3.2GHz) TRAY", Price = 605, Brand = "Intel", Socket = "LGA1700", TDP = 125, ImageUrl = "https://m.media-amazon.com/images/I/61S7Ej68L+L._AC_SL1000_.jpg" },
                new Processor { Id = 17, Name = "Intel Core i9-13900K (3.0GHz) TRAY", Price = 575, Brand = "Intel", Socket = "LGA1700", TDP = 125, ImageUrl = "https://m.media-amazon.com/images/I/61S7Ej68L+L._AC_SL1000_.jpg" },
                new Processor { Id = 18, Name = "Intel Core i9-13900KF (3.0GHz) TRAY", Price = 550, Brand = "Intel", Socket = "LGA1700", TDP = 125, ImageUrl = "https://m.media-amazon.com/images/I/61S7Ej68L+L._AC_SL1000_.jpg" },

                // INTEL i7
                new Processor { Id = 19, Name = "Intel Core i7-14700K (3.4GHz) TRAY", Price = 440, Brand = "Intel", Socket = "LGA1700", TDP = 125, ImageUrl = "https://m.media-amazon.com/images/I/61fS0Xp8YpL._AC_SL1000_.jpg" },
                new Processor { Id = 20, Name = "Intel Core i7-14700KF (3.4GHz) TRAY", Price = 415, Brand = "Intel", Socket = "LGA1700", TDP = 125, ImageUrl = "https://m.media-amazon.com/images/I/61fS0Xp8YpL._AC_SL1000_.jpg" },
                new Processor { Id = 21, Name = "Intel Core i7-13700K (3.4GHz) TRAY", Price = 390, Brand = "Intel", Socket = "LGA1700", TDP = 125, ImageUrl = "https://m.media-amazon.com/images/I/61fS0Xp8YpL._AC_SL1000_.jpg" },
                new Processor { Id = 22, Name = "Intel Core i7-13700KF (3.4GHz) TRAY", Price = 370, Brand = "Intel", Socket = "LGA1700", TDP = 125, ImageUrl = "https://m.media-amazon.com/images/I/61fS0Xp8YpL._AC_SL1000_.jpg" },

                // INTEL i5
                new Processor { Id = 23, Name = "Intel Core i5-14600K (3.5GHz) TRAY", Price = 340, Brand = "Intel", Socket = "LGA1700", TDP = 125, ImageUrl = "https://m.media-amazon.com/images/I/616v8WvV94L._AC_SL1000_.jpg" },
                new Processor { Id = 24, Name = "Intel Core i5-14600KF (3.5GHz) TRAY", Price = 315, Brand = "Intel", Socket = "LGA1700", TDP = 125, ImageUrl = "https://m.media-amazon.com/images/I/616v8WvV94L._AC_SL1000_.jpg" },
                new Processor { Id = 25, Name = "Intel Core i5-13600K (3.5GHz) TRAY", Price = 310, Brand = "Intel", Socket = "LGA1700", TDP = 125, ImageUrl = "https://m.media-amazon.com/images/I/616v8WvV94L._AC_SL1000_.jpg" },
                new Processor { Id = 26, Name = "Intel Core i5-13600KF (3.5GHz) TRAY", Price = 290, Brand = "Intel", Socket = "LGA1700", TDP = 125, ImageUrl = "https://m.media-amazon.com/images/I/616v8WvV94L._AC_SL1000_.jpg" },
                new Processor { Id = 27, Name = "Intel Core i5-12600K (3.7GHz) TRAY", Price = 230, Brand = "Intel", Socket = "LGA1700", TDP = 125, ImageUrl = "https://m.media-amazon.com/images/I/616v8WvV94L._AC_SL1000_.jpg" },

                // INTEL i3
                new Processor { Id = 28, Name = "Intel Core i3-14100 (3.5GHz) TRAY", Price = 160, Brand = "Intel", Socket = "LGA1700", TDP = 60, ImageUrl = "https://m.media-amazon.com/images/I/51p6K6N5p1L._AC_SL1000_.jpg" },
                new Processor { Id = 29, Name = "Intel Core i3-14100F (3.5GHz) TRAY", Price = 130, Brand = "Intel", Socket = "LGA1700", TDP = 60, ImageUrl = "https://m.media-amazon.com/images/I/51p6K6N5p1L._AC_SL1000_.jpg" },
                new Processor { Id = 30, Name = "Intel Core i3-13100 (3.4GHz) TRAY", Price = 135, Brand = "Intel", Socket = "LGA1700", TDP = 60, ImageUrl = "https://m.media-amazon.com/images/I/51p6K6N5p1L._AC_SL1000_.jpg" },
                new Processor { Id = 31, Name = "Intel Core i3-13100F (3.4GHz) TRAY", Price = 110, Brand = "Intel", Socket = "LGA1700", TDP = 60, ImageUrl = "https://m.media-amazon.com/images/I/51p6K6N5p1L._AC_SL1000_.jpg" },
                new Processor { Id = 32, Name = "Intel Core i3-12100 (3.3GHz) TRAY", Price = 110, Brand = "Intel", Socket = "LGA1700", TDP = 60, ImageUrl = "https://m.media-amazon.com/images/I/51p6K6N5p1L._AC_SL1000_.jpg" },
                new Processor { Id = 33, Name = "Intel Core i3-12100F (3.3GHz) TRAY", Price = 95, Brand = "Intel", Socket = "LGA1700", TDP = 60, ImageUrl = "https://m.media-amazon.com/images/I/51p6K6N5p1L._AC_SL1000_.jpg" }
            );

            // ===================== 2. GPUs (SEED DATA) =====================
            modelBuilder.Entity<GPU>().HasData(
                // NVIDIA RTX 40
                new GPU { Id = 1, Name = "NVIDIA GeForce RTX 4090 24GB", Price = 1950, Brand = "Nvidia", RecommendedPSU = 1000, ImageUrl = "https://m.media-amazon.com/images/I/81xU+7F7L7L._AC_SL1500_.jpg" },
                new GPU { Id = 2, Name = "NVIDIA GeForce RTX 4080 SUPER 16GB", Price = 1100, Brand = "Nvidia", RecommendedPSU = 850, ImageUrl = "https://m.media-amazon.com/images/I/71Xf+6pY90L._AC_SL1500_.jpg" },
                new GPU { Id = 3, Name = "NVIDIA GeForce RTX 4080 16GB", Price = 1050, Brand = "Nvidia", RecommendedPSU = 850, ImageUrl = "https://m.media-amazon.com/images/I/71Xf+6pY90L._AC_SL1500_.jpg" },
                new GPU { Id = 4, Name = "NVIDIA GeForce RTX 4070 Ti SUPER 16GB", Price = 880, Brand = "Nvidia", RecommendedPSU = 750, ImageUrl = "https://m.media-amazon.com/images/I/71XmjJ1N7RL._AC_SL1500_.jpg" },
                new GPU { Id = 5, Name = "NVIDIA GeForce RTX 4070 Ti 12GB", Price = 800, Brand = "Nvidia", RecommendedPSU = 750, ImageUrl = "https://m.media-amazon.com/images/I/71XmjJ1N7RL._AC_SL1500_.jpg" },
                new GPU { Id = 6, Name = "NVIDIA GeForce RTX 4070 SUPER 12GB", Price = 660, Brand = "Nvidia", RecommendedPSU = 700, ImageUrl = "https://m.media-amazon.com/images/I/71XmjJ1N7RL._AC_SL1500_.jpg" },
                new GPU { Id = 7, Name = "NVIDIA GeForce RTX 4070 12GB", Price = 600, Brand = "Nvidia", RecommendedPSU = 650, ImageUrl = "https://m.media-amazon.com/images/I/71XmjJ1N7RL._AC_SL1500_.jpg" },
                new GPU { Id = 8, Name = "NVIDIA GeForce RTX 4060 Ti 8GB", Price = 410, Brand = "Nvidia", RecommendedPSU = 550, ImageUrl = "https://m.media-amazon.com/images/I/716m2O2GkTL._AC_SL1500_.jpg" },
                new GPU { Id = 9, Name = "NVIDIA GeForce RTX 4060 8GB", Price = 310, Brand = "Nvidia", RecommendedPSU = 550, ImageUrl = "https://m.media-amazon.com/images/I/716m2O2GkTL._AC_SL1500_.jpg" },

                // NVIDIA RTX 30
                new GPU { Id = 10, Name = "NVIDIA GeForce RTX 3090 Ti 24GB", Price = 1200, Brand = "Nvidia", RecommendedPSU = 850, ImageUrl = "https://m.media-amazon.com/images/I/81xU+7F7L7L._AC_SL1500_.jpg" },
                new GPU { Id = 11, Name = "NVIDIA GeForce RTX 3090 24GB", Price = 1100, Brand = "Nvidia", RecommendedPSU = 850, ImageUrl = "https://m.media-amazon.com/images/I/81xU+7F7L7L._AC_SL1500_.jpg" },
                new GPU { Id = 12, Name = "NVIDIA GeForce RTX 3080 Ti 12GB", Price = 900, Brand = "Nvidia", RecommendedPSU = 750, ImageUrl = "..." },
                new GPU { Id = 13, Name = "NVIDIA GeForce RTX 3080 10GB", Price = 700, Brand = "Nvidia", RecommendedPSU = 750, ImageUrl = "..." },
                new GPU { Id = 14, Name = "NVIDIA GeForce RTX 3070 Ti 8GB", Price = 580, Brand = "Nvidia", RecommendedPSU = 750, ImageUrl = "..." },
                new GPU { Id = 15, Name = "NVIDIA GeForce RTX 3070 8GB", Price = 500, Brand = "Nvidia", RecommendedPSU = 650, ImageUrl = "..." },
                new GPU { Id = 16, Name = "NVIDIA GeForce RTX 3060 Ti 8GB", Price = 380, Brand = "Nvidia", RecommendedPSU = 600, ImageUrl = "..." },
                new GPU { Id = 17, Name = "NVIDIA GeForce RTX 3060 12GB", Price = 290, Brand = "Nvidia", RecommendedPSU = 550, ImageUrl = "..." },
                new GPU { Id = 18, Name = "NVIDIA GeForce RTX 3050 8GB", Price = 230, Brand = "Nvidia", RecommendedPSU = 550, ImageUrl = "..." },

                // NVIDIA RTX 20 & GTX & GT
                new GPU { Id = 19, Name = "NVIDIA GeForce RTX 2080 Ti 11GB", Price = 500, Brand = "Nvidia", RecommendedPSU = 650, ImageUrl = "..." },
                new GPU { Id = 20, Name = "NVIDIA GeForce RTX 2080 Super", Price = 450, Brand = "Nvidia", RecommendedPSU = 650, ImageUrl = "..." },
                new GPU { Id = 21, Name = "NVIDIA GeForce RTX 2070 Super", Price = 350, Brand = "Nvidia", RecommendedPSU = 650, ImageUrl = "..." },
                new GPU { Id = 22, Name = "NVIDIA GeForce RTX 2060 Super", Price = 280, Brand = "Nvidia", RecommendedPSU = 550, ImageUrl = "..." },
                new GPU { Id = 23, Name = "NVIDIA GeForce RTX 2060 6GB", Price = 220, Brand = "Nvidia", RecommendedPSU = 500, ImageUrl = "..." },
                new GPU { Id = 24, Name = "NVIDIA GeForce GTX 1660 SUPER", Price = 210, Brand = "Nvidia", RecommendedPSU = 450, ImageUrl = "..." },
                new GPU { Id = 25, Name = "NVIDIA GeForce GTX 1650 SUPER", Price = 160, Brand = "Nvidia", RecommendedPSU = 350, ImageUrl = "..." },
                new GPU { Id = 26, Name = "NVIDIA GeForce GTX 1630", Price = 130, Brand = "Nvidia", RecommendedPSU = 300, ImageUrl = "..." },
                new GPU { Id = 27, Name = "NVIDIA GeForce GTX 1080 Ti 11GB", Price = 300, Brand = "Nvidia", RecommendedPSU = 600, ImageUrl = "..." },
                new GPU { Id = 28, Name = "NVIDIA GeForce GTX 1080 8GB", Price = 250, Brand = "Nvidia", RecommendedPSU = 500, ImageUrl = "..." },
                new GPU { Id = 29, Name = "NVIDIA GeForce GTX 1070 Ti", Price = 220, Brand = "Nvidia", RecommendedPSU = 500, ImageUrl = "..." },
                new GPU { Id = 30, Name = "NVIDIA GeForce GTX 1060 6GB", Price = 150, Brand = "Nvidia", RecommendedPSU = 400, ImageUrl = "..." },
                new GPU { Id = 31, Name = "NVIDIA GeForce GTX 1050 Ti 4GB", Price = 110, Brand = "Nvidia", RecommendedPSU = 300, ImageUrl = "..." },
                new GPU { Id = 32, Name = "NVIDIA GeForce GTX 1030 2GB", Price = 80, Brand = "Nvidia", RecommendedPSU = 300, ImageUrl = "..." },
                new GPU { Id = 33, Name = "NVIDIA GeForce GTX 980 Ti", Price = 120, Brand = "Nvidia", RecommendedPSU = 600, ImageUrl = "..." },
                new GPU { Id = 34, Name = "NVIDIA GeForce GTX 970", Price = 90, Brand = "Nvidia", RecommendedPSU = 500, ImageUrl = "..." },
                new GPU { Id = 35, Name = "NVIDIA GeForce GTX 750 Ti", Price = 60, Brand = "Nvidia", RecommendedPSU = 300, ImageUrl = "..." },
                new GPU { Id = 36, Name = "NVIDIA GeForce GT 730 2GB", Price = 55, Brand = "Nvidia", RecommendedPSU = 300, ImageUrl = "..." },
                new GPU { Id = 37, Name = "NVIDIA GeForce GT 710 2GB", Price = 45, Brand = "Nvidia", RecommendedPSU = 300, ImageUrl = "..." },

                // AMD Radeon
                new GPU { Id = 38, Name = "AMD Radeon RX 7900 XTX 24GB", Price = 1050, Brand = "AMD", RecommendedPSU = 850, ImageUrl = "https://m.media-amazon.com/images/I/81sh97S0tWL._AC_SL1500_.jpg" },
                new GPU { Id = 39, Name = "AMD Radeon RX 7900 XT 20GB", Price = 840, Brand = "AMD", RecommendedPSU = 750, ImageUrl = "..." },
                new GPU { Id = 40, Name = "AMD Radeon RX 7800 XT 16GB", Price = 540, Brand = "AMD", RecommendedPSU = 750, ImageUrl = "..." },
                new GPU { Id = 41, Name = "AMD Radeon RX 7700 XT 12GB", Price = 460, Brand = "AMD", RecommendedPSU = 700, ImageUrl = "..." },
                new GPU { Id = 42, Name = "AMD Radeon RX 7600 8GB", Price = 280, Brand = "AMD", RecommendedPSU = 550, ImageUrl = "..." },
                new GPU { Id = 43, Name = "AMD Radeon RX 6950 XT", Price = 690, Brand = "AMD", RecommendedPSU = 850, ImageUrl = "..." },
                new GPU { Id = 44, Name = "AMD Radeon RX 6800 XT", Price = 500, Brand = "AMD", RecommendedPSU = 750, ImageUrl = "..." },
                new GPU { Id = 45, Name = "AMD Radeon RX 6700 XT", Price = 350, Brand = "AMD", RecommendedPSU = 650, ImageUrl = "..." },
                new GPU { Id = 46, Name = "AMD Radeon RX 6600 XT", Price = 260, Brand = "AMD", RecommendedPSU = 500, ImageUrl = "..." },
                new GPU { Id = 47, Name = "AMD Radeon RX 6600", Price = 220, Brand = "AMD", RecommendedPSU = 450, ImageUrl = "..." },
                new GPU { Id = 48, Name = "AMD Radeon RX 5700 XT", Price = 200, Brand = "AMD", RecommendedPSU = 600, ImageUrl = "..." },
                new GPU { Id = 49, Name = "AMD Radeon RX 590 8GB", Price = 130, Brand = "AMD", RecommendedPSU = 550, ImageUrl = "..." },
                new GPU { Id = 50, Name = "AMD Radeon RX 580 8GB", Price = 110, Brand = "AMD", RecommendedPSU = 500, ImageUrl = "..." },
                new GPU { Id = 51, Name = "AMD Radeon RX 570 4GB", Price = 90, Brand = "AMD", RecommendedPSU = 450, ImageUrl = "..." },
                new GPU { Id = 52, Name = "AMD Radeon RX 550 4GB", Price = 70, Brand = "AMD", RecommendedPSU = 350, ImageUrl = "..." },
                new GPU { Id = 53, Name = "AMD Radeon Vega 64", Price = 250, Brand = "AMD", RecommendedPSU = 750, ImageUrl = "..." },
                new GPU { Id = 54, Name = "AMD Radeon HD 7770 1GB", Price = 40, Brand = "AMD", RecommendedPSU = 450, ImageUrl = "..." }
            );

            // ===================== 3. MOTHERBOARDS (SEED DATA) =====================
            modelBuilder.Entity<Motherboard>().HasData(
                // Intel
                new Motherboard { Id = 1, Name = "ASUS ROG Strix Z790-E Gaming WiFi", Price = 520, Brand = "Intel", Socket = "LGA1700", FormFactor = "ATX", MemoryType = "DDR5", ImageUrl = "https://m.media-amazon.com/images/I/81O58vLIDeL._AC_SL1500_.jpg" },
                new Motherboard { Id = 2, Name = "GIGABYTE Z790 AORUS Elite AX", Price = 280, Brand = "Intel", Socket = "LGA1700", FormFactor = "ATX", MemoryType = "DDR5", ImageUrl = "..." },
                new Motherboard { Id = 3, Name = "MSI MAG Z690 Tomahawk WiFi", Price = 250, Brand = "Intel", Socket = "LGA1700", FormFactor = "ATX", MemoryType = "DDR5", ImageUrl = "..." },
                new Motherboard { Id = 4, Name = "ASUS TUF Gaming B760M-Plus WiFi", Price = 180, Brand = "Intel", Socket = "LGA1700", FormFactor = "mATX", MemoryType = "DDR5", ImageUrl = "..." },
                new Motherboard { Id = 5, Name = "MSI PRO B760M-A WiFi", Price = 160, Brand = "Intel", Socket = "LGA1700", FormFactor = "mATX", MemoryType = "DDR4", ImageUrl = "..." },
                new Motherboard { Id = 6, Name = "GIGABYTE B660M DS3H", Price = 120, Brand = "Intel", Socket = "LGA1700", FormFactor = "mATX", MemoryType = "DDR4", ImageUrl = "..." },
                new Motherboard { Id = 7, Name = "ASUS PRIME H610M-K D4", Price = 95, Brand = "Intel", Socket = "LGA1700", FormFactor = "mATX", MemoryType = "DDR4", ImageUrl = "..." },
                new Motherboard { Id = 8, Name = "ASUS Z590 Gaming WiFi", Price = 170, Brand = "Intel", Socket = "LGA1200", FormFactor = "ATX", MemoryType = "DDR4", ImageUrl = "..." },
                new Motherboard { Id = 9, Name = "MSI Z390-A PRO", Price = 130, Brand = "Intel", Socket = "LGA1151", FormFactor = "ATX", MemoryType = "DDR4", ImageUrl = "..." },

                // AMD
                new Motherboard { Id = 10, Name = "ASUS ROG Crosshair X670E Hero", Price = 650, Brand = "AMD", Socket = "AM5", FormFactor = "ATX", MemoryType = "DDR5", ImageUrl = "..." },
                new Motherboard { Id = 11, Name = "GIGABYTE X670E AORUS Master", Price = 480, Brand = "AMD", Socket = "AM5", FormFactor = "ATX", MemoryType = "DDR5", ImageUrl = "..." },
                new Motherboard { Id = 12, Name = "MSI MPG B650 Carbon WiFi", Price = 300, Brand = "AMD", Socket = "AM5", FormFactor = "ATX", MemoryType = "DDR5", ImageUrl = "..." },
                new Motherboard { Id = 13, Name = "ASRock B650 Steel Legend", Price = 240, Brand = "AMD", Socket = "AM5", FormFactor = "ATX", MemoryType = "DDR5", ImageUrl = "..." },
                new Motherboard { Id = 14, Name = "ASUS PRIME A620M-A", Price = 110, Brand = "AMD", Socket = "AM5", FormFactor = "mATX", MemoryType = "DDR5", ImageUrl = "..." },
                new Motherboard { Id = 15, Name = "ASUS ROG Strix X570-E Gaming", Price = 300, Brand = "AMD", Socket = "AM4", FormFactor = "ATX", MemoryType = "DDR4", ImageUrl = "..." },
                new Motherboard { Id = 16, Name = "MSI MAG B550 Tomahawk", Price = 160, Brand = "AMD", Socket = "AM4", FormFactor = "ATX", MemoryType = "DDR4", ImageUrl = "..." },
                new Motherboard { Id = 17, Name = "ASRock B550 Phantom Gaming", Price = 120, Brand = "AMD", Socket = "AM4", FormFactor = "ATX", MemoryType = "DDR4", ImageUrl = "..." },
                new Motherboard { Id = 18, Name = "Gigabyte B450M DS3H", Price = 80, Brand = "AMD", Socket = "AM4", FormFactor = "mATX", MemoryType = "DDR4", ImageUrl = "..." },
                new Motherboard { Id = 19, Name = "ASUS PRIME A520M-K", Price = 65, Brand = "AMD", Socket = "AM4", FormFactor = "mATX", MemoryType = "DDR4", ImageUrl = "..." }
            );

            // ===================== 4. PSUs (SEED DATA) =====================
            modelBuilder.Entity<PSU>().HasData(
                new PSU { Id = 1, Name = "Corsair RM1000x 1000W 80+ Gold", Price = 190, Brand = "Corsair", Wattage = 1000, Rating = "Gold", ImageUrl = "https://m.media-amazon.com/images/I/71Xf+6pY90L._AC_SL1500_.jpg" },
                new PSU { Id = 2, Name = "Corsair RM850x 850W 80+ Gold", Price = 150, Brand = "Corsair", Wattage = 850, Rating = "Gold", ImageUrl = "..." },
                new PSU { Id = 3, Name = "Corsair RM750x 750W 80+ Gold", Price = 130, Brand = "Corsair", Wattage = 750, Rating = "Gold", ImageUrl = "..." },
                new PSU { Id = 4, Name = "Corsair CV550 550W 80+ Bronze", Price = 60, Brand = "Corsair", Wattage = 550, Rating = "Bronze", ImageUrl = "..." },
                new PSU { Id = 5, Name = "Seasonic Focus GX-850 850W Gold", Price = 150, Brand = "Seasonic", Wattage = 850, Rating = "Gold", ImageUrl = "..." },
                new PSU { Id = 6, Name = "Seasonic Focus GX-650 650W Gold", Price = 120, Brand = "Seasonic", Wattage = 650, Rating = "Gold", ImageUrl = "..." },
                new PSU { Id = 7, Name = "be quiet! Pure Power 11 700W Gold", Price = 110, Brand = "be quiet!", Wattage = 700, Rating = "Gold", ImageUrl = "..." },
                new PSU { Id = 8, Name = "EVGA SuperNOVA 850 G5 850W Gold", Price = 145, Brand = "EVGA", Wattage = 850, Rating = "Gold", ImageUrl = "..." },
                new PSU { Id = 9, Name = "EVGA 600 BR 600W 80+ Bronze", Price = 65, Brand = "EVGA", Wattage = 600, Rating = "Bronze", ImageUrl = "..." },
                new PSU { Id = 10, Name = "Gigabyte P650B 650W 80+ Bronze", Price = 70, Brand = "Gigabyte", Wattage = 650, Rating = "Bronze", ImageUrl = "..." },
                new PSU { Id = 11, Name = "Thermaltake Smart RGB 600W", Price = 55, Brand = "Thermaltake", Wattage = 600, Rating = "Bronze", ImageUrl = "..." }
            );

            // ===================== 5. RAM, Storage, Cases =====================
            modelBuilder.Entity<RAM>().HasData(
                new RAM { Id = 1, Name = "Kingston FURY Beast 32GB (2x16) DDR5 6000MHz", Price = 130, Brand = "Kingston", Type = "DDR5", SizeGB = 32, ImageUrl = "..." },
                new RAM { Id = 2, Name = "Kingston FURY Beast 16GB (2x8) DDR5 5200MHz", Price = 80, Brand = "Kingston", Type = "DDR5", SizeGB = 16, ImageUrl = "..." },
                new RAM { Id = 3, Name = "Corsair Vengeance LPX 16GB (2x8) DDR4 3200MHz", Price = 45, Brand = "Corsair", Type = "DDR4", SizeGB = 16, ImageUrl = "..." }
            );

            modelBuilder.Entity<Storage>().HasData(
                new Storage { Id = 1, Name = "Samsung 990 PRO 2TB NVMe", Price = 180, Brand = "Samsung", SizeGB = 2000, Type = "NVMe", ImageUrl = "..." },
                new Storage { Id = 2, Name = "Kingston NV2 1TB NVMe", Price = 65, Brand = "Kingston", SizeGB = 1000, Type = "NVMe", ImageUrl = "..." }
            );

            modelBuilder.Entity<Case>().HasData(
               new Case { Id = 1, Name = "NZXT H7 Flow (ATX)", Price = 130, Brand = "NZXT", SupportedFormFactors = "ATX,mATX", ImageUrl = "..." },
                new Case { Id = 2, Name = "DeepCool CC560 V2 (ATX)", Price = 65, Brand = "DeepCool", SupportedFormFactors = "ATX,mATX", ImageUrl = "..." }
            );
        }
    }
}