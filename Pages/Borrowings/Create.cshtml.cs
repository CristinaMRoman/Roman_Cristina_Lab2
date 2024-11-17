using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Roman_Cristina_Lab2.Data;
using Roman_Cristina_Lab2.Models;

namespace Roman_Cristina_Lab2.Pages.Borrowings
{
    public class CreateModel : PageModel
    {
        private readonly Roman_Cristina_Lab2.Data.Roman_Cristina_Lab2Context _context;

        public CreateModel(Roman_Cristina_Lab2.Data.Roman_Cristina_Lab2Context context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            var book = _context.Book
               .Include(b => b.Author)
               .Select(x => new
               {
                   ID = x.ID,
                   FullName = x.Title + " - " + x.Author.AuthorName,
               });
            ViewData["BookID"] = new SelectList(_context.Book, "ID", "FullName");
            ViewData["MemberID"] = new SelectList(_context.Member, "ID", "FullName");
            return Page();
        }

        [BindProperty]
        public Borrowing Borrowing { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Borrowing.Add(Borrowing);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
