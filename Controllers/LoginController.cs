using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TuncayAlbayrakMvcSinav.Data;
using TuncayAlbayrakMvcSinav.Models.Entities;
using TuncayAlbayrakMvcSinav.Models.ViewModels.Login;

namespace TuncayAlbayrakMvcSinav.Controllers
{
    public class LoginController : Controller
    {

        private readonly FilmlerDbContext _dbContext;
        private readonly UserManager<Yonetmen> _userManager;
        private readonly SignInManager<Yonetmen> _signInManager;

        public LoginController(FilmlerDbContext dbContext, UserManager<Yonetmen> userManager, SignInManager<Yonetmen> signInManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _signInManager = signInManager;
        }


        //Login ekranı
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Index(Login_VM login)
        {

            if (ModelState.IsValid)
            {
                var result = _userManager.FindByNameAsync(login.Username).Result;

                if (result != null)
                {
                    bool sifreDoguMu = _userManager.CheckPasswordAsync(result, login.Password).Result;

                    if (sifreDoguMu)
                    {
                        //Uye var ve Sifre doğru
                        await _signInManager.SignInAsync(result, false);
                        return RedirectToAction("Index", "Home");
                    }
                }
            }

            ModelState.AddModelError("HATA", "Kullanıcı adı veya şifre yanlış");
            return View();
        }


        public IActionResult Register()
        {

            return View();
        }



        [HttpPost]
        public async Task<IActionResult> Register(Register_VM uye)
        {

            if (ModelState.IsValid)
            {
                Yonetmen yeniUye = new Yonetmen()
                {
                    Ad = uye.Ad,
                    Soyad = uye.Soyad,
                    UserName = uye.KullaniciAdi,
                    Email = uye.EPosta,
                };

                var result = await _userManager.CreateAsync(yeniUye, uye.Sifre);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Login");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            ModelState.AddModelError("HATA", "Kullanıcı adı veya şifre hatalı");
            return View();
        }



        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }



    }
}
