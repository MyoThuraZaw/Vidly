using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Movies/Random
        public ActionResult Random()
        {            
            var movie = new Movie() { Name = "Shrek!"};

            var customers = new List<Customer>()
            {
                new Customer() { Name = "Customer 1"},
                new Customer() { Name = "Customer 2"}
            };

            var model = new RandomMovieViewModel();

            model.Movie = movie;
            model.Customers = customers;

            return View(model);
            //            ViewData["Movie"] = movie;
            //            ViewBag.RandomMovie = movie;

            //            var viewResult = new ViewResult();
            //            viewResult.ViewData.Model


            //            return Content("Hello World");
            //            return HttpNotFound();
            //            return new EmptyResult();
            //            return RedirectToAction("Index", "Home", new { page = 1, sortBy = "Name"});
        }

//        public ActionResult Edit(int id)
//        {
//            return Content("id = " + id);
//        }

        public ActionResult Index()
        {
//            List<Movie> movies = GetMovies().ToList();
            var movies = _context.Movies.Include(m => m.Genre).ToList();
            return View(movies);
        }

        public ActionResult Details(int id)
        {
            var movie = _context.Movies.Include(m => m.Genre).FirstOrDefault(m => m.Id == id);
//            var movie = GetMovies().SingleOrDefault(m => m.Id == id);
            
            if(movie == null)
            {
                return HttpNotFound();
            }

            return View(movie);
        }

        public ActionResult New()
        {
            var genres = _context.Genres.ToList();

            var viewModel = new MovieFormViewModel()
            {
                Genres = genres
            };

            return View("MovieForm",viewModel);
        }

        [HttpPost]
        public ActionResult Save(Movie movie)
        {
            if (movie.Id == 0)
            {
                _context.Movies.Add(movie);
            }
            else
            {
                var movieInDb = _context.Movies.Single(m => m.Id == movie.Id);

                movieInDb.Name = movie.Name;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.NumberInStock = movie.NumberInStock;
            }
            
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (movie == null)
            {
                return HttpNotFound();
            }

            var viewModel = new MovieFormViewModel()
            {
                Movie = movie,
                Genres = _context.Genres.ToList()
//                IsEdit = true
            };

            return View("MovieForm", viewModel);
        }

        //        public IEnumerable<Movie> GetMovies()
        //        {
        //            return new List<Movie>()
        //            {
        //                new Movie() { Id = 1, Name = "Shrek"},
        //                new Movie() { Id = 2, Name = "Wall-e"}
        //            };
        //        }

        // movies/
        //        public ActionResult Index(int? pageIndex, string sortBy)
        //        {
        //            if (!pageIndex.HasValue)
        //            {
        //                pageIndex = 1;
        //            }
        //
        //            if (String.IsNullOrWhiteSpace(sortBy))
        //            {
        //                sortBy = "Name";
        //            }
        //
        //            return Content(String.Format("pageIndex={0} & sortBy={1}", pageIndex, sortBy));
        //        }

        // Attribute Routes with constraints      
        [Route("movies/released/{year:regex(\\d{4})}/{month:regex(\\d{2}):range(1,12)}")]  
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content(year + "/" + month);
        }

        
    }
}