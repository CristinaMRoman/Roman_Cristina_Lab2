using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Roman_Cristina_Lab2.Models;

namespace Roman_Cristina_Lab2.Data
{
    public class Roman_Cristina_Lab2Context : DbContext
    {
        public Roman_Cristina_Lab2Context (DbContextOptions<Roman_Cristina_Lab2Context> options)
            : base(options)
        {
        }

        public DbSet<Roman_Cristina_Lab2.Models.Book> Book { get; set; } = default!;
        public DbSet<Roman_Cristina_Lab2.Models.Publisher> Publisher { get; set; } = default!;
        public DbSet<Roman_Cristina_Lab2.Models.Author> Author { get; set; } = default!;
        public DbSet<Roman_Cristina_Lab2.Models.Category> Category { get; set; } = default!;
    }
}
