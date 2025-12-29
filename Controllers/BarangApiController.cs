using Microsoft.AspNetCore.Mvc;
using GudangWebApp.Models;

namespace GudangWebApp.Controllers
{
    // Menandakan ini adalah API Controller, bukan MVC biasa
    [Route("api/[controller]")]
    [ApiController]
    public class BarangApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BarangApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 1. GET: api/barangapi (Ambil Semua Data)
        [HttpGet]
        public IActionResult GetAll()
        {
            var data = _context.Barang.ToList();
            return Ok(data); // Mengembalikan data dalam format JSON
        }

        // 2. GET: api/barangapi/5 (Ambil 1 Data berdasarkan ID)
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var barang = _context.Barang.Find(id);
            if (barang == null)
            {
                return NotFound(); // Return 404 jika tidak ketemu
            }
            return Ok(barang);
        }

        // 3. POST: api/barangapi (Tambah Data Baru)
        [HttpPost]
        public IActionResult Create([FromBody] Barang barang)
        {
            _context.Barang.Add(barang);
            _context.SaveChanges();

            // Return 201 Created dan kasih info lokasi data barunya
            return CreatedAtAction(nameof(GetById), new { id = barang.Id }, barang);
        }

        // 4. PUT: api/barangapi/5 (Update Data)
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Barang barang)
        {
            var existing = _context.Barang.Find(id);
            if (existing == null)
            {
                return NotFound();
            }

            // Update data yang ada
            existing.KodeBarang = barang.KodeBarang;
            existing.NamaBarang = barang.NamaBarang;
            existing.JumlahStok = barang.JumlahStok;
            existing.Kategori = barang.Kategori;

            _context.SaveChanges();
            return NoContent(); // Return 204 (Sukses tapi tidak ada konten yg dikembalikan)
        }

        // 5. DELETE: api/barangapi/5 (Hapus Data)
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var barang = _context.Barang.Find(id);
            if (barang == null)
            {
                return NotFound();
            }

            _context.Barang.Remove(barang);
            _context.SaveChanges();
            return Ok(); // Return 200 OK
        }
    }
}