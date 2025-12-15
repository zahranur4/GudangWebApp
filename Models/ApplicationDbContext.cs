using Microsoft.EntityFrameworkCore;

namespace GudangWebApp.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Mendaftarkan model Barang agar menjadi tabel di database
        public DbSet<Barang> Barang { get; set; }
    }
}