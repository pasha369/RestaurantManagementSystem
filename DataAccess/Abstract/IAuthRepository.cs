using DataModel.Model;

namespace DataAccess.Abstract
{
    public interface IAuthRepository
    {
        UserInfo Login(string login, string password);

    }
}
