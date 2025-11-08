using LibraryManagement.Models;
using LibraryManagement.Services;

namespace LibraryManagement.ViewModels
{
    public class AuthorViewModel : IMapFrom<Author>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime DateOfBirthDateOnly => this.DateOfBirth.Date;
        public string Country { get; set; }
        public IEnumerable<BookViewModel> Books { get; set; }
        public string FullName => string.Join(" ", new[] { this.FirstName, this.LastName }.Where(n => !string.IsNullOrEmpty(n)));
    }
}
