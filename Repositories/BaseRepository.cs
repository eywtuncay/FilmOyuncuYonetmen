using Microsoft.EntityFrameworkCore;
using TuncayAlbayrakMvcSinav.Abstracts;
using TuncayAlbayrakMvcSinav.Data;

namespace TuncayAlbayrakMvcSinav.Repositories
{
    public class BaseRepository<TEntity> : ICRUD<TEntity> where TEntity : class
    {

        protected readonly FilmlerDbContext _dbContext;
        protected readonly DbSet<TEntity> _table;

        protected BaseRepository(FilmlerDbContext dbContext)
        {
            _dbContext = dbContext;
            _table = _dbContext.Set<TEntity>();
        }


        public TEntity Bul(int id)
        {
            return _table.Find(id);
        }

        public void Ekle(TEntity entity)
        {
            _table.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Guncelle(TEntity entity)
        {
            _table.Update(entity);
            _dbContext.SaveChanges();
        }

        public List<TEntity> Listele()
        {
            return _table.ToList();
        }

        public IQueryable<TEntity> ListeleQuery()
        {
            return _table;
        }

        public void Sil(int id)
        {
            _table.Remove(Bul(id));
            _dbContext.SaveChanges();
        }


    }
}
