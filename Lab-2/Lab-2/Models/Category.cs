using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lab_2.Models
{
    public class Category
    {
        public Category()
        {
            Films = new List<Film>();
        }
        public int Id { get; set; }
        [Required]
        [Display(Name = "Категория")]
        public string Name { get; set; }
        public virtual ICollection<Film> Films { get; set; }
    }
}
