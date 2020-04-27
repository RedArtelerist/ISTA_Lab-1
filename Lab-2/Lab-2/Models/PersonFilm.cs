using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab_2.Models
{
    public class PersonFilm
    {
        public int Id { get; set; }
        public int FilmId { get; set; }
        public int PersonId { get; set; }
        public virtual Person Person { get; set; }
        public virtual Film Film { get; set; }
    }
}
