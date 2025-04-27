using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using TuncayAlbayrakMvcSinav.Models.Entities;
using TuncayAlbayrakMvcSinav.Models.ViewModels.Film;
using TuncayAlbayrakMvcSinav.Repositories;

namespace TuncayAlbayrakMvcSinav.Controllers
{
    [Authorize]
    public class FilmController : Controller
    {

        private readonly FilmRepository _filmRepository;
        private readonly OyuncuRepository _oyuncuRepository;
        private readonly IMapper _mapper;

        public FilmController(FilmRepository filmRepository, OyuncuRepository oyuncuRepository, IMapper mapper)
        {
            _filmRepository = filmRepository;
            _oyuncuRepository = oyuncuRepository;
            _mapper = mapper;
        }


        // Film listesini gösteren sayfa
        public IActionResult Index()
        {
            var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value);

            // Yalnızca giriş yapan kullanıcıya ait filmleri alıyoruz
            var kullaniciKitaplari = _filmRepository.ListeleQuery()
                .Where(t => t.YonetmenId == userId) // Veya tüm filmler için koşul kaldırılabilir
                .Include(k => k.Yonetmen) // yonetmen ilişkisini dahil ediyoruz
                .Include(t => t.OyuncuFilmler) // OyuncuFilmler ilişkisini dahil ediyoruz
                .ThenInclude(k => k.Oyuncu) // Oyuncu bilgilerini de dahil ediyoruz
                .ToList();

            // AutoMapper ile veriyi ViewModel'e dönüştür
            var viewData = _mapper.Map<List<FilmListele_VM>>(kullaniciKitaplari);

            return View(viewData);
        }


        public IActionResult Ekle()
        {
            var vm = new FilmEklemeFormu_VM()
            {
                Film = new FilmEkle_VM(),
                TumOyuncular = _oyuncuRepository.Listele()
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult Ekle(FilmEklemeFormu_VM model)
        {

            if (!ModelState.IsValid)
            {
                model.TumOyuncular = _oyuncuRepository.Listele();
                return View(model);
            }

            // ViewModel'den gelen Kitap verisini Kitap entity'sine map ediyoruz
            var yeniKitap = _mapper.Map<Film>(model.Film);

            // Giriş yapan kullanıcının ID'sini alıyoruz ve Kitap entity'sine atıyoruz
            var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value);
            yeniKitap.YonetmenId = userId;

            // ViewModel'den gelen seçili kategori ID'lerini KitapKategori ilişkisine dönüştürüyoruz
            yeniKitap.OyuncuFilmler = model.SelectedOyuncuIds.Select(id => new OyuncuFilm
            {
                OyuncuId = id
            }).ToList();

            // Kitap ve ilişkili kategorileri veritabanına kaydediyoruz
            _filmRepository.Ekle(yeniKitap);

            // Kitap eklendikten sonra kitap listesini gösteren sayfaya yönlendiriyoruz
            return RedirectToAction("Index");
        }


        // Film güncelleme sayfası
        public IActionResult Edit(int id)
        {
            // Kitabı ve ilişkili kategorileri bul
            var film = _filmRepository.ListeleQuery()
                .Include(k => k.OyuncuFilmler)
                .ThenInclude(kk => kk.Oyuncu)
                .FirstOrDefault(k => k.FilmId == id);

            if (film == null)
            {
                return NotFound();
            }

            // Kitap bilgilerini ViewModel'e map et
            var vm = new FilmGuncelle_VM
            {
                FilmId = film.FilmId,
                Ad = film.Ad,
                Sure = film.Sure,
                Ulke = film.Ulke,
                YapimYili = film.YapimYili,

                TumOyuncular = _oyuncuRepository.Listele(),
                SelectedOyuncuIds = film.OyuncuFilmler.Select(kk => kk.OyuncuId).ToList()
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult Edit(int id, FilmGuncelle_VM model)
        {

            if (!ModelState.IsValid)
            {
                model.TumOyuncular = _oyuncuRepository.Listele();
                return View(model);
            }

            // Mevcut kitabı bul
            var kitap = _filmRepository.ListeleQuery()
                .Include(k => k.OyuncuFilmler)
                .FirstOrDefault(k => k.FilmId == id);

            if (kitap == null)
            {
                return NotFound();
            }

            // Mevcut UyeId'yi koruyun
            var mevcutYonetmenId = kitap.YonetmenId;

            // Kitap bilgilerini güncelle
            kitap.Ad = model.Ad;
            kitap.Sure = model.Sure;
            kitap.Ulke = model.Ulke;
            kitap.YapimYili = model.YapimYili;

            // UyeId'yi koruyun
            kitap.YonetmenId = mevcutYonetmenId;

            // Mevcut kategorileri temizle
            kitap.OyuncuFilmler.Clear();

            // Yeni kategorileri ekle
            kitap.OyuncuFilmler = model.SelectedOyuncuIds.Select(oyuncuId => new OyuncuFilm
            {
                FilmId = kitap.FilmId,
                OyuncuId = oyuncuId
            }).ToList();

            // Veritabanında güncelle
            _filmRepository.Guncelle(kitap);

            return RedirectToAction("Index");
        }


        //Film Detay sayfası
        [AllowAnonymous]
        public IActionResult Details(int id)
        {
            // Kitabı ve ilişkili kategorileri bul
            var kitap = _filmRepository.ListeleQuery()
                .Include(k => k.Yonetmen) // Kullanıcı bilgilerini dahil et
                .Include(k => k.OyuncuFilmler)
                .ThenInclude(kk => kk.Oyuncu)
                .FirstOrDefault(k => k.FilmId == id);

            if (kitap == null)
            {
                return NotFound();
            }

            // Kitap bilgilerini ViewModel'e map et
            var vm = _mapper.Map<FilmDetay_VM>(kitap);

            return View(vm);
        }


        //Film silme işlemi
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Delete(int id)
        {
            // Kitabı bul
            var kitap = _filmRepository.Bul(id);
            if (kitap == null)
            {
                return NotFound();
            }

            // Kitabı sil
            _filmRepository.Sil(id);

            // Kitap listesini gösteren sayfaya yönlendir
            return RedirectToAction("Index");
        }



    }
}
