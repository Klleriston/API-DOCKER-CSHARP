using src.Models;
using System.Collections.Generic;

namespace src.Data
{
    public interface IUserRepository
    {
        User Create(User user);
        User GetUserByEmail(string email);
        User GetUserById(int id);
        ICollection<User> GetAllUsers();
        void DeleteUser(int id);
    }
}
