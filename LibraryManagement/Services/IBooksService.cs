using LibraryManagement.Models;

namespace LibraryManagement.Services
{
    public interface IBooksService
    {
        IEnumerable<T> GetAllBooksWithAuthors<T>();

        T GetById<T>(int id);

        Task<int> CreateAsync(Book book);

        Task DeleteAsync(int id);

        Task EditAsync(int id, string title, int? year, bool isAvailable, int authorId);
    }
}
