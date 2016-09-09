using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotesApp.Models
{
    public class Note
    {
        public string Key { get; set; }
        [NotMapped]
        public DateTime CreateDate { get; set; }
        
        public DateTime FinishDate { get; set; }

        public bool Finished { get; set; }

        public int Importance { get; set; }

        public String Content { get; set; }

        public Note()
        {
            CreateDate = DateTime.Now;
        }
    }
}
