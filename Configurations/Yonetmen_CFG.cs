using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TuncayAlbayrakMvcSinav.Models.Entities;

namespace TuncayAlbayrakMvcSinav.Configurations
{
    public class Yonetmen_CFG : IEntityTypeConfiguration<Yonetmen>
    {
        public void Configure(EntityTypeBuilder<Yonetmen> builder)
        {

            builder.Property(x => x.Ad)
               .HasMaxLength(30)
               .IsRequired();

            builder.Property(x => x.Soyad)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.DogumTarihi)
                .IsRequired();

        }
    }
}
