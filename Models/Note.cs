using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotesApp.Models
{
    public class Note
    {
        public string Key { get; set; }
        [NotMapped]
        public DateTime CreateDate { get; set; }
        
        [DataType(DataType.DateTime)]
        public DateTime FinishDate { get; set; }

        public bool Finished { get; set; }

        [Required]
        [Range(0, 5)]
        public int Importance { get; set; }

        [Required]
        public String Title { get; set; }

        public String Content { get; set; }

        public Note()
        {
            CreateDate = DateTime.Now;
        }
    }
}
