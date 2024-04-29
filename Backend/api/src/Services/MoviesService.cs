using src.Models;

namespace src.Data
{
    public class MovieService
    {
        private readonly MovieNetContext _movieNetContext;

        public MovieService(MovieNetContext movieNetContext)
        {
            _movieNetContext = movieNetContext;
        }

        public ICollection<Movie> GetAllMovies()
        {
            return _movieNetContext.Movies.ToList();
        }

        public void Create(Movie movie)
        {
            _movieNetContext.Movies.Add(movie);
            _movieNetContext.SaveChanges();
        }

        public Movie GetMovieById(int id)
        {
            return _movieNetContext.Movies.FirstOrDefault(m => m.Id == id);
        }

        public void UpdateMovie(Movie movie)
        {
            _movieNetContext.Update(movie);
            _movieNetContext.SaveChanges();
        }

        public void DeleteMovie(int id)
        {
            var movie = _movieNetContext.Movies.Find(id);
            if (movie != null)
            {
                _movieNetContext.Remove(movie);
                _movieNetContext.SaveChanges();
            }
        }
    }
}
