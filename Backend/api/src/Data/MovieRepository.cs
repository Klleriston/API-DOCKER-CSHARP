using src.Models;

namespace src.Data
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MovieNetContext _movieNetContext;
        public MovieRepository(MovieNetContext movieNetContext)
        {
            _movieNetContext = movieNetContext;
        }
        public Movie Create(Movie movie)
        {
           _movieNetContext.Movies.Add(movie);
            movie.Id = _movieNetContext.SaveChanges();
            return movie;
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
    }
}
