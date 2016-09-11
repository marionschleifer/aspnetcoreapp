using System.Collections.Generic;

namespace NotesApp.Models
{
    public interface INoteRepository
    {
        void Add(Note item);
        IEnumerable<Note> GetAll();
        Note Find(string key);
        Note Remove(string key);
        void Update(Note item);
    }
}