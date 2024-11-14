
using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.Data;
using TaskManagerAPI.Models;

namespace TaskManagerAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //SQL
            builder.Services.AddDbContext<TaskContext>(options =>
                options.UseSqlite(builder.Configuration.GetConnectionString("TaskContext")));


            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<TaskContext>();
                dbContext.Database.Migrate(); //database is created
                SeedData(dbContext); 
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }

        private static void SeedData(TaskContext context)
        {
            if (!context.Tasks.Any())
            {
                context.Tasks.AddRange(
                    new TaskItem { Title = "Task 1", Description = "Description for Task 1", IsCompleted = false, DueDate = DateTime.Now.AddDays(2) },
                    new TaskItem { Title = "Task 2", Description = "Description for Task 2", IsCompleted = false, DueDate = DateTime.Now.AddDays(3) },
                    new TaskItem { Title = "Task 3", Description = "Description for Task 3", IsCompleted = true, DueDate = DateTime.Now.AddDays(-1) }
                );
                context.SaveChanges();
            }
        }
    }
}
