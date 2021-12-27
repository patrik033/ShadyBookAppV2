using ShadyBookAppV2.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShadyBookAppV2
{
    public class Book
    {
        public ulong Id { get; set; } //ISBN

        
        public int AuthorsId { get; set; }

        [Required]
        public string Title { get; set; }

        [Column(TypeName ="money")]
        public decimal Price { get; set; }
        
        public DateTime ReleaseDate { get; set; } = DateTime.UtcNow;
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
        public List<Author> Authors { get; set; }
        public List<Stock> Stocks { get; set; }
    }
}
