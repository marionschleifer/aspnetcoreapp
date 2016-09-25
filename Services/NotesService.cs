using NotesApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NotesApp.Services;

namespace NotesApp.Services
{    public class NotesService : INotesService
    {
        private string orderFinish="desc";
        private string orderCreate="desc";
        private string orderImportance="desc";
        public List<Note> GetSortedList(List<Note> notes, string sort, bool hide)
        {
            switch (sort)
            {
                case "finishDate":
                switch (orderFinish)
                {
                    case "desc":
                        notes = notes.OrderBy(n => n.FinishDate).ToList();
                        orderFinish="asc";
                        break;
                    case "asc":
                        notes = notes.OrderByDescending(n => n.FinishDate).ToList();
                        orderFinish="desc";
                        break;
                }
                    break;
                case "createDate":
                switch (orderCreate)
                {
                    case "desc":
                        notes = notes.OrderBy(n => n.CreateDate).ToList();
                        orderCreate="asc";
                        break;
                    case "asc":
                        notes = notes.OrderByDescending(n => n.CreateDate).ToList();
                        orderCreate="desc";
                        break;
                }                
                    break;
                case "importance":
                switch (orderImportance)
                {
                    case "desc":
                        notes = notes.OrderBy(n => n.Importance).ToList();
                        orderImportance="asc";
                        break;
                    case "asc":
                        notes = notes.OrderByDescending(n => n.Importance).ToList();
                        orderImportance="desc";
                        break;
                }                    
                    break;
                default:
                    notes = notes.OrderBy(n => n.CreateDate).ToList();
                    break;
            }
            if (hide)
            {
                notes = notes.Where(s => s.Finished.Equals(false)).ToList();
            }
            return notes;
        }

    }
}