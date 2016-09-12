using Microsoft.EntityFrameworkCore;
using NotesApp.DbModels;
 
namespace NotesApp
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