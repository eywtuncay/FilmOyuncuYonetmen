using System.ComponentModel.DataAnnotations;

namespace TuncayAlbayrakMvcSinav.Models.ViewModels.Login
{
    public class Register_VM
    {
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string EPosta { get; set; }
        public string KullaniciAdi { get; set; }
        public string Sifre { get; set; }

        [Compare("Sifre")]
        public string SifreTekrari { get; set; }
    }
}
