using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Abstract;
using DataAccess.Abstract.Menu;
using DataModel.Contexts;

namespace DataAccess.Concrete.Menu
{
    public class MenuManager : IMenuManager
    {
        private RestorauntDbContext _ctx = new ContextManager().Context;

        public DataModel.Model.Menu GetByRestaurant(int rstId)
        {
            return _ctx.Restoraunts.Find(rstId).Menu;
        }
    }
}
