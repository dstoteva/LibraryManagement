
using LibraryManagement.Data;
using LibraryManagement.Models;

namespace LibraryManagement.Services
{
    public class AuthorsService : IAuthorsService
    {
        private readonly ApplicationDbContext context;

        public AuthorsService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IEnumerable<T> GetAll<T>()
        {
            return this.context.Authors.To<T>().ToList();
        }

        public T GetById<T>(int id)
        {
            var book = this.context.Authors.Where(b => b.Id == id)
                .To<T>()
                .FirstOrDefault();

            return book;
        }
        public async Task<int> CreateAsync(Author author)
        {
            await this.context.Authors.AddAsync(author);
            await this.context.SaveChangesAsync();

            return author.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var author = this.context.Authors.Where(b => b.Id == id).FirstOrDefault();

            this.context.Authors.Remove(author);
            await this.context.SaveChangesAsync();
        }

        public async Task EditAsync(int id, string firstName, string lastName, DateTime dateOfBirth, string country)
        {
            var author = this.context.Authors.Where(b => b.Id == id).FirstOrDefault();

            author.FirstName = firstName;
            author.LastName = lastName;
            author.DateOfBirth = dateOfBirth;   
            author.Country = country;

            this.context.Authors.Update(author);
            await this.context.SaveChangesAsync();
        }
    }
}
