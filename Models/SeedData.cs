using Microsoft.Extensions.DependencyInjection;
using NotesApp.Models;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace NotesApp.Models
{
    public class SeedData
    {
        public static void Initalize(IServiceProvider serviceProvider)
        {
            using (var context = new NotesContext(
                serviceProvider.GetRequiredService<DbContextOptions<NotesContext>>()))
            {
                //Look for any notes.
                if (context.Notes.Any())
                {
                    return;
                }

                context.Notes.AddRange(
                    new Note
                    {
                        Title = "Pick up grandma",
                        Content = "17:25 platform 15, be on time!",
                        FinishDate = DateTime.Parse("2016-09-26 17:25"),
                        Finished = false,
                        Importance = 5
                    },
                    new Note
                    {
                        Title = "Mini project internet technologies",
                        Content = "JunitTests? Validation? DatePicker?",
                        FinishDate = DateTime.Parse("2016-10-09 00:00"),
                        Finished = false,
                        Importance = 4
                    },
                    new Note
                    {
                        Title = "Shopping",
                        Content = "Milk, bread, marmelade, salad, kiwi",
                        FinishDate = DateTime.Parse("2016-09-17 18:00"),
                        Finished = true,
                        FinishedDate = DateTime.Parse("2016-09-25 10:00"),
                        Importance = 2
                    },
                    new Note
                    {
                        Title = "Birthday present Mella",
                        Content = "IPad? Other idea?",
                        FinishDate = DateTime.Parse("2016-09-19 17:00"),
                        Finished = true,
                        FinishedDate = DateTime.Parse("2016-09-24 10:00"),
                        Importance = 3
                    }
                    );
                context.SaveChanges();
            }
        }
    }
}
