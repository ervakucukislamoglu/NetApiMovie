using Microsoft.EntityFrameworkCore;
using NetApiMovie.Models;
using NetApiMovie.Models.Context;
using NetApiMovie.Models.Enum;
using NetApiMovie.Service.Interface;
using System.Collections.Generic;
using System.Linq;

namespace NetApiMovie.Service.Concrete
{
    public class MovieService : IMovieService
    {
        private readonly MovieDbContext context;


        public MovieService(MovieDbContext context)
        {
            this.context = context;
        }

        public MovieStatusCodes CreateMovie(Movie movie)
        {
            if (movie != null)
            {
                context.Movies.Add(movie);
                return MovieStatusCodes.Ok;
            }
            else
            {
                return MovieStatusCodes.BadRequest;
            }

        }

        public MovieStatusCodes DeleteMovie(int id)
        {
            var deleted = context.Movies.Find(id);
            if (deleted != null)
            {
                context.Movies.Remove(deleted);
                context.SaveChanges();
                return MovieStatusCodes.Ok;
            }
            else
            {
                return MovieStatusCodes.NotFound;
            }
        }

        public Movie GetMovieById(int id)
        {

            var founded = context.Movies.FirstOrDefault(x => x.Id == id);
            if (founded != null)
            {
                return founded;
            }
            else
            {
                return null; //Hocam buranın logic'ine müdahale etmeniz gerekebilir. Else senaryosunu kurgulayamadım.
            }


        }

        public List<Movie> GetMovies()
        {

            return context.Movies.ToList();


        }

        public List<Movie> SearchMovies(string value)
        {

            var searchedMovies = context.Movies.Where(x => x.Title == value).ToList();
            return searchedMovies;
        }

        public MovieStatusCodes UpdateMovie(Movie movie)
        {
            var updated = context.Movies.FirstOrDefault(x => x.Id == movie.Id);
            if (updated == null)
            {
                return MovieStatusCodes.NotFound;
            }
            else
            {
                //todo: context.entry(movie).state işlemi hata veriyor. araştırın.
                updated.Title = movie.Title;
                updated.Description = movie.Description;
                updated.Rank = movie.Rank;
                updated.Genre = movie.Genre;
                if (context.SaveChanges() > 0)
                {
                    return MovieStatusCodes.Ok;
                }
                else
                {
                    return MovieStatusCodes.BadRequest;
                }
            }
        }
    }
}
