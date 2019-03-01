using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.Dtos
{
    public class MovieDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter your name.")]
        [StringLength(255)]        
        public string Name { get; set; }

        [ForeignKey("Genre")]        
        public int GenreId { get; set; }        

        [Required]        
        public DateTime? ReleaseDate { get; set; }        

        [Required]
        [Range(1, 20)]        
        public int NumberInStock { get; set; }
    }
}