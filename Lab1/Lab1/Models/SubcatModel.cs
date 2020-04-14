using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab1.Models
{
    public class SubcatModel
    {
        public Subcategory subcategory { get; set; }
        public bool isChecked { get; set; }
    }
    public class SubcatList
    {
        public List<SubcatModel> subcatModels { get; set; }
    }
}
