using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspProjekat.Application.DTO
{
    public class GenreDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<MovieDto> Movies { get; set; }

    }
}
