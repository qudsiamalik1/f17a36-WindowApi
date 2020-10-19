using Microsoft.EntityFrameworkCore;

namespace WindowApi.Models
{
    public class WindowContext : DbContext
    {
        public WindowContext(DbContextOptions<WindowContext> options)
            : base(options)
        {
        }

        public DbSet<Window> TodoItems { get; set; }
    }
}