using System.ComponentModel.DataAnnotations;

namespace MovieCatalogAPI.Models
{
    public class Director
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}