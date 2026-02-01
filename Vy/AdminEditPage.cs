using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Webbshopp2.Models;
using static DrawingUI.DrawingVy;

namespace Webbshopp2.Vy
{
    internal class AdminEditPage
    {


        public static void PlaggEdit() //inspekteraPlagg
        {

            List<string> topText9 = new List<string>
                    {
                          "Backa (B)  " + "  Edit (E) "  + " New Product (N) "   + "  Ny Kategori (C)" ,
                    };
            var windowTop18 = new Window("", 0, 0, topText9);
            windowTop18.Draw();

            using (var db = new Webbshopp2.Models.MyDbContext())
            {       //visa alla plagg //by category /id
                foreach (var plagg in db.Clothes.Include(c => c.Category).OrderByDescending(c => c.Category.Name).ThenBy(c => c.Id))
                {
                    string onFirstPage = plagg.showOnFirstPage == null ? "null" : plagg.showOnFirstPage.Value ? "1" : "0";

                    Console.WriteLine(plagg.Category.Name + "  id: " + plagg.Id + "  " + plagg.Name + " " + plagg.Antal + " " + plagg.Size + " " + plagg.Color + "  " + plagg.Material + "  " +
                       plagg.Price + "  " + onFirstPage);
                }
                //visa alla plagg



                char userKey = char.ToLower(Console.ReadKey(true).KeyChar); int idEdit;  //deklarera id
                switch (userKey)
                {
                    case 'b':  
                        Console.Clear();
                        StartsidaAdmin.DrawAdminStartPage();

                        break;//Backa
                    case 'n':
                        Console.Clear();
                        NewProduct();
                        break;//NewProduct
                    case 'e':  
                        while (true)
                        {
                            Console.WriteLine("Vilket plagg vill du ändra på?  Ange Id");
                            if (!int.TryParse(Console.ReadLine(), out idEdit))
                            {
                                Console.WriteLine("Fel inmatning. Ange ett nummer.");
                                Console.ReadKey(true);
                                continue;
                            }
                            if (!db.Clothes.Any(c => c.Id == idEdit))
                            {
                                Console.WriteLine("Plagget finns inte.");
                                Console.ReadKey(true);
                                Console.Clear();
                                continue;
                            }    //ErrorSafe
                            EditClothes(idEdit);

                        }
                        break;//EditProduct
                    case 'c':
                        NewCategory();
                        break;//NewCategory
                }
            }

        }

        public static void EditClothes(int idEdit)
        {
            Console.Clear();

            using (var db = new Webbshopp2.Models.MyDbContext())
            {
                Console.WriteLine(" Visa på startsida = (R) , Visa inte på Startsida = (D)  , Ändra Detaljer (H)  ,Backa = (B)");
                var startObjekt = db.Clothes.FirstOrDefault(c => c.Id == idEdit);


                char userKey = char.ToLower(Console.ReadKey(true).KeyChar);
                switch (userKey)
                {
                    case 'r':  //Visa På StartSida

                        if (startObjekt != null && startObjekt.showOnFirstPage != true)
                        {
                            startObjekt.showOnFirstPage = true;
                            db.SaveChanges();
                            Console.WriteLine(startObjekt.Name + " " + "visas på startsidan =" + startObjekt.showOnFirstPage);
                            Console.WriteLine("Tryck På valfri knapp för att fortsätta");
                            Console.ReadKey(true);
                            Console.Clear();
                            PlaggEdit();

                        }
                        else
                        {
                            Console.WriteLine(startObjekt.Name + " " + "visas på startsidan =" + startObjekt.showOnFirstPage);
                            Console.WriteLine("Objektet hittades ej, eller så visas det redan på startsidan. Tryck På valfri knapp för att fortsätta");
                            Console.ReadKey(true);
                            Console.Clear();
                            PlaggEdit();
                        }
                        break;//Visa På StartSida 
                    case 'd':


                        if (startObjekt.showOnFirstPage == true)
                        {
                            startObjekt.showOnFirstPage = false;
                            db.SaveChanges();
                            Console.WriteLine(startObjekt.Name + " " + "visas inte längre på startsidan");
                            Console.WriteLine("Tryck På valfri knapp för att fortsätta");
                            Console.ReadKey(true);
                            Console.Clear();
                            PlaggEdit();

                        }
                        else
                        {
                            Console.WriteLine(startObjekt.Name + " " + "visades aldrig på startsidan ");
                            Console.ReadKey(true);
                            Console.Clear();
                            PlaggEdit();
                        }
                        break; //Visa inte På StartSida
                    case 'h':
                        Console.Clear();
                        ModifyClothes(idEdit);
                        break;//EditDetaljerPåPlagg                    
                    case 'b':
                        Console.Clear();
                        PlaggEdit();
                        break;//Back
                    

                }
            }
        }


        public static void ModifyClothes(int idEdit)  //EditDetaljerPåPlagg
        {
            while (true)
            {
                using (var db = new Webbshopp2.Models.MyDbContext())
                {
                    Console.Clear();
                    Console.WriteLine("Vad vill du ändra på?");
                    RitaUppEttPlagg.PlaggShowDrawer(idEdit, 0); //visa plagg detaljer
                    //Borde börjat med att skicka data vi modeller som denna långt tidigare :(

                    Console.WriteLine("Namn (N) , Antal (A) , Storlek (S) ,Description (D), Färg (F) ," +
                    " Material (M) , Pris (P) , Backa (B)");
            
                   var plagg = db.Clothes.FirstOrDefault(c => c.Id == idEdit);         
                    
                    char userKey = char.ToLower(Console.ReadKey(true).KeyChar);
                    switch (userKey)
                    {

                        case 'n':
                            
                            Console.Write("Nytt namn: ");
                            plagg.Name = Console.ReadLine();
                            break;// Namn

                        case 'a':
                            Console.Write("Nytt antal: ");
                            plagg.Antal = int.Parse(Console.ReadLine());
                            break;// Antal

                        case 's':
                            Console.Write("Ny storlek: ");
                            plagg.Size = Console.ReadLine();
                            break;// Storlek

                        case 'd':
                            Console.Write("Ny beskrivning: ");
                            plagg.Description = Console.ReadLine();
                            break;// Description

                        case 'f':
                            Console.Write("Ny färg: ");
                            plagg.Color = Console.ReadLine();
                            break;// Färg

                        case 'm':
                            Console.Write("Nytt material: ");
                            plagg.Material = Console.ReadLine();
                            break;// Material

                        case 'p':
                            Console.Write("Nytt pris: ");
                            plagg.Price = int.Parse(Console.ReadLine());
                            break;// Pris

                        case 'b':
                            PlaggEdit();
                            break;// Backa

                        default:
                            Console.WriteLine("Ogiltigt Knapptryck! :)");
                            break;
                    }
                    Console.WriteLine("Sparat :)");
                    db.SaveChanges();

                }



            }


        }

        public static void NewProduct()
        {
            using(var db = new Webbshopp2.Models.MyDbContext())
            {
                Console.Clear();
                Clothes nyttPlagg = new Clothes();
                Console.Write("Namn: ");
                nyttPlagg.Name = Console.ReadLine();
                Console.Write("Antal: ");
                int.TryParse(Console.ReadLine(), out int antal);
                nyttPlagg.Antal = antal;
                Console.Write("Storlek: ");
                nyttPlagg.Size = Console.ReadLine();
                Console.Write("Description: ");
                nyttPlagg.Description = Console.ReadLine();
                Console.Write("Färg: ");
                nyttPlagg.Color = Console.ReadLine();
                Console.Write("Material: ");
                nyttPlagg.Material = Console.ReadLine();
                Console.Write("Märke : ");
                nyttPlagg.Company = Console.ReadLine();
                Console.Write("Pris: ");
                int.TryParse(Console.ReadLine(), out int pris);
                nyttPlagg.Price = pris;

                foreach (var category in db.Categories)
                {
                    Console.WriteLine("Kategori Id: " + category.Id + " Kategori Namn: " + category.Name);
                }

                Console.Write("Kategori Id: ");
                int.TryParse(Console.ReadLine(), out int kategori);
                nyttPlagg.CategoryId = kategori;
                nyttPlagg.showOnFirstPage = false;
                db.Clothes.Add(nyttPlagg);
                db.SaveChanges();
                Console.WriteLine("Nytt plagg tillagt! Tryck på valfri knapp för att fortsätta.");
                Console.ReadKey(true);
                Console.Clear();               
                PlaggEdit();
            }
        }  //Farlig metod då lätt att skriva fel


        public static void NewCategory()
        {
            Console.Clear();

            using (var db = new Webbshopp2.Models.MyDbContext()) {                 
                Category nyKategori = new Category();
                Console.Write("Kategori Namn: ");
                nyKategori.Name = Console.ReadLine();
                db.Categories.Add(nyKategori);
                db.SaveChanges();
                Console.WriteLine("Ny Kategori tillagd! Tryck på valfri knapp för att fortsätta.");
                Console.ReadKey(true);
                Console.Clear();
                PlaggEdit();






            }
        }

    }



}




    
    

