using System;
using System.Collections.Generic;
using System.Text;

namespace Webbshopp2.Models
{
    internal class BeställdaProdukter
    {

        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public string? fraktAlternativ { get; set; }

        public virtual ICollection<KöptaProdukter> Items { get; set; } = new List<KöptaProdukter>(); //Många Till En

    }
}
