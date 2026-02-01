using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Webbshopp2.Models;

namespace Webbshopp2.Vy
{
    internal class Kundkorg
    {
        

        public static async Task ShoppingCart(Clothes plagg)  
        {

            using (var db = new Webbshopp2.Models.MyDbContext())
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                var product = await db.Clothes.FirstAsync(c => c.Id == plagg.Id);

                if (product.Antal <= 0) 
                {
                    Console.WriteLine("Slut i lager!");
                    
                    return;
                }


                var existingItem = await db.CartItems
               .FirstOrDefaultAsync(c => c.ClothesId == product.Id && c.UserId == Användare.KundId);   
                if (existingItem != null)
                {
                    existingItem.Antal++;
                }    //Ökar antal i kundkorg och databasKorgen

                else
                {
                    db.CartItems.Add(new CartItem
                    {
                        ClothesId = product.Id,
                        UserId = Användare.KundId,
                        Antal = 1  //kan endast ta en vara i taget
                    });
                }
                product.Antal--;  //minskar antal i databasen
                await db.SaveChangesAsync();
                sw.Stop();
                Console.WriteLine("asynkroniska metoden tog för varukorgen tog " + sw.ElapsedMilliseconds + " ms");
            }
            Console.WriteLine("Plagg  " + plagg.Name + " lades till i kundkorgen.");
        }

        public static void ShowCart()
        {
            Console.Clear();
            using (var db = new Webbshopp2.Models.MyDbContext())
            {


                if (!db.CartItems.Any(c => c.UserId == Användare.KundId))  //kund med varor
                {

                    Console.WriteLine("Kundkorgen är tom.");
                    Console.WriteLine("Tryck valfri tangent för att gå tillbaka");
                    Console.ReadKey(true);
                    Console.Clear();
                    Startsida.DrawStartPage();
                }
                else
                {
                    int totalPris = 0;
                    Console.WriteLine("Cart");
                    var cartItemsList = db.CartItems.Where(c => c.UserId == Användare.KundId).Include(c => c.Clothes).ToList();

                    foreach (var plagg in cartItemsList)
                    {                   
                        
                            Console.WriteLine(
                              "ID: " + plagg.Id +
                              ", Namn: " + plagg.Clothes.Name +
                              ", Antal: " + plagg.Antal +
                              ", Pris: " + plagg.Clothes.Price + " kr" +
                              ", Summa: " + (plagg.Clothes.Price * plagg.Antal) + " kr"
                          
                          );

                        totalPris += (plagg.Clothes.Price ?? 0) * plagg.Antal; //nullCeck

                        
                    }
                    Console.WriteLine();
                    Console.WriteLine(totalPris);

                    Console.WriteLine();
                    Console.WriteLine("Backa - B   , CheckOut - C ,  RemoveProduct - R");
                    char userKey = char.ToLower(Console.ReadKey(true).KeyChar);
                    switch (userKey)
                    {
                        case 'b':    
                            if (Användare.KundId == 5)
                            {
                                Console.Clear();
                                StartsidaAdmin.DrawAdminStartPage();
                            }
                            else
                            {
                                Console.Clear();
                                Startsida.DrawStartPage();
                            }
                            break;//Back
                        case 'r':    
                            RemoveProduct();
                            break;//RemoveProduct
                        case 'c':                        
                            CheckOut.SlutförKöp();
                            break;//Checkout    

                    }
                }
            }
        }

        public static void RemoveProduct()
        {
         using(var db = new Webbshopp2.Models.MyDbContext())
          {
            
            Console.WriteLine();
            Console.WriteLine("Vilken Produkt vill du ta bort? Ange ID");
            string input = Console.ReadLine();

            if (!int.TryParse(input, out int idToRemove))
            {
                Console.WriteLine("Felaktigt ID.");
                Console.ReadKey();
                ShowCart();
                return;
            } 
            var itemToRemove  = db.CartItems.FirstOrDefault(c => c.Id == idToRemove && c.UserId == Användare.KundId); 

            if (itemToRemove == null) 
            {
                Console.WriteLine("Produkten finns inte i kundkorgen.");
                Console.ReadKey();
                ShowCart();
                return;
            } //idCheck



            if (itemToRemove.Antal > 1)  
                {
                itemToRemove.Antal--;
                }//tittar antal i kundkorg
            else
            {
                    db.CartItems.Remove(itemToRemove);
            }
                db.Clothes.First(c => c.Id == itemToRemove.ClothesId).Antal++; //lägger tillbaka i lager
                db.SaveChanges();


            }
            ShowCart();

       }
  }
}