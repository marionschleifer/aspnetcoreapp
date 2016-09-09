using System;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace NotesApp.Models
{
    public class NoteRepository : INoteRepository
    {
        private static ConcurrentDictionary<string, Note> _notes =
              new ConcurrentDictionary<string, Note>();

        public NoteRepository()
        {
            Add(new Note { Content = "Bla" });
        }

        public IEnumerable<Note> GetAll()
        {
            return _notes.Values;
        }

        public void Add(Note item)
        {
            item.Key = Guid.NewGuid().ToString();
            _notes[item.Key] = item;
        }

        public Note Find(string key)
        {
            Note item;
            _notes.TryGetValue(key, out item);
            return item;
        }

        public Note Remove(string key)
        {
            Note item;
            _notes.TryRemove(key, out item);
            return item;
        }

        public void Update(Note item)
        {
            _notes[item.Key] = item;
        }
    }
}
