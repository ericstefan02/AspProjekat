using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspProjekat.Application.DTO
{
    public class MovieSearch:PagedSearch
    {
       public string? MovieTitle {  get; set; }
       public IEnumerable<int>? GenreIds { get; set; }
       public IEnumerable<int>?ActorIds { get; set; }
       public int? DurationInMinutes { get; set; }
    }
}
