namespace DataAccess.Abstract.Menu
{
    public interface IMenuManager
    {
        DataModel.Model.Menu GetByRestaurant(int rstId);
    }
}
