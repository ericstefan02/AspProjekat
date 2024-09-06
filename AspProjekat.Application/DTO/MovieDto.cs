using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspProjekat.Application.DTO
{
    public class MovieDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime PremierDate { get; set; }
        public int DurationInMinutes { get; set; }
        public decimal BasePrice { get; set; }
        public string CoverImage { get; set; }
        public IEnumerable<ActorDto> Actors { get; set; }
        public IEnumerable<GenreDto> Genres { get; set; }
    }
}
