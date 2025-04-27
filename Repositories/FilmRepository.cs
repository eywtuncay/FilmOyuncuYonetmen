using AutoMapper;
using TuncayAlbayrakMvcSinav.Data;
using TuncayAlbayrakMvcSinav.Models.Entities;

namespace TuncayAlbayrakMvcSinav.Repositories
{
    public class FilmRepository : BaseRepository<Film>
    {

        private readonly IMapper _mapper;

        public FilmRepository(FilmlerDbContext dbContext, IMapper mapper) : base(dbContext)
        {
            _mapper = mapper;
        }


    }
}
