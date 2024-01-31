using BookCatalogApis.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BookCatalogApis.Data
{
    public class BookCatalogContext : DbContext
    {
        public BookCatalogContext(DbContextOptions<BookCatalogContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
