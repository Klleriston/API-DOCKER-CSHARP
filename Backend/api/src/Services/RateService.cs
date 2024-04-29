using src.Data;
using src.Models;

public class RateService : IMovieRate
{
     private readonly IMovieRate _movieRate;

    public void AddRating(int userId, int movieId, int rating)
    {
        var movieRating = new Rate
        {
            UserId = userId,
            MovieId = movieId,
            Rating = rating
        };
        _movieRate.AddRating(movieRating);
    }

    public void AddRating(Rate rating)
    {
        throw new NotImplementedException();
    }

    public IList<Rate> GetAllRatings()
    {
        return _movieRate.GetAllRatings();
    }

    public IList<Rate> GetRatingsForMovie(int movieId)
    {
        return _movieRate.GetRatingsForMovie(movieId);
    }

    public IList<Rate> GetRatingsForUser(int userId)
    {
        return _movieRate.GetRatingsForUser(userId);
    }

}