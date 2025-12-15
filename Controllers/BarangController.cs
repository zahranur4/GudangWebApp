using Microsoft.AspNetCore.Mvc;
using GudangWebApp.Models; // Pastikan using ini ada

namespace GudangWebApp.Controllers
{
    public class BarangController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Constructor Injection untuk mengambil akses database
        public BarangController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Menampilkan daftar barang
        public IActionResult Index()
        {
            var data = _context.Barang.ToList();
            return View(data);
        }

        // Menampilkan form tambah barang
        public IActionResult Create() => View();

        // Memproses data yang dikirim dari form (POST)
        [HttpPost]
        public IActionResult Create(Barang barang)
        {
            if (ModelState.IsValid)
            {
                _context.Barang.Add(barang); // Menambah data ke memory
                _context.SaveChanges();      // Menyimpan ke database
                return RedirectToAction("Index");
            }
            return View(barang);
        }
    }
}