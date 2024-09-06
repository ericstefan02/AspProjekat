using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspProjekat.Domain
{
    public class Actor : Entity
    {
        public string FirstName {  get; set; }
        public string LastName { get; set; }
        public virtual ICollection<Movie> Movies { get; set; } = new HashSet<Movie>();
    }
}
