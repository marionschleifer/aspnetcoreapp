using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using NotesApp.Models;
using System.Threading.Tasks;

namespace NotesApp.Controllers
{
    public class NoteController : Controller
    {
        private readonly NotesContext _context;
        public NoteController(NotesContext context)
        {
           // Notes = notes;
            _context = context;
        }
        //public INoteRepository Notes { get; set; }

        public IActionResult Index(string sort, string hide)
        {
            ViewBag.finishedDateSortParm = String.IsNullOrEmpty(sort) ? "finishedDate_desc" : "";
            ViewBag.createDateSortParm = sort == "createDate" ? "createDate_desc" : "createDate";
            ViewBag.importanceSortParm = sort == "importance" ? "importance_desc" : "importance";

            ViewBag.filterFinishedParm = String.IsNullOrEmpty(hide) ? "filterFinishOff" : (hide == "filterFinishOn" ? "filterFinishOff" : "filterFinishOn");
            var notes = from n in _context.Notes
                        select n;
            switch (sort)
            {
                case "finishedDate_desc":
                    notes = notes.OrderByDescending(n => n.FinishDate);
                    break;
                case "createDate":
                    notes = notes.OrderBy(n => n.CreateDate);
                    break;
                case "createDate_desc":
                    notes = notes.OrderByDescending(n => n.CreateDate);
                    break;
                case "importance":
                    notes = notes.OrderBy(n => n.Importance);
                    break;
                case "importance_desc":
                    notes = notes.OrderByDescending(n => n.Importance);
                    break;
                default:
                    notes = notes.OrderBy(n => n.FinishDate);
                    break;
            }
            if (sort== "filterFinishOn")
            {
                notes = notes.Where(s => s.Finished.Equals(false));
            }
            return View(notes.ToList());
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
    }    
}