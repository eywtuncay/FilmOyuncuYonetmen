using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TuncayAlbayrakMvcSinav.Models.Entities;

namespace TuncayAlbayrakMvcSinav.Configurations
{
    public class OyuncuFilm_CFG : IEntityTypeConfiguration<OyuncuFilm>
    {
        public void Configure(EntityTypeBuilder<OyuncuFilm> builder)
        {

            builder.HasOne(kk => kk.Film)
                   .WithMany(k => k.OyuncuFilmler)
                   .HasForeignKey(kk => kk.FilmId);

            builder.HasOne(kk => kk.Oyuncu)
                   .WithMany(k => k.FilmKategoriler)
                   .HasForeignKey(kk => kk.OyuncuId);

        }
    }
}
