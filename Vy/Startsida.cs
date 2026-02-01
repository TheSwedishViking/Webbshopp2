using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using Webbshopp2.Models;
using static DrawingUI.DrawingVy;


namespace Webbshopp2.Vy
{
    internal class Startsida
    {

        public static async Task DrawStartPage()
        {
            while (true)
            {

                Console.Clear();
                Stopwatch sw = new Stopwatch();
                List<string> topText1 = new List<string> { "# Mårtens Loppis #", "Bara hela kläder!" };
                var windowTop1 = new Window("", 2, 1, topText1);
                windowTop1.Draw();
                List<Clothes> randomOffers = new();
                string username = "Gäst";  //Deklarera name här för att undvika scope problem

                using (var db = new Webbshopp2.Models.MyDbContext())
                {

                    sw.Start();



                    Random rnd = new Random();
                    randomOffers = (await db.Clothes.Include(c => c.Category).Where(c => c.showOnFirstPage == true)
                        .ToListAsync()).OrderBy(x => rnd.Next()).Take(3).ToList();
                    //c# blandat med sql bråkar, därför 2 toList

                    if (!randomOffers.Any())
                    {
                        Console.WriteLine("No offers marked to show on first page.");
                        Console.ReadKey();
                    }
                    else
                    {

                        int x = 10;
                        char[] keys = { '1', '2', '3' };
                        int index = 0;

                        foreach (var item in randomOffers)
                        {
                            char key = keys[index];

                            string category = item.Category.Name;
                            string name = item.Name;
                            string price = item.Price.HasValue ? item.Price + " kr" : "Pris saknas";
                            string color = item.Color;
                            string size = item.Size;
                            int antal = item.Antal;
                            string material = item.Material;

                            List<string> topText3 = new List<string>
                        {
                             category, name,"Färg: "+ color,"Material: " + material ,"Storlek : " + size,"Antal:  " + antal ,price, "",
                             "Tryck " + key + " för att köpa"
                        };

                            var windowTop3 = new Window("Erbjudande", x, 20, topText3);  // x , y 
                            windowTop3.Draw();


                            x += 30; // move window right
                            index++;
                        }

                    }

                    var kundNamn = await db.Users.FirstOrDefaultAsync(u => u.Id == Användare.KundId);
                    username = kundNamn?.Namn ?? "Gäst";


                    sw.Stop();
                    Console.WriteLine("Tog " + sw.ElapsedMilliseconds + " ms för task");

                }

                List<string> topText2 = new List<string>
                {
                             "Tröjor - A" ,
                             "BYXOR - B" ,
                             "Jackor - C" ,
                             "Kundkorg - K",
                             "CheckOut - Y",
                             "Senaste nytt - L",
                             "Easy Search - S",
                             "Beställningshistorik - H",
                             "",
                             "Byta Användare - X",
                             "Ändra Kunduppgifter - N",
                             "Nuvrande Användare  " + username + " ID: " + Användare.KundId,
                };

                var windowTop2 = new Window("Kategorier", 50, 2, topText2);  // x , y 
                windowTop2.Draw();


                char userKey = char.ToLower(Console.ReadKey(true).KeyChar);
                int kategori;
                switch (userKey)
                {
                    case 'a':    //Shirt
                        KategoriSida.CategoriesPage(kategori = 1);
                        break;//Shirt

                    case 'b':    //Pants    
                        KategoriSida.CategoriesPage(kategori = 2);

                        break;//Pants

                    case 'c':   //Jacket
                        KategoriSida.CategoriesPage(kategori = 3);

                        break;//Jackets

                    case 'k':
                        Kundkorg.ShowCart();

                        break;//Cart

                    case '1':
                        await Kundkorg.ShoppingCart(randomOffers[0]);  //första objektet i offers
                        break;//sale
                    case 'l':
                        KategoriSida.SenasteKategorier();
                        break;//Senaste Kategorierna
                    case '2':    //Pants sale   
                        await Kundkorg.ShoppingCart(randomOffers[1]);
                        break;//sale
                    case 's':
                        KategoriSida.EasySearch();
                        break;//EasySearch
                    case '3':   //Jacket sale
                        await Kundkorg.ShoppingCart(randomOffers[2]);
                        break;//Sale
                    case 'y':
                        CheckOut.SlutförKöp();
                        break;
                    case 'x':
                        Console.WriteLine("Vem Är Nya användare? Admin  = 5");
                        int id = int.Parse(Console.ReadLine());
                        Användare.KundId = id;
                        using (var db = new Webbshopp2.Models.MyDbContext())
                        {
                            var user = db.Users.Find(id);

                            if (user == null)
                            {
                                Console.WriteLine("Användaren finns inte. Skapar ny användare...");
                                Console.ReadKey(true);

                                ÄndraKundUppgifter.CreateUser();
                                break;
                            }

                            Användare.KundId = user.Id;
                        }

                        Console.Clear();

                        if (id == 5)
                        {
                            StartsidaAdmin.DrawAdminStartPage();
                        }
                        else
                        {
                            DrawStartPage();
                        }
                        break;//SwitchOrCreateUser
                    case 'n':
                        ÄndraKundUppgifter.UpdateUser();
                        break;//UpdateUser
                    case 'h':
                        Beställningshistorik.Orders();
                        break;//OrderHistory

                }


            }
        }
    }
}
