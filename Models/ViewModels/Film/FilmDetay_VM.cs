namespace TuncayAlbayrakMvcSinav.Models.ViewModels.Film
{
    public class FilmDetay_VM
    {
        public int FilmId { get; set; }
        public string Ad { get; set; }
        public int Sure { get; set; }
        public string Ulke { get; set; }
        public DateTime YapimYili { get; set; }

        public string YonetmenAd { get; set; }
        public List<string> Oyuncular { get; set; }
    }
}
