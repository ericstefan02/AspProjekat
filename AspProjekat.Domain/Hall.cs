using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspProjekat.Domain
{
    public class Hall:NamedEntity
    {
        public int Capacity { get; set; }
        public virtual ICollection<Projection> Projections { get; set; } = new HashSet<Projection>();
        
    }
}
