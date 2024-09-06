using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspProjekat.Application.DTO
{
    public class CreateGenreDto
    {
        public string Name { get; set; }
        public IEnumerable<int>? MovieIds { get; set; }
    }
}
