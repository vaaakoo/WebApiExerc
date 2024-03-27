using Microsoft.EntityFrameworkCore;
using productApi3_IActionResult__T__.Models;

namespace productApi3_IActionResult__T__.Data
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }

    }
}
