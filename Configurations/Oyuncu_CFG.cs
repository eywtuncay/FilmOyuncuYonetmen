using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TuncayAlbayrakMvcSinav.Models.Entities;

namespace TuncayAlbayrakMvcSinav.Configurations
{
    public class Oyuncu_CFG : IEntityTypeConfiguration<Oyuncu>
    {
        public void Configure(EntityTypeBuilder<Oyuncu> builder)
        {

            builder.Property(x => x.Ad)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(x => x.Soyad)
                .IsRequired()
                .HasMaxLength(50);


            builder.HasData(
                new Oyuncu
                {
                    OyuncuId = 1,
                    Ad = "Ramiz",
                    Soyad = "Karaeski",
                    
                },
                new Oyuncu
                {
                    OyuncuId = 2,
                    Ad = "Pala",
                    Soyad = "Pala",
                },
                new Oyuncu
                {
                    OyuncuId = 3,
                    Ad = "Walter",
                    Soyad = "White",
                }
            );


        }
    }
}
