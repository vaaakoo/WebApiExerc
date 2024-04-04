using Microsoft.EntityFrameworkCore;
using todoApp.Models;

namespace todoApp.Data
{
    public class dataContext : DbContext
    {
        public dataContext(DbContextOptions<dataContext> options) : base(options) { }
    
        public DbSet<TodoText> todoTexts { get; set; }
}
}
