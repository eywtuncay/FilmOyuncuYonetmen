using TuncayAlbayrakMvcSinav.Models.Entities;

namespace TuncayAlbayrakMvcSinav.Models.ViewModels.Film
{
    public class FilmEklemeFormu_VM
    {
        public FilmEkle_VM Film { get; set; }

        // Seçilen oyuncuların ID’leri
        public List<int> SelectedOyuncuIds { get; set; } = new();

        // Tüm Oyuncular (checkbox için)
        public List<Oyuncu> TumOyuncular { get; set; } = new();
    }
}
