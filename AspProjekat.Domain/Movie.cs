using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspProjekat.Domain
{
    public class Movie:NamedEntity
    {
        public string Description { get; set; }
        public DateTime PremierDate { get; set; }
        public int DurationInMinutes { get; set; }
        public decimal BasePrice { get; set; }
        public string CoverImage { get; set; }
        public virtual ICollection<Genre> Genres { get; set; } = new HashSet<Genre>();
        public virtual ICollection<Actor> Actors { get; set; } = new HashSet<Actor>();
        public virtual ICollection<Projection> Projections { get; set; } = new HashSet<Projection>();   
    }
}
