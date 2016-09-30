using NotesApp.Models;
using System;
using System.Collections.Generic;

namespace NotesApp.Services
{
    public interface INotesService
    {
        List<Note> GetSortedList(List<Note> list, string filter, bool hideFinished);
    }
}
    
