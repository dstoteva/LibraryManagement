using LibraryManagement.Data;
using LibraryManagement.Models;

namespace LibraryManagement.Services
{
    public class BooksService : IBooksService
    {
        private readonly ApplicationDbContext context;

        public BooksService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<T> GetAllBooksWithAuthors<T>(int? take = null, int skip = 0)
        {
            var allBooks = this.context.Books.OrderBy(b => b.Title).Skip(skip);

            if (take.HasValue)
            {
                allBooks = allBooks.Take(take.Value);
            }

            return allBooks.To<T>().ToList();
        }

        public T GetById<T>(int id)
        {
            var book = this.context.Books.Where(b => b.Id == id)
                .To<T>()
                .FirstOrDefault();

            return book;
        }

        public async Task<int> CreateAsync(Book book)
        {
            await this.context.Books.AddAsync(book);
            await this.context.SaveChangesAsync();

            return book.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var book = this.context.Books.Where(b => b.Id == id).FirstOrDefault();

            this.context.Books.Remove(book);
            await this.context.SaveChangesAsync();
        }

        public async Task EditAsync(int id, string title, int? year, bool isAvailable, int authorId)
        {
            var book = this.context.Books.Where(b => b.Id == id).FirstOrDefault();

            book.Title = title;
            book.Year = (int?)year ?? 0;
            book.IsAvailable = isAvailable;
            book.AuthorId = authorId;

            this.context.Books.Update(book);
            await this.context.SaveChangesAsync();
        }

        public int GetBooksCount()
        {
            return this.context.Books.Count();
        }
    }
}
