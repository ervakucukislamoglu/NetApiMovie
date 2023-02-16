using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetApiMovie.Models;
using NetApiMovie.Models.Context;
using NetApiMovie.Service.Interface;
using System.Collections.Generic;
using System.Linq;

namespace NetApiMovie.Controllers
{


    public class MovieController : ControllerBase
    {
        private readonly MovieDbContext context;
        private readonly IMovieService movieService;

        //todo: IMovieService inject edilecek.
        public MovieController(MovieDbContext context, IMovieService movieService)
        {
            this.context = context;
            this.movieService = movieService;
        }

        //GET
        [HttpGet]
        public IActionResult GetMovies()
        {
            var movies = movieService.GetMovies();

            if (movies == null)
            {
                return NotFound("filmler bulunamadı");
            }
            else
            {
                return Ok(movies);
            }
        }


        //POST
        [HttpPost]
        public IActionResult PostMovie(Movie movie)
        {
            if (ModelState.IsValid)
            {
                var result = movieService.CreateMovie(movie);
                if(result == Models.Enum.MovieStatusCodes.Ok)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest(ModelState);
                }
                
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        //PUT
        [HttpPut]
        public IActionResult PutMovie(Movie movie)
        {
            if (ModelState.IsValid)
            {
                var result =movieService.UpdateMovie(movie);
                if(result == Models.Enum.MovieStatusCodes.Ok)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest(ModelState);
                }
                
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        //DELETE
        public IActionResult DeleteMovie(int id)
        {
            var result = movieService.DeleteMovie(id);
            if(result == Models.Enum.MovieStatusCodes.Ok)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
            

        }
    }
}
