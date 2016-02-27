using System.Linq;

namespace DataAccess.Abstract.Menu
{
    public interface IManager<TEntity>
    {
        IQueryable<TEntity> Get();
        TEntity Get(int id);
    }
}
