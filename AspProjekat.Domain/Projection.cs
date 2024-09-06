using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspProjekat.Domain
{
    public class Projection:Entity
    {
        public int MovieId { get; set; }
        public int HallId { get; set; }

        public int TypeId { get; set; }
        public DateTime Time { get; set; }
        public double Price { get; set; }

        public virtual ICollection<User> Users { get; set; } = new HashSet<User>();
        public virtual Movie Movie { get; set; }
        public virtual Hall Hall { get; set; }
        public virtual ProjectionType Type { get; set; }
    }
}
