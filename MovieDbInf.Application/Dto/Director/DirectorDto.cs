using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDbInf.Application.Dto.Director
{
    public class DirectorDto
    {
        public int Id { get; set; }
        
        [Required, StringLength(3)]
        public string First_name { get; set; }
        public string Last_name { get; set; }
        public int CountryId { get; set; }

    }
}
