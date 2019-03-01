using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter your name.")]
        [StringLength(255)]
        [Display(Name = "Name")]        
        public string Name { get; set; }

        [ForeignKey("Genre")]
        [Display(Name = "Genre")]
        public int GenreId { get; set; }

        public Genre Genre { get; set; }
        
        [Required]
        [Display(Name = "Release Date")]
        public DateTime? ReleaseDate { get; set; }
        
        public DateTime? DateAdded { get; set; }

        [Required]
        [Range(1, 20)]
        [Display(Name = "Number in Stock")]
        public int NumberInStock { get; set; }
    }
}