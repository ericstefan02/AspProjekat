using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspProjekat.Application.DTO
{
    public class CreateProjectionDto
    {
        public DateTime Time {  get; set; }
        public int MovieId { get; set; }
        public int HallId { get; set; }
        public int ProjectionTypeId { get; set; }
        public IEnumerable<int>? UserIds { get; set; }

    }
}
