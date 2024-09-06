using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspProjekat.Application.DTO
{
    public class UpdateProjectionTypeDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal? Multiplier { get; set; }
        public bool? IsActive { get; set; }
    }
}
