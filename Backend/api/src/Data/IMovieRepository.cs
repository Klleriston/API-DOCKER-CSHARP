using src.Models;

namespace src.Data
{
    public interface IMovieRepository
    {
        Movie Create(Movie movie);
        Movie GetMovieByTitle(string title);
        Movie GetMovieById(int id);
    }
}
