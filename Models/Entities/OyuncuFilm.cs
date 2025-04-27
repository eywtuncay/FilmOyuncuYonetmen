namespace TuncayAlbayrakMvcSinav.Models.Entities
{
    public class OyuncuFilm
    {

        public int OyuncuFilmId { get; set; }

        public int OyuncuId { get; set; }
        public Oyuncu? Oyuncu { get; set; }

        public int FilmId { get; set; }
        public Film? Film { get; set; }


    }
}
