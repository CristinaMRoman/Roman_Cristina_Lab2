using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Roman_Cristina_Lab2.Data;
using Roman_Cristina_Lab2.Models;

namespace Roman_Cristina_Lab2.Pages.Books
{
    public class CreateModel : BookCategoriesPageModel
    {
        private readonly Roman_Cristina_Lab2Context _context;

        public CreateModel(Roman_Cristina_Lab2Context context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        

            {
            var authors = _context.Author.Select(a => new { a.ID, a.AuthorName }).ToList();

            foreach (var author in authors)
            {
                Console.WriteLine($"Author ID: {author.ID}, AuthorName: {author.AuthorName}");
            }

            ViewData["PublisherID"] = new SelectList(_context.Set<Publisher>(), "ID", "PublisherName");

            
            ViewData["AuthorID"] = new SelectList(_context.Set<Author>(), "ID", "AuthorName");

            var book = new Book();
            book.BookCategories = new List<BookCategory>();
            PopulateAssignedCategoryData(_context, book);

            return Page();
        }

        [BindProperty]
        public Book Book { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync(string[] selectedCategories)
        {
            if (!ModelState.IsValid)
            {
                PopulateAssignedCategoryData(_context, Book);
                return Page();
            }

            var newBook = new Book();

            if (selectedCategories != null)
            {
                newBook.BookCategories = new List<BookCategory>();
                foreach (var cat in selectedCategories)
                {
                    var catToAdd = new BookCategory
                    {
                        CategoryID = int.Parse(cat)
                    };
                    newBook.BookCategories.Add(catToAdd);
                }
            }

            Book.BookCategories = newBook.BookCategories;
            _context.Book.Add(Book);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Books/Index");
        }
    }
}

