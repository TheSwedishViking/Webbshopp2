using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Webbshopp2.Models;



namespace Webbshopp2.Vy
{
    internal class CheckOut
    {

        public static void SlutförKöp()
        {
            Console.Clear();

            using (var db = new Webbshopp2.Models.MyDbContext())
            {
                var cartItemsList = db.CartItems.Where(c => c.UserId == Användare.KundId).Include(c => c.Clothes).ToList();
                var user = db.Users.Find(Användare.KundId);
                if (!cartItemsList.Any())
                {
                    Console.WriteLine("Kundkorgen är tom.");
                    Console.ReadKey();
                    Startsida.DrawStartPage();
                }   
                if(user.Adress == null)
                {
                    Console.WriteLine("Måste uppdatera KundUppgifter.");
                    Console.ReadKey(true);
                    Console.Clear();
                    ÄndraKundUppgifter.UpdateUser();
                } 
                else
                {
                Console.WriteLine(user.Namn + "  Adress" + user.Adress +  "  "  + user.Email + "  Tel.Nr: "  + user.Telefonnummer + "  Id: "  + user.Id);
                } //Adress etc


                    Console.WriteLine("Välj frakt alternativ :");
                Console.WriteLine("Slöa Bocken Bruse  (1) - 50kr  ");
                Console.WriteLine("Snabba Bocken Bruse  (2) - 200kr  ");
                Console.WriteLine("Backa  (3) ");
                Console.WriteLine();
                double moms = 0;
                double totalPris = 0;
                double frakt = 0;
                string fraktVal = "";
                int key = Console.ReadKey(true).KeyChar; 
                switch (key)
                {
                        case '1':
                        Console.WriteLine("Du har valt Slöa Bocken Bruse som frakt alternativ. 50kr tillkommer på din beställning.");
                        frakt = 50;
                        fraktVal = "Slöa Bocken Bruse";
                        break;//Alternativ
                        case '2':
                        Console.WriteLine("Du har valt Snabba Bocken Bruse som frakt alternativ. 200kr tillkommer på din beställning.");
                        fraktVal = "Snabba Bocken Bruse";
                        frakt = 200;
                        break;
                        case '3':
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
                        break;//Backa
                }

                
                foreach (var plagg in cartItemsList)
                {
                    totalPris += (plagg.Clothes.Price ?? 0) * plagg.Antal; //nullCheck
                    moms += ((plagg.Clothes.Price ?? 0) * plagg.Antal) * 0.25; //25% moms
                }



                Console.WriteLine("Totalt pris för din beställning är: " + (totalPris + moms + frakt) + " kr  " +
                    " varav " + moms + "kr (25%)  och " + frakt + " i frakt"); //fuling moms
                Console.WriteLine("Tryck -  K - för att köpa");
                Console.WriteLine("Tryck -  B - för att backa");

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
                        break;  //Back

                    case 'k':    //CheckOut
                        BeställdaProdukter beställning = new BeställdaProdukter
                        {
                            fraktAlternativ = fraktVal,
                            UserId = Användare.KundId,
                            Created = DateTime.Now
                        };  //kvitto - BeställdaProdukter
                        db.BeställdaProdukters.Add(beställning);
                        db.SaveChanges();  // keyFor KöptaProdukter

                        foreach (var item in cartItemsList)
                        {
                            KöptaProdukter köptPlagg = new KöptaProdukter
                            {
                                BeställningId = beställning.Id,              // key from KöptaProdukter
                                ClothesId = item.ClothesId,
                                ProductName = item.Clothes.Name,
                                BoughtAntal = item.Antal,
                                PriceAtPurchase = item.Clothes.Price ?? 0
                            };
                            db.KöptaProdukters.Add(köptPlagg);
                            Clothes clothesToUpdate = db.Clothes.Find(item.ClothesId);
                            clothesToUpdate.NumbersSold += item.Antal;
                            //update sold count
                        }

                        db.SaveChanges();

                      
                        db.CartItems.RemoveRange(cartItemsList);  //emptyForUser
                        db.SaveChanges();
                        Console.WriteLine("Tack för ditt köp! Tryck på valfri tangent för att återgå till startsidan.");
                        Console.ReadKey(true);
                        Console.Clear();
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
                        break;



                       
                }




            }


        }
    }
}
