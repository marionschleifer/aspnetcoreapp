using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using NotesApp.Models;
using NotesApp.Services;
using System.Threading.Tasks;

namespace NotesApp.Controllers
{
    public class NoteController : Controller
    {
        private readonly NotesContext _context;
        private readonly INotesService _notesService;

        private string SessionSelectedFilter = "SelectedFilter";
        private string SessionHideFinished = "HideFinished";

        public NoteController(NotesContext context, INotesService notesService)
        {
            _context = context;
            _notesService = notesService;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.Get(SessionSelectedFilter) == null)
            {
                HttpContext.Session.SetString(SessionSelectedFilter, "Default");
            }

            string sort = HttpContext.Session.GetString(SessionSelectedFilter);
            bool hide = Convert.ToBoolean(HttpContext.Session.GetInt32(SessionHideFinished));

            ViewBag.filterFinishedParm = hide == true ? false : true;

            List<Note> notes = _notesService.GetSortedList(_context.Notes.ToList(), sort, hide);
            return View(notes);
        }

        [HttpGet("{id}", Name = "GetNote")]
        public IActionResult Details(long? id)
        {
            var item = _context.Notes.SingleOrDefault(m => m.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }


        // // GET: Notes/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("ID,Title,Content,FinishDate,Finished,Importance")] Note note)
        {
            if (ModelState.IsValid)
            {
                if(note.Finished){
                    note.FinishedDate=DateTime.Now;
                }
                _context.Add(note);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return BadRequest();
        }

        public IActionResult Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var note = _context.Notes.SingleOrDefault(m => m.Id == id);
            if (note == null)
            {
                return NotFound();
            }
            return View(note);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Note note)
        {
            if (ModelState.IsValid)
            {
                bool orgNoteFinish = _context.Notes.Where(m => m.Id == note.Id).Select(m => m.Finished).FirstOrDefault();
                if(!orgNoteFinish && note.Finished){
                    note.FinishedDate=DateTime.Now;
                }
                _context.Update(note);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return BadRequest(); 
        }

        [ActionName("Delete")]
        public IActionResult Delete(long? id)
        {
            var note = _context.Notes.SingleOrDefault(m => m.Id == id);
            if (note == null)
            {
                return NotFound();
            }

            _context.Remove(note);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

                // GET: Note/SortBy
        [HttpGet]
        public IActionResult SortBy(string sort)
        {
            HttpContext.Session.SetString(SessionSelectedFilter, sort);
            return RedirectToAction("Index");
        }

        // GET: Note/ShowOnlyFinished
        [HttpGet]
        public IActionResult ShowOnlyFinished()
        {
            bool showOnlyFinished = Convert.ToBoolean(HttpContext.Session.GetInt32(SessionHideFinished));
            HttpContext.Session.SetInt32(SessionHideFinished, Convert.ToInt32(!showOnlyFinished));
            return RedirectToAction("Index");
        }
    }    
}