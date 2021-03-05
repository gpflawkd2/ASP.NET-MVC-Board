using AspnetNote.Models;
using Microsoft.EntityFrameworkCore;

namespace AspnetNote.Data
{
    public class AspnetNoteDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Note> Notes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost;Database=AspnetNoteDb;User Id=Board;Password=phr8611!;");
        }
    }
}
