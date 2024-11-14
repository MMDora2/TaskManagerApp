using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TaskManagerAPI.Models;

namespace TaskManagerAPI.Data
{
    public class TaskContext : DbContext
    {
        public TaskContext(DbContextOptions<TaskContext> options) : base(options) { }

        public DbSet<TaskItem> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskItem>().HasData(
                new TaskItem { Id = 1, Title = "Task 1", Description = "Description for Task 1", IsCompleted = false, DueDate = DateTime.Now.AddDays(2) },
                new TaskItem { Id = 2, Title = "Task 2", Description = "Description for Task 2", IsCompleted = false, DueDate = DateTime.Now.AddDays(3) },
                new TaskItem { Id = 3, Title = "Task 3", Description = "Description for Task 3", IsCompleted = true, DueDate = DateTime.Now.AddDays(-1) }
            );
        }
    }
}