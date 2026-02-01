using System;
using System.Collections.Generic;
using System.Text;

namespace Webbshopp2.Models
{
    internal class CartItem
    {



        public int Id { get; set; }

        public int ClothesId { get; set; }
        public virtual Clothes Clothes { get; set; }

        public int Antal { get; set; }

        public int UserId { get; set; }
    }
}
