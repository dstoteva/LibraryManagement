using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Models
{
    public class Book
    {
        public Book()
        {

        }

        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public int Year { get; set; }
        public bool IsAvailable { get; set; } = true;
        public int AuthorId { get; set; }
        public virtual Author Author { get; set; }
    }
}
