using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using NotesApp.Models;

namespace NotesApp.Controllers
{
    [Route("api/[controller]")]
    public class NoteController : Controller
    {
        public NoteController(INoteRepository notes)
        {
            Notes = notes;
        }
        public INoteRepository Notes { get; set; }

        public IEnumerable<Note> GetAll()
        {
            return Notes.GetAll();
        }

        [HttpGet("{id}", Name = "GetNote")]
        public IActionResult GetById(string id)
        {
            var item = Notes.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Note note)
        {
            if (note == null)
        {
            return BadRequest();
        }
        Notes.Add(note);
            return CreatedAtRoute("GetNote", new { id = note.Key }, note);
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody] Note note)
        {
            if (note == null || note.Key != id)
            {
                return BadRequest();
            }

            var todo = Notes.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            Notes.Update(note);
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var note = Notes.Find(id);
            if (note == null)
            {
                return NotFound();
            }

            Notes.Remove(id);
            return new NoContentResult();   
        }
    }    
}