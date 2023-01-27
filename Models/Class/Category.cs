using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EF.Models
{

    public class Category
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
        public string Salubrity { get; set; }

        [JsonIgnore]
        public virtual ICollection<Task> Tasks { get; set; }
    }
}


