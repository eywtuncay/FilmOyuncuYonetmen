namespace TuncayAlbayrakMvcSinav.Models.Entities
{
    public class Oyuncu
    {
        public int OyuncuId { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }

        public ICollection<OyuncuFilm>? FilmKategoriler { get; set; }

    }
}
