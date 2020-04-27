using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lab_2.Models
{
    public class Film
    {
        public Film()
        {
            PersonFilms = new List<PersonFilm>();
            Reviews = new List<Review>();
        }
        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "Название")]
        public string Name { get; set; }
        [StringLength(50, MinimumLength = 3)]
        [RegularExpression(@"^[a-zA-Z0-9""'\s-]*$", ErrorMessage = "Only latina")]
        [Display(Name = "Ориг. название")]
        [Required]
        public string EngName { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "Страна")]
        public string Country { get; set; }
        [Required]
        [Display(Name = "Премьера")]
        public DateTime Premiere { get; set; }
        [Required]
        [Range(1, 600)]
        [Display(Name = "Бюджет млн$")]
        public long Budget { get; set; }
        [Required]
        [Range(60, 300)]
        [Display(Name = "Продолжительность")]
        public int Time { get; set; }
        public int CategoryId { get; set; }
        [Display(Name = "Категория")]
        public string CategoryName { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<PersonFilm> PersonFilms { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }

    }
}
