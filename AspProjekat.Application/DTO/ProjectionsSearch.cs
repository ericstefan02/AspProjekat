using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspProjekat.Application.DTO
{
    public class ProjectionsSearch:PagedSearch
    {
        public decimal? MaxPrice { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public int? MovieId {  get; set; }
        public int? TypeId { get; set; }
    }
}
