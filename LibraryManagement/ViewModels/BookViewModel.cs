using LibraryManagement.Models;
using LibraryManagement.Services;

namespace LibraryManagement.ViewModels
{
    public class BookViewModel :IMapFrom<Book>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public bool IsAvailable { get; set; } = true;
        public int AuthorId { get; set; }
        public AuthorViewModel Author { get; set; }
    }
}
