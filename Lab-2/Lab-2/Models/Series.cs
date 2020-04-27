using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lab_2.Models
{
    public class Series
    {
        public Series()
        {
            PersonSeries = new List<PersonSeries>();
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
        [Range(1, 50)]
        [Display(Name = "Продолжительность")]
        public int AmountSeasons { get; set; }
        [Required]
        [Range(5, 10000)]
        [Display(Name = "Продолжительность")]
        public int AmountChapters { get; set; }
        public int CategoryId { get; set; }
        [Display(Name = "Категория")]
        public string CategoryName { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<PersonSeries> PersonSeries { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }

    }
}

