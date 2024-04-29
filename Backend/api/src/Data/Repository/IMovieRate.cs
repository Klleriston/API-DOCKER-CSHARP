using src.Models;
using System.Collections.Generic;

namespace src.Data
{
    public interface IMovieRate
    {
        void AddRating(Rate rating);
        IList<Rate> GetRatingsForUser(int userId);
        IList<Rate> GetRatingsForMovie(int movieId);
        IList<Rate> GetAllRatings();
    }
}
