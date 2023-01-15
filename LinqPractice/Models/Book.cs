using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LinqPractice.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public Guid AuthorId { get; set; }
        [ForeignKey("AuthorId")]
        public Author? Author { get; set; }
        public int Pages { get; set; } = 0;

    }
}
