using Microsoft.EntityFrameworkCore;
using NotesApp.Models;
 
namespace NotesApp.Models
{
    public class NotesContext : DbContext
    {
        public NotesContext(DbContextOptions<NotesContext> options)
            : base(options)
        {
        }
 
        public DbSet<Note> Notes { get; set; }

    }
}