using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab_2.Models
{
    public class PersonSeries
    {
        public int Id { get; set; }
        public int SeriesId { get; set; }
        public int PersonId { get; set; }
        public virtual Person Person { get; set; }
        public virtual Series Series { get; set; }
    }
}
