using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EF.Models
{

    public class Task
    {
        [Key]
        public Guid Id { get; set; }
        public Guid IdCategory { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
        public Priority PriorityTask { get; set; }
        public DateTime Date { get; set; }

        public virtual Category Category { get; set; }

        public string summary { get; set; }



    }

    public enum Priority
    {
        low,
        medium,
        high
    }
}

