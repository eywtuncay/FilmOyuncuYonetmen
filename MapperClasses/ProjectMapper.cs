using AutoMapper;
using System.Drawing;
using TuncayAlbayrakMvcSinav.Models.Entities;
using TuncayAlbayrakMvcSinav.Models.ViewModels.Film;

namespace TuncayAlbayrakMvcSinav.MapperClasses
{
    public class ProjectMapper:Profile
    {
        public ProjectMapper()
        {
            CreateMap<Film, FilmEkle_VM>().ReverseMap();

            CreateMap<Film, FilmListele_VM>()
                .ForMember(dest => dest.YonetmenAd, opt => opt.MapFrom(src => src.Yonetmen.Ad))  // Üye'nin Ad'ını alıyoruz
                .ForMember(dest => dest.Oyuncular, opt => opt.MapFrom(src => src.OyuncuFilmler.Select(k => k.Oyuncu.Ad).ToList())); // Kategoriler

            // Film -> FilmDetay_VM eşlemesi
            CreateMap<Film, FilmDetay_VM>()
                .ForMember(dest => dest.YonetmenAd, opt => opt.MapFrom(src => src.Yonetmen != null ? src.Yonetmen.UserName : ""))
                .ForMember(dest => dest.Oyuncular, opt => opt.MapFrom(src => src.OyuncuFilmler.Select(kk => kk.Oyuncu.Ad).ToList()));

        }
    }
}
