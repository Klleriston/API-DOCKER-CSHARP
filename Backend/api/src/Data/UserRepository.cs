using src.Models;

namespace src.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly MovieNetContext _movieNetContext;
        public UserRepository(MovieNetContext movieNetContext)
        {
            _movieNetContext = movieNetContext;
        }
        public User Create(User user)
        {
            _movieNetContext.Users.Add(user);
            user.Id = _movieNetContext.SaveChanges();
            return user;
        }

        public User GetUserByEmail(string email)
        {
            var user = _movieNetContext.Users.FirstOrDefault(u =>  u.Email == email);
            return user;
        }

        public User GetUserById(int id)
        {
           var user = _movieNetContext.Users.FirstOrDefault(u => u.Id == id);
            return user;
        }
    }
}
