using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Webbshopp2.Models
{
    internal class Category
    {

        public int Id { get; set; }
        public string Name { get; set; }


        public virtual ICollection<Clothes> Clothes { get; set; } = new List<Clothes>();



    }
}
