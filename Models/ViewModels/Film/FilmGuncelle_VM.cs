using TuncayAlbayrakMvcSinav.Models.Entities;

namespace TuncayAlbayrakMvcSinav.Models.ViewModels.Film
{
    public class FilmGuncelle_VM
    {
        public int FilmId { get; set; }
        public string Ad { get; set; }
        public int Sure { get; set; }
        public string Ulke { get; set; }
        public DateTime YapimYili { get; set; }

        // Seçilen kategorilerin ID'leri
        public List<int> SelectedOyuncuIds { get; set; } = new();

        // Tüm kategoriler (checkbox için)
        public List<Oyuncu> TumOyuncular { get; set; } = new();


    }
}
