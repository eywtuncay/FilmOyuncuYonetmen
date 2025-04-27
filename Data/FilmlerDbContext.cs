using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TuncayAlbayrakMvcSinav.Models.Entities;

namespace TuncayAlbayrakMvcSinav.Data
{
    public class FilmlerDbContext : IdentityDbContext<Yonetmen, IdentityRole<int>, int>
    {
        public FilmlerDbContext(DbContextOptions options) : base(options)
        {
        }

        protected FilmlerDbContext()
        {
        }

        public DbSet<OyuncuFilm> OyuncuFilmler { get; set; }
        public DbSet<Oyuncu> Oyuncular { get; set; }
        public DbSet<Film> Filmler { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }

    }
}
