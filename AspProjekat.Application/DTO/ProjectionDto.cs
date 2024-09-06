using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspProjekat.Application.DTO
{
    public class ProjectionDto
    {
        public int Id { get; set; }
        public DateTime Time {  get; set; }
        public decimal Price { get; set; }
        public MovieDto Movie { get; set; }
        public HallDto Hall { get; set; }
        public ProjectionTypeDto ProjectionType { get; set; }
        public IEnumerable<UserDto> Users { get; set; }
    }
}
