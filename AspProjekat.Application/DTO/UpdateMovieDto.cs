using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspProjekat.Application.DTO
{
    public class UpdateMovieDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime? PremierDate { get; set; }
        public int? DurationInMinutes { get; set; }
        public decimal? BasePrice { get; set; }
        //public string? CoverImage { get; set; }
        public IFormFile? Image {  get; set; }
        public IEnumerable<int>? ActorIds { get; set; }
        public IEnumerable<int>? GenreIds { get; set; }
        public bool? isActive { get; set; }
    }
}
