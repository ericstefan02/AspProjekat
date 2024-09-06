using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspProjekat.Application.DTO
{
    public class AuditLogSearch : PagedSearch
    {
        public string? UseCaseName { get; set; }
        public string? Username { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To {  get; set; }
        
    }
}
