using System.ComponentModel.DataAnnotations;

namespace MovieCatalogAPI.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public int ReleaseYear { get; set; }

        [Required]
        public int DirectorId { get; set; }

        public Director? Director { get; set; }
    }
}