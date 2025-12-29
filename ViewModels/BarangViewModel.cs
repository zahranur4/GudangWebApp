using System.ComponentModel.DataAnnotations;

namespace GudangWebApp.ViewModels
{
    public class BarangViewModel
    {
        [Required(ErrorMessage = "Kode Barang wajib diisi")]
        [Display(Name = "Kode Barang")]
        public string KodeBarang { get; set; }

        [Required(ErrorMessage = "Nama Barang wajib diisi")]
        [Display(Name = "Nama Barang")]
        public string NamaBarang { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Stok tidak boleh negatif")]
        [Display(Name = "Jumlah Stok")]
        public int JumlahStok { get; set; }

        [Display(Name = "Kategori")]
        public string Kategori { get; set; }
    }
}