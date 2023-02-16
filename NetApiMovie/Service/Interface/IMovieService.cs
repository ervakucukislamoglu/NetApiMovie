using NetApiMovie.Models;
using NetApiMovie.Models.Enum;
using System.Collections.Generic;

namespace NetApiMovie.Service.Interface
{
    public interface IMovieService
    {
        MovieStatusCodes CreateMovie(Movie movie);
        List<Movie> GetMovies();
        Movie GetMovieById(int id);
        MovieStatusCodes DeleteMovie(int id);

        MovieStatusCodes UpdateMovie(Movie movie);

        List<Movie> SearchMovies(string value);



    }
}
