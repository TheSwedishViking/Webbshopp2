using System;
using System.Collections.Generic;
using System.Text;

namespace Webbshopp2.Models
{
    internal class KöptaProdukter
    {

        public int Id { get; set; }

        public int BeställningId { get; set; }
        public virtual BeställdaProdukter Beställning { get; set; } //En Till Många

        public int ClothesId { get; set; }
        public virtual Clothes Clothes { get; set; }  //LazyLoding.

        public string ProductName { get; set; }
        public int BoughtAntal { get; set; }
        public int PriceAtPurchase { get; set; }



    }
}
