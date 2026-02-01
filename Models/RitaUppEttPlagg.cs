using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using static DrawingUI.DrawingVy;

namespace Webbshopp2.Models
{
    internal class RitaUppEttPlagg
    {
        public static void PlaggShowDrawer(int plaggId, int kategori)
        {
            Clothes plagg;
            using (var db = new Webbshopp2.Models.MyDbContext())
            {
                plagg = db.Clothes.Include(c => c.Category).FirstOrDefault(c => c.Id == plaggId);

                string price = plagg.Price.HasValue ? plagg.Price + " kr" : "Pris saknas";
                string description = plagg.Description ?? "Ingen beskrivning tillgänglig.";
                string märke = plagg.Company ?? "Okändt.";

                Console.WriteLine(
                    $"ID: {plagg.Id} | Namn: {plagg.Name} | Kategori: {plagg.Category.Name} | Färg: {plagg.Color}"
                );

                Console.WriteLine(
                    $"Material: {plagg.Material} |Märke: {märke} | Storlek: {plagg.Size} | Antal: {plagg.Antal} | Pris: {price} kr"
                );

                Console.WriteLine(description);


            }




        }


    }
}
