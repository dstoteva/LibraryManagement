using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace LibraryManagement.Models
{
    public class Author
    {
        public Author()
        {
                this.Books = new HashSet<Book>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Country { get; set; }
        public virtual ICollection<Book> Books { get; set; }

    }
}
