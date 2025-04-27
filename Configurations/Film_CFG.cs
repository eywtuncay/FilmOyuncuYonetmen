using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TuncayAlbayrakMvcSinav.Models.Entities;

namespace TuncayAlbayrakMvcSinav.Configurations
{
    public class Film_CFG : IEntityTypeConfiguration<Film>
    {
        public void Configure(EntityTypeBuilder<Film> builder)
        {
            builder.Property(x => x.Ad)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.Sure)
                .IsRequired();

            builder.Property(x => x.Ulke)
                .IsRequired()
                .HasMaxLength(15);

        }
    }
}
