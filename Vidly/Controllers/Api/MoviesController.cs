﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class MoviesController : ApiController
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        // GET /api/movies
        public IHttpActionResult GetMovies()
        {
            var movieDtos = _context.Movies.ToList().Select(Mapper.Map<Movie, MovieDto>);

            return Ok(movieDtos);
        }

        // GET /api/movies/1       
        public IHttpActionResult GetMovie(int id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (movie == null)
            {
//                throw new HttpResponseException(HttpStatusCode.NotFound);
                return NotFound();
            }

//            return Mapper.Map<Movie, MovieDto>(movie);
            return Ok(Mapper.Map<Movie, MovieDto>(movie));
        }

        // POST /api/movies/
        [HttpPost]
        public IHttpActionResult CreateMoive(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
            {
//                throw new HttpResponseException(HttpStatusCode.BadRequest);
                return BadRequest();
            }            

            var movie = Mapper.Map<MovieDto, Movie>(movieDto);

            _context.Movies.Add(movie);

            _context.SaveChanges();

            movieDto.Id = movie.Id;

//            return movieDto;
            return Created(new Uri(Request.RequestUri + "/" + movie.Id), movieDto);
        }

        // PUT /api/movies/1
        [HttpPut]
        public IHttpActionResult UpdateMovie(int id, MovieDto movieDto)
        {
            if (!ModelState.IsValid)
            {
//                throw new HttpResponseException(HttpStatusCode.BadRequest);
                return BadRequest();
            }

            var movieInDb = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (movieInDb == null)
            {
//                throw new HttpResponseException(HttpStatusCode.NotFound);
                return NotFound();
            }

            Mapper.Map(movieDto, movieInDb);

            _context.SaveChanges();

            return Ok();
        }

        // DELETE api/movies/1
        [HttpDelete]
        public IHttpActionResult DeleteMovie(int id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (movie == null)
            {
//                throw new HttpResponseException(HttpStatusCode.NotFound);
                return NotFound();
            }

            _context.Movies.Remove(movie);

            _context.SaveChanges();

            return Ok();
        }
    }
}
