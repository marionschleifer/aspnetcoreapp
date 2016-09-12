using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NotesApp.Models;
using Microsoft.EntityFrameworkCore;

namespace NotesApp
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
            services.AddSingleton<INoteRepository, NoteRepository>();
            services.AddRouting();
            services.AddDbContext<NotesContext>(opt => opt.UseInMemoryDatabase());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            var context = app.ApplicationServices.GetService<NotesContext>();
            AddTestData(context);


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Note}/{action=Index}/{id?}");
            });
        }

        private static void AddTestData(NotesContext context)
        {
            var testNote1 = new DbModels.Note
            {
                Title = "Call mum",
                Content = "Call mum on number 12345",
                Importance = 1
            };
        
            context.Notes.Add(testNote1);
        
            var testNote2 = new DbModels.Note
            {
                Title = "Book flight",
                Content = "Book flight on skyscanner.com",
                Importance = 5
            };
        
            context.Notes.Add(testNote2);
        
            context.SaveChanges();
        }
    }
}
