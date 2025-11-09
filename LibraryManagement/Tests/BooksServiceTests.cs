using LibraryManagement.Data;
using LibraryManagement.Models;
using LibraryManagement.Services;
using LibraryManagement.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Xunit;

namespace LibraryManagement.Tests
{
    public class BooksServiceTests
    {

        [Fact]
        public async Task CreateAsyncAddsThreeBooks()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var service = new BooksService(dbContext);



            var book1 = new Book
            {
                Id = 1,
                Title = "Title",
                Year = 2000,
                AuthorId = 1
            };

            var book2 = new Book
            {
                Id = 2,
                Title = "Title",
                Year = 2000,
                AuthorId = 1
            };

            var book3 = new Book
            {
                Id = 3,
                Title = "Title",
                Year = 2000,
                AuthorId = 1
            };

            await service.CreateAsync(book1);
            await service.CreateAsync(book2);
            await service.CreateAsync(book3);

            var expectedCount = 3;
            var actualCount = service.GetBooksCount();

            Assert.Equal(expectedCount, actualCount);
        }


        [Fact]
        public async Task DeleteAsyncDeletesOneBook()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var service = new BooksService(dbContext);



            var book1 = new Book
            {
                Id = 1,
                Title = "Title",
                Year = 2000,
                AuthorId = 1
            };

            var book2 = new Book
            {
                Id = 2,
                Title = "Title",
                Year = 2000,
                AuthorId = 1
            };

            var book3 = new Book
            {
                Id = 3,
                Title = "Title",
                Year = 2000,
                AuthorId = 1
            };

            await service.CreateAsync(book1);
            await service.CreateAsync(book2);
            await service.CreateAsync(book3);
            await service.DeleteAsync(3);

            var expectedCount = 2;
            var actualCount = service.GetBooksCount();

            Assert.Equal(expectedCount, actualCount);
        }

        [Fact]
        public async Task EditAsyncChangesTitle()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var service = new BooksService(dbContext);

            var book1 = new Book
            {
                Id = 1,
                Title = "Title",
                Year = 2000,
                IsAvailable = true,
                AuthorId = 1
            };

            await service.CreateAsync(book1);

            await service.EditAsync(book1.Id, "New Book Title", book1.Year, book1.IsAvailable, book1.AuthorId);

            var expectedTitle = "New Book Title";

            AutoMapperConfig.RegisterMappings(typeof(BookTest).Assembly);
            var actualTitle = service.GetById<BookTest>(1).Title;

            Assert.Equal(expectedTitle, actualTitle);
        }

        public class BookTest : IMapFrom<Book>
        {
            public string Title { get; set; }
        }
    }
}
