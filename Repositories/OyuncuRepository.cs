using TuncayAlbayrakMvcSinav.Data;
using TuncayAlbayrakMvcSinav.Models.Entities;

namespace TuncayAlbayrakMvcSinav.Repositories
{
    public class OyuncuRepository : BaseRepository<Oyuncu>
    {
        public OyuncuRepository(FilmlerDbContext dbContext) : base(dbContext)
        {
        }
    }
}
