using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TuncayAlbayrakMvcSinav.Models;
using TuncayAlbayrakMvcSinav.Models.ViewModels.Film;
using TuncayAlbayrakMvcSinav.Repositories;

namespace TuncayAlbayrakMvcSinav.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly FilmRepository _filmRepository;
        private readonly IMapper _mapper;

        public HomeController(FilmRepository filmRepository, OyuncuRepository oyuncuRepository, IMapper mapper)
        {
            _filmRepository = filmRepository;
            _mapper = mapper;
        }


        public IActionResult Index()
        {
            // Tüm Blogları alıyoruz
            var kitaplar = _filmRepository.ListeleQuery()
                .Include(k => k.Yonetmen) // Bloglarla ilişkili Yazar bilgilerini de alıyoruz
                .Include(t => t.OyuncuFilmler) // BlogKategori ilişkisini dahil ediyoruz
                .ThenInclude(k => k.Oyuncu) // Kategori bilgilerini de dahil ediyoruz
                .ToList();

            // AutoMapper ile veriyi ViewModel'e dönüştür
            var viewData = _mapper.Map<List<FilmListele_VM>>(kitaplar);

            return View(viewData);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
