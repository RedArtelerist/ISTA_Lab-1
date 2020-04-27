using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Lab_2.Models
{
    public enum Role
    {
        Producer,
        Direcor,
        Screenwriter,
        Operator,
        Composer,
        Editor,
        Actor
    }
    public class Person
    {
        public Person()
        {
            PersonFilms = new List<PersonFilm>();
            Roles = new List<Role>();
        }
        public int Id { get; set; }
        [Required]
        [Display(Name = "ФИО")]
        [StringLength(50, MinimumLength = 5)]

        public string Name { get; set; }
        [Required]
        [Display(Name = "Дата рождение")]
        public DateTime Birth { get; set; }
        public int Age { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "Место рождение(Страна)")]
        public string Country { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "Место рождение(Город)")]
        public string City { get; set; }
        [NotMapped]
        public List<Role> Roles { get; set; }
        public virtual ICollection<PersonFilm> PersonFilms { get; set; }
        public static int GetAge(DateTime birthDate)
        {
            var now = DateTime.Today;
            return now.Year - birthDate.Year - 1 +
                ((now.Month > birthDate.Month || now.Month == birthDate.Month && now.Day >= birthDate.Day) ? 1 : 0);
        }
    }
}
