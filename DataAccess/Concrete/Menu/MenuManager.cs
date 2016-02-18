using DataAccess.Abstract.Menu;
using DataModel.Contexts;

namespace DataAccess.Concrete.Menu
{
    public class MenuManager : IMenuManager
    {
        private RestorauntDbContext _ctx = new ContextManager().Context;

        public DataModel.Model.Menu GetByRestaurant(int rstId)
        {
            var menu = _ctx.Restoraunts.Find(rstId)?.Menu;
            return menu;
        }
    }
}
