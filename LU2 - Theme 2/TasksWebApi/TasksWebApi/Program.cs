using Microsoft.EntityFrameworkCore;
using TasksWebApi.Data;

namespace TasksWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddSwaggerGen();

            //we have to add DBContext so that we can link our DB
            builder.Services.AddDbContext<DataContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // sessions | 26/08/2026
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(2); 
                //user info will be deleted after 2 minutes of no activity on the page
                options.Cookie.HttpOnly = true;
                //cookies can only be read by our server (the browser can still see them (as shown in class))
                options.Cookie.IsEssential = true;
                //cookies are mandatory. No cookies, no site 
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.

            if (app.Environment.IsDevelopment()) {

                app.UseSwagger();
                app.UseSwaggerUI();

                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Task List");

                    options.RoutePrefix = "swagger";  
                    /*this is for the broswer, so that we can get our SwaggerUI easily,
                     * by creating our own route. It could be ANYTHING, doesn't have to be swagger*/
                });
            }
            app.UseHttpsRedirection();

            /*We need to tell our app to use sessions -> enables us to manage
             * "who's who" | 26/08/2026
            */
            app.UseSession();
            
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
