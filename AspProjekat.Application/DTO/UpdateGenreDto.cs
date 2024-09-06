using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspProjekat.Application.DTO
{
    public class UpdateGenreDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool? isActive { get; set; }
        public IEnumerable<int>? MovieIds { get; set; }
    }
}
