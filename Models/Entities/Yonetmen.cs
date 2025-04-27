using Microsoft.AspNetCore.Identity;

namespace TuncayAlbayrakMvcSinav.Models.Entities
{
    public class Yonetmen : IdentityUser<int>
    {
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public DateTime DogumTarihi { get; set; }

        public ICollection<Film>? Filmler { get; set; }

    }
}
