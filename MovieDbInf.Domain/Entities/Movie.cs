using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDbInf.Domain.Entities
{
    public class Movie
    {
        public int Id { get; set; }

        //[MinLength(2)]
        public string Title { get; set; }

        //Between years : 1900-2099
        //[RegularExpression("^(19|20)[0-9]{2}")]
        public int ReleaseDate { get; set; }

        
        public int DirectorId { get; set; }
        public int GenreId { get; set; }
        public int CountryId { get; set; }
        
        [ForeignKey("GenreId")]
        public virtual Genre Genre { get; set; }

        [ForeignKey("DirectorId")]
        public virtual Director Director { get; set; }

        [ForeignKey("CountryId")]
        public virtual Country Country { get; set; }

    }
}
