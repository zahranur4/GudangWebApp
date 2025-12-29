using GudangWebApp.Models;
using GudangWebApp.ViewModels; // Wajib ada agar BarangViewModel terbaca
using Microsoft.AspNetCore.Mvc;

namespace GudangWebApp.Controllers
{
    public class BarangController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BarangController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 1. READ: Menampilkan daftar barang
        public IActionResult Index()
        {
            var data = _context.Barang.ToList();
            return View(data);
        }

        // 2. CREATE: Menampilkan form tambah (GET)
        public IActionResult Create() => View();

        // 2. CREATE: Memproses data (POST) - Gunakan Versi ViewModel (Sesuai Bab 10)
        [HttpPost]
        public IActionResult Create(BarangViewModel vm)
        {
            if (ModelState.IsValid)
            {
                // Pindahkan data dari ViewModel ke Model asli
                var barang = new Barang
                {
                    KodeBarang = vm.KodeBarang,
                    NamaBarang = vm.NamaBarang,
                    JumlahStok = vm.JumlahStok,
                    Kategori = vm.Kategori
                };

                _context.Barang.Add(barang);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            // Jika validasi gagal, kembalikan ke form
            return View(vm);
        }

        // 3. EDIT: Menampilkan Form Edit (GET)
        public IActionResult Edit(int id)
        {
            var barang = _context.Barang.Find(id);
            if (barang == null) return NotFound();
            return View(barang);
        }

        // 3. EDIT: Memproses Update Data (POST)
        // Untuk Edit, kita pakai Model asli saja biar cepat (kecuali modul minta lain)
        [HttpPost]
        public IActionResult Edit(Barang barang)
        {
            if (ModelState.IsValid)
            {
                _context.Barang.Update(barang);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(barang);
        }

        // 4. DELETE: Menampilkan Halaman Konfirmasi (GET)
        public IActionResult Delete(int id)
        {
            var barang = _context.Barang.Find(id);
            if (barang == null) return NotFound();
            return View(barang);
        }

        // 4. DELETE: Hapus Data Permanen (POST)
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var barang = _context.Barang.Find(id);
            if (barang != null)
            {
                _context.Barang.Remove(barang);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}