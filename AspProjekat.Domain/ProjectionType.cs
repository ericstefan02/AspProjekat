using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspProjekat.Domain
{
    public class ProjectionType:NamedEntity
    {
        public decimal Multiplier { get; set; }
        public virtual ICollection<Projection> Projections { get; set; } = new HashSet<Projection>();
    }
}
