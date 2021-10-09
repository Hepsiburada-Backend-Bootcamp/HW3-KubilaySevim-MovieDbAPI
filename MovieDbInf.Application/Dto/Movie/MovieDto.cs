using MovieDbInf.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDbInf.Application.Dto.Movie
{
    public class MovieDto
    {
        public int Id { get; set; }
        
        [Required]
        [MinLength(3)]
        public string Title { get; set; }
        
        public int ReleaseDate { get; set; }

        public int DirectorId { get; set; }
        public int GenreId { get; set; }
        public int CountryId { get; set; }
    }
}
