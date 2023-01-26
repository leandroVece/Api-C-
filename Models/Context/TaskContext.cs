using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace EF.Models
{
    public class TaskContext : DbContext
    {

        public DbSet<Category> Categoties { get; set; }

        public DbSet<Task> Tasks { get; set; }

        public TaskContext(DbContextOptions<TaskContext> options) : base(options) { }

        //con esto puedo crear una base de datos con especificaciones mas detalladas que con las propiedades de atributos
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            List<Category> listCategory = new List<Category>();
            listCategory.Add(new Category() { Id = Guid.Parse("7b5e9399-8e95-4ae8-8745-9542a01e2cc0"), Name = "Asunto domesticos", Salubrity = "Insalubre" });
            listCategory.Add(new Category() { Id = Guid.Parse("0a9fa564-0604-4dfa-88df-3636fe395651"), Name = "Actividad recreativa", Salubrity = "sadudable y recomendable" });


            modelBuilder.Entity<Category>(categoty =>
            {
                categoty.ToTable("Category");
                categoty.HasKey(p => p.Id);

                categoty.Property(p => p.Name).IsRequired().HasMaxLength(150);
                categoty.Property(p => p.Salubrity).IsRequired();
                categoty.Property(p => p.Description).IsRequired(false);

                categoty.HasData(listCategory);


            });


            List<Task> listTask = new List<Task>();
            listTask.Add(new Task() { Id = Guid.Parse("f5d327bf-be98-4786-81d5-0a2412b7807e"), IdCategory = Guid.Parse("7b5e9399-8e95-4ae8-8745-9542a01e2cc0"), Title = "Limpiar Baño", PriorityTask = Priority.medium, Date = DateTime.Now });
            listTask.Add(new Task() { Id = Guid.Parse("629f9587-abc8-4c85-859f-acb762b754ed"), IdCategory = Guid.Parse("0a9fa564-0604-4dfa-88df-3636fe395651"), Title = "Practica con el arco", PriorityTask = Priority.medium, Date = DateTime.Now });

            modelBuilder.Entity<Task>(task =>
            {
                task.ToTable("TasK");
                task.HasKey(p => p.Id);

                //aqui le digo a fluet que cree una base que tenga una relacion de unos a muchos y que la clave foranea va a ser idCategory
                task.HasOne(p => p.Category).WithMany(p => p.Tasks).HasForeignKey(p => p.IdCategory);

                task.Property(p => p.Title).IsRequired().HasMaxLength(150);
                task.Property(p => p.Description).IsRequired(false);
                task.Property(p => p.Date);
                task.Property(p => p.PriorityTask).HasConversion(x => x.ToString(), x => (Priority)Enum.Parse(typeof(Priority), x));
                task.Ignore(p => p.summary);

                task.HasData(listTask);

            });
        }

    }
}