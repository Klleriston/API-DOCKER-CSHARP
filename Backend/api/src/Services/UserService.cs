using src.Models;

namespace src.Data
{
    public class UserService : IUserRepository
    {
        private readonly MovieNetContext _movieNetContext;

        public UserService(MovieNetContext movieNetContext)
        {
            _movieNetContext = movieNetContext;
        }

        public ICollection<User> GetAllUsers()
        {
            return _movieNetContext.Users.ToList();
        }

        public User Create(User user)
        {
            _movieNetContext.Users.Add(user);
            _movieNetContext.SaveChanges();
            return user;
        }

        public User GetUserByEmail(string email)
        {
            return _movieNetContext.Users.FirstOrDefault(u => u.Email == email);
        }

        public User GetUserById(int id)
        {
            return _movieNetContext.Users.FirstOrDefault(u => u.Id == id);
        }

        public void UpdateUser(User user)
        {
            _movieNetContext.Update(user);
            _movieNetContext.SaveChanges();
        }

        public void DeleteUser(int id)
        {
            var user = _movieNetContext.Users.Find(id);
            if (user != null)
            {
                _movieNetContext.Remove(user);
                _movieNetContext.SaveChanges();
            }
        }
    }
}
