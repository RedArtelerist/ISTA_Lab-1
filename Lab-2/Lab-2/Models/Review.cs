using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lab_2.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string Info { get; set; }
        [Display(Name = "Дата")]
        public DateTime Date { get; set; }
        [Required]
        [Range(1,10)]
        [Display(Name ="Рейтинг")]
        public float Rating { get; set; }
        public int FilmId { get; set; }
        public int SeriesId { get; set; }
        public Film Film { get; set; }
        public Series Series { get; set; }
    }
}
