using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using NotesApp.Models;
using System.Threading.Tasks;

namespace NotesApp.Controllers
{
    public class NoteController : Controller
    {
        public NoteController(INoteRepository notes)
        {
            Notes = notes;
        }
        public INoteRepository Notes { get; set; }

        public IActionResult Index()
        {
            return View(Notes.GetAll());
        }

        [HttpGet("{id}", Name = "GetNote")]
        public IActionResult Details(string id)
        {
            var item = Notes.Find(id);
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
        public IActionResult Create(Note note)
        {
            if (ModelState.IsValid)
            {
                Notes.Add(note);
                return RedirectToAction("Index");
            }
            return View(note);
        }

        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var note = Notes.Find(id);
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
                Notes.Update(note);
                return RedirectToAction("Index");
            }
            return View(note); 
        }

        [ActionName("Delete")]
        public IActionResult Delete(string id)
        {
            var note = Notes.Find(id);
            if (note == null)
            {
                return NotFound();
            }

            Notes.Remove(id);
            return RedirectToAction("Index");
        }
    }    
}