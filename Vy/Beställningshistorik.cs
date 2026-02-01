using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Webbshopp2.Models;

namespace Webbshopp2.Vy
{
    internal class Beställningshistorik
    {

        public static void Orders()
        {
            using (var db = new Webbshopp2.Models.MyDbContext())
            {
                var previousOrders = db.BeställdaProdukters.Include(o => o.Items)
                    .ThenInclude(i => i.Clothes).Where(o => o.UserId == Användare.KundId).ToList();

                if (!previousOrders.Any())
                {
                    Console.WriteLine("Inga ordrar hittades! Klicka valfri.. tng");
                    Console.ReadKey(true);
                    return;
                }// NullCheck

                Console.WriteLine("Tidigare Beställningar");
                Console.WriteLine();
                foreach (var order in previousOrders)
                {
                    double totalt = 0;
                    string frakt = order.fraktAlternativ ?? "Okända Bocken Bruse";
                    Console.WriteLine();
                    Console.WriteLine(" Id: " + order.Id + "  Date " + order.Created.ToString("yyyy-MM-dd") + " Frakt -  " + frakt);
                    foreach (var item in order.Items)
                    {
                        Console.WriteLine(" Produkt: " + item.ProductName + "  Antal: " + item.BoughtAntal + "  Pris/st:" +
                            " " + item.PriceAtPurchase + "kr  Summa = " + (item.BoughtAntal*item.PriceAtPurchase) + " kr");
                       
                        totalt += item.BoughtAntal * item.PriceAtPurchase;
                    }
                    Console.WriteLine("Totalt " + totalt   + " Kr,  exklusive " + (totalt * 0.25) + "moms");
                    Console.WriteLine();
                }


                Console.WriteLine("Klicka på valfri tangent För att lämna..");
                Console.ReadKey(true);
                Console.Clear();

            }




        }

    }
}
