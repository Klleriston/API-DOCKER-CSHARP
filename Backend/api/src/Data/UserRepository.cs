using src.Models;

namespace src.Data
{
    public class UserRepository
    {
        private readonly MovieNetContext _movieNetContext;
        public UserRepository(MovieNetContext movieNetContext)
        {
            _movieNetContext = movieNetContext;
        }
        public ICollection<User> GetAllUsers() 
        {
            return _movieNetContext.Users.ToList();
        }
        public void Create(User user)
        {
            _movieNetContext.Users.Add(user);
            user.Id = _movieNetContext.SaveChanges();
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

        public void UpdatedUser(User user, int id) 
        {
            var updatedUser = _movieNetContext.Users.FirstOrDefault(u => u.Id == id);

            updatedUser.Name = user.Name;
            updatedUser.Password = user.Password;   
            updatedUser.Photo = user.Photo;
            updatedUser.Email = user.Email;
            updatedUser.PhoneNumber = user.PhoneNumber;

            _movieNetContext.Update(updatedUser);
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
