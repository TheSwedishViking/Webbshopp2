using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Text;
using System.Linq;


namespace Webbshopp2.Models
{
    internal class Clothes
    {
        public Clothes(string? name, int categoryId)
        {
            Name = name;
            CategoryId = categoryId;
        }  
        public Clothes() { } //Constructor för nya kläder


        public int Id { get; set; }
        public string Name { get; set; }
        public string Size { get; set; }
        public string? Color { get; set; }
        public string? Material { get; set; }
        public int? Price { get; set; }
        public int Antal { get; set; }
        public bool? showOnFirstPage { get; set; }
        public int CategoryId  { get; set; }//FK
        public int NumbersSold { get; set; }
        public string? Company { get; set; }
        public string? Description { get; set; }

        //Varje plagg tillhör en kategori
        public virtual Category Category { get; set; }


    }
}
