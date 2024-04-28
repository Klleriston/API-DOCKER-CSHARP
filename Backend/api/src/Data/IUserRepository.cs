using src.Models;

namespace src.Data
{
    public interface IUserRepository
    {
        User Create(User user);
        User GetUserByEmail(string email);
        User GetUserById(int id);
    }
}
