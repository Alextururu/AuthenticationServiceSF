using System.Collections.Generic;

namespace AuthenticationServiceSF
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        User GetByLogin(string login);
    }
}
