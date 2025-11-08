using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryManagement.Data;
using LibraryManagement.Models;
using LibraryManagement.Services;
using LibraryManagement.ViewModels;

namespace LibraryManagement.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBooksService booksService;
        private readonly IAuthorsService authorsService;

        public BooksController(IBooksService booksService, IAuthorsService authorsService)
        {
            this.booksService = booksService;
            this.authorsService = authorsService;
        }

        // GET: Books
        public IActionResult Index()
        {
            return View(this.booksService.GetAllBooksWithAuthors<BookViewModel>().ToList());
        }

        // GET: Books/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = this.booksService
                .GetById<BookViewModel>((int)id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            ViewData["AuthorId"] = new SelectList(authorsService.GetAll<AuthorViewModel>(), "Id", "FullName");
            return View();
        }

        // POST: Books/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Year,IsAvailable,AuthorId")] Book book)
        {
            if (ModelState.IsValid)
            {
                await this.booksService.CreateAsync(book);
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorId"] = new SelectList(this.authorsService.GetAll<AuthorViewModel>(), "Id", "FullName", book.AuthorId);
            return View(book);
        }

        // GET: Books/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = this.booksService.GetById<BookViewModel>((int)id);
            if (book == null)
            {
                return NotFound();
            }
            ViewData["AuthorId"] = new SelectList(this.authorsService.GetAll<AuthorViewModel>(), "Id", "FullName", book.AuthorId);
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Year,IsAvailable,AuthorId")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await this.booksService.EditAsync(id, book.Title, book.Year, book.IsAvailable, book.AuthorId);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorId"] = new SelectList(this.authorsService.GetAll<AuthorViewModel>(), "Id", "FullName", book.AuthorId);
            return View(book);
        }

        // GET: Books/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = this.booksService.GetById<BookViewModel>((int)id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = this.booksService.GetById<BookViewModel>(id);
            if (book != null)
            {
                await this.booksService.DeleteAsync(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return this.booksService.GetAllBooksWithAuthors<BookViewModel>().ToList().Any(e => e.Id == id);
        }
    }
}
