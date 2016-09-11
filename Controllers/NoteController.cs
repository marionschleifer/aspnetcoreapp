using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using NotesApp.Models;
using System.Threading.Tasks;

namespace NotesApp.Controllers
{
    [Route("[controller]")]
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


        // GET: Notes/Create
        // public IActionResult Create()
        // {
        //     return View();
        // }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([FromBody] Note note)
        {
            if (note == null)
            {
                return BadRequest();
            }
            Notes.Add(note);
            return CreatedAtRoute("GetById", new { id = note.Key }, note);
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

        // [HttpPut("{id}")]
        // public IActionResult Update(string id, [FromBody] Note note)
        // {
        //     if (note == null || note.Key != id)
        //     {
        //         return BadRequest();
        //     }

        //     var todo = Notes.Find(id);
        //     if (todo == null)
        //     {
        //         return NotFound();
        //     }

        //     Notes.Update(note);
        //     return View(note);
        // }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var note = Notes.Find(id);
            if (note == null)
            {
                return NotFound();
            }

            Notes.Remove(id);
            return View(note);
        }
    }    
}