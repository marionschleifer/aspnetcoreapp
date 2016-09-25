using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotesApp.Models
{
    public class Note
    {
        public long Id { get; set; }

        [NotMapped]
        public DateTime CreateDate { get; set; }
        
        [DataType(DataType.DateTime)]
        public DateTime? FinishDate { get; set; }

        public bool Finished { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime FinishedDate { get; set; }

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

        public string CompareDate(DateTime date){
            DateTime now = DateTime.Now;

            if(date.Year==now.Year){
                if(date.DayOfYear==now.AddDays(1).DayOfYear){
                    return "Tomorrow";
                }else if(date.DayOfYear==now.AddDays(-1).DayOfYear){
                    return "Yesterday";
                }else if(date.DayOfYear==now.DayOfYear){
                    return "Today";
                }
            }
            return date.ToString("MM/dd/yyyy");
        }

                public string CompareDate(DateTime? date){
            if(date.HasValue){
                return CompareDate(date.Value);
            } else{
                return "Anytime";
            }
        }
    }
}
