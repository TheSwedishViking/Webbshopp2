using System;
using System.Collections.Generic;
using System.Text;

namespace Webbshopp2.Models
{
    internal class Användare
    {
        public static int KundId { get; set; }
        public int Id { get; set; }
        public string? Adress { get; set; }
        public int? Telefonnummer { get; set; }
        public string? Email { get; set; }
        public string? Namn { get; set; }
        public int? Ålder { get; set; }



    }
}
