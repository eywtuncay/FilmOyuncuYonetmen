namespace TuncayAlbayrakMvcSinav.Models.Entities
{
    public class Film
    {
        public int FilmId { get; set; }
        public string Ad { get; set; }
        public int Sure { get; set; }
        public string Ulke { get; set; }
        public DateTime YapimYili { get; set; }


        public int YonetmenId { get; set; }
        public Yonetmen? Yonetmen { get; set; }

        public ICollection<OyuncuFilm>? OyuncuFilmler { get; set; }

    }
}
