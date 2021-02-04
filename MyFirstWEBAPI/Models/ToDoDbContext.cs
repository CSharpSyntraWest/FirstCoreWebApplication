using Microsoft.EntityFrameworkCore;

namespace MyFirstWEBAPI.Models
{
    public class ToDoDbContext : DbContext
    {
        public ToDoDbContext(DbContextOptions<ToDoDbContext> options) : base(options) { }

        public DbSet<ToDo> ToDos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder
  optionsBuilder)
        {

  
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            

            modelBuilder.Entity<ToDo>(toDo =>
            {
                toDo.Property(c => c.Id)
                            .ValueGeneratedOnAdd(); //  autonummering
                toDo.HasKey(s => s.Id);
                toDo.Property(s => s.Titel).IsRequired().HasMaxLength(50);
                toDo.Property(s => s.Omschrijving).IsRequired().HasMaxLength(255);
               
            });
            base.OnModelCreating(modelBuilder);
        }
      
    }
}