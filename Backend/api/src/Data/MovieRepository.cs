using src.Models;

namespace src.Data
{
    public class MovieRepository
    {
        private readonly MovieNetContext _movieNetContext;
        public MovieRepository(MovieNetContext movieNetContext)
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
            movie.Id = _movieNetContext.SaveChanges();
        }

        public Movie GetMovieById(int id)
        {
            var movie = _movieNetContext.Movies.FirstOrDefault(m =>  m.Id == id);
            return movie;
        }
        public Movie GetMovieByTitle(string title)
        {
           var movie = _movieNetContext.Movies.FirstOrDefault(m =>m.Title == title);
            return movie;
        }

        public void UpdatedMovie(Movie movie, int id)
        {
            var updatedMovie = _movieNetContext.Movies.FirstOrDefault(m =>  m.Id == id);

            updatedMovie.Title = movie.Title;
            updatedMovie.Description = movie.Description;
            updatedMovie.Year = movie.Year;

            _movieNetContext.Update(updatedMovie);
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
