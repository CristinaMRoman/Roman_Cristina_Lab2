﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Roman_Cristina_Lab2.Data;
using Roman_Cristina_Lab2.Models;
using System.Threading.Tasks;

namespace Roman_Cristina_Lab2.Pages.Books
{
    public class EditModel : BookCategoriesPageModel
    {
        private readonly Roman_Cristina_Lab2Context _context;

        public EditModel(Roman_Cristina_Lab2Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Book Book { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Book = await _context.Book
                .Include(b => b.Publisher)
                .Include(b => b.Author)
                .Include(b => b.BookCategories).ThenInclude(b => b.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if (Book == null)
            {
                return NotFound();
            }

            PopulateAssignedCategoryData(_context, Book);
            ViewData["PublisherID"] = new SelectList(_context.Set<Publisher>(), "ID", "PublisherName");

            
            ViewData["AuthorID"] = new SelectList(_context.Set<Author>(), "ID", "AuthorName");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id, string[] selectedCategories)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookToUpdate = await _context.Book
                .Include(i => i.Publisher)
                .Include(b => b.Author)
                .Include(i => i.BookCategories)
                .ThenInclude(i => i.Category)
                .FirstOrDefaultAsync(s => s.ID == id);

            if (bookToUpdate == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                PopulateAssignedCategoryData(_context, bookToUpdate);
                return Page();
            }

            if (await TryUpdateModelAsync<Book>(
                bookToUpdate,
                "Book",
                i => i.Title, i => i.Author, i => i.Price, i => i.PublishingDate, i => i.PublisherID))
            {
                UpdateBookCategories(_context, selectedCategories, bookToUpdate);
                await _context.SaveChangesAsync();
                return RedirectToPage("/Books/Index");
            }

            // Reapelăm PopulateAssignedCategoryData pentru a actualiza categoriile
            PopulateAssignedCategoryData(_context, bookToUpdate);
            return Page();
        }

        private bool BookExists(int id)
        {
            return _context.Book.Any(e => e.ID == id);
        }
    }
}

