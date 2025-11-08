using LibraryManagement.Models;

namespace LibraryManagement.Services
{
    public interface IAuthorsService
    {
        IEnumerable<T> GetAll<T>();

        T GetById<T>(int id);

        Task<int> CreateAsync(Author author);

        Task DeleteAsync(int id);

        Task EditAsync(int id, string firstName, string lastName, DateTime dateOfBirth, string country);
    }
}
