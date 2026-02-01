using Dapper;
using DrawingUI;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Webbshopp2.Models;
using static DrawingUI.DrawingVy;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Webbshopp2.Vy
{
    internal class KategoriSida
    {
        static string connString = "data source=.\\SQLEXPRESS; initial catalog = WebbShopp; persist security info = True; Integrated Security = True; TrustServerCertificate=true;";



        public static void CategoriesPage(int kategori)
        {
            Console.Clear();
            using (var db = new Webbshopp2.Models.MyDbContext())
            {
                var category = db.Categories
                                 .Include(c => c.Clothes)
                                 .FirstOrDefault(c => c.Id == kategori);



                if (kategori != 1 && kategori != 2 && kategori != 3)
                {
                    Console.WriteLine("Kategorin hittades inte.");
                    return;
                } //spärr
                List<string> topText2 = new List<string>
                    {
                             category.Name
                    }; //rubrik
                var windowTop2 = new Window("", 0, 7, topText2);
                windowTop2.Draw();


                foreach (var clothes in category.Clothes)
                {
                    string price = clothes.Price.HasValue ? clothes.Price + " kr" : "Pris saknas";

                    Console.WriteLine(clothes.Name + "  Antal: " + clothes.Antal + "  ,  pris - " + price + " ,    Plagg id : - " + clothes.Id);
                }

                Console.WriteLine();
                Console.WriteLine();
                List<string> topText4 = new List<string>
                    {
                          "Backa (B)","Inspektera plagg (A)"
                    };
                var windowTop4 = new Window("", 15, 5, topText4);
                windowTop4.Draw();


                char userKey = char.ToLower(Console.ReadKey(true).KeyChar); int plaggId;
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
                        break;   //Back

                    case 'a':
                        while (true)   //lopp ny inmatningsförsök
                        {
                            Console.WriteLine();
                            List<string> topText33 = new List<string>
                            {
                             "Ange Id För Plagget Du Vill Inspektera:"
                             };
                            var windowTop33 = new Window("", 0, 15, topText33);
                            windowTop33.Draw();

                            if (!int.TryParse(Console.ReadLine(), out plaggId))
                            {
                                Console.WriteLine("Fel inmatning. Ange ett nummer.");
                                Console.ReadKey(true);
                                continue;
                            }
                            if (!db.Clothes.Any(c => c.Id == plaggId))
                            {
                                Console.WriteLine("Plagget finns inte.");
                                Console.ReadKey(true);
                                Console.Clear();
                                continue;
                            }    //ErrorSafe
                            PlaggShow(plaggId, kategori);
                        }
                        break;  //ToInspekt
                }
            }

        }

        public static void PlaggShow(int plaggId, int kategori) //inspekteraPlagg
        {
            
            
                Clothes plagg;
                Console.Clear();
            using (var db = new Webbshopp2.Models.MyDbContext())
            {
                plagg = db.Clothes.Include(c => c.Category).FirstOrDefault(c => c.Id == plaggId);

                RitaUppEttPlagg.PlaggShowDrawer(plaggId, kategori);  //ritaPlagg




                Console.WriteLine();
                List<string> topText4 = new List<string>
                    {
                          "Backa (B)","Lägg till i kundkorg (K)"
                    };
                var windowTop4 = new Window("", 0, 20, topText4);
                windowTop4.Draw();

                char userKey = char.ToLower(Console.ReadKey(true).KeyChar);
                switch (userKey)
                {
                    case 'b':
                        Console.Clear();
                        KategoriSida.CategoriesPage(kategori);


                        break;//backaFrånInspekteraPlagg

                    case 'k':
                        Kundkorg.ShoppingCart(plagg);

                        break;//Lägg TIll I Varukorg

                }

            }
        }


        public static void EasySearch()
        {                    
            using( var db = new Webbshopp2.Models.MyDbContext())
            {
                while (true)
                {
                    
                    Console.Clear();
                    Console.WriteLine("Vad vill du söka genom?");
                    Console.WriteLine("Namn = N ,  Färg = F  , Material = M  ,  Specificerade Sökningar = S   , Backa = B");


                    char userKey = char.ToLower(Console.ReadKey(true).KeyChar);
                    string sökOrd = "";
                    List<Clothes> results = new();        //deklarera varaibler innan switch
                    Console.WriteLine("Skriv ditt sökord:");
                    string searchTerm ="";      

                    switch (userKey)
                    {


                        case 'n':
                            sökOrd = "Name";
                            results = db.Clothes.Include(c => c.Category)
                            .Where(c => c.Name.Contains(searchTerm)).ToList();

                            break;// Namn
                        case 'f':   // Färg
                            sökOrd = "Color";
                        searchTerm = Console.ReadLine();
                            results = db.Clothes.Include(c => c.Category)
                           .Where(c => c.Color.Contains(searchTerm)).ToList();
                        break;// Färg
                        case 'm':
                            sökOrd = "Material";
                        searchTerm = Console.ReadLine();
                        results = db.Clothes.Include(c => c.Category)
                        .Where(c => c.Material.Contains(searchTerm)).ToList();
                        break;// Material
                        case 's':
                            Console.Clear();
                            SpecifiedSearches();


                            break;// Specificerad prisram
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
                            break;//Backa
                        default:
                            Console.WriteLine("Ogiltigt val.");
                            Console.ReadKey(true);                          
                            break;
                    }



                    if (results.Count == 0)
                    {
                        Console.WriteLine("Inga plagg hittades för sökordet: " + searchTerm + "  i " + sökOrd + " klicka valfri tng..");
                        Console.ReadKey(true);
                    }//null check
                    else
                    {
                        Console.WriteLine("Sökresultat för: " + searchTerm + " i  kategorin för " + sökOrd);
                        Console.WriteLine();
                        foreach (var item in results)
                        {
                            string price = item.Price.HasValue ? item.Price + " kr" : "Pris saknas";
                            Console.WriteLine(
                                 $"Kategori: {item.Category.Name} | " +
                                 $"Namn: {item.Name} | " +
                                 $"Storlek: {item.Size} | " +
                                 $"Färg: {item.Color ?? "Okänd"} | " +
                                 $"Material: {item.Material ?? "Okänt"} | " +
                                 $"Pris: {price} | " +
                                 $"Antal: {item.Antal}"
);
                            Console.WriteLine();
                        }
                        Console.WriteLine("  klicka valfri tng..");
                        Console.ReadKey(true);
                    }
                }
            }
        }

        public static void SpecifiedSearches()
        {
            Console.WriteLine(" Prisintervall (a)  ,  Mest Sålda produkter (M) ,  Märke (D)  ,  Backa  (B),");
            char userKey = char.ToLower(Console.ReadKey(true).KeyChar); int maxPrice; List<Clothes> maxPriceClothes = new();
          using (var connection = new SqlConnection(connString)) { 

                switch (userKey)
                {

                        case 'a':   // Max price
                        Console.WriteLine();
                        Console.Write("Visa plagg som max kostar: ");

                        if (!int.TryParse(Console.ReadLine(), out maxPrice))
                        {
                            Console.WriteLine("Fel inmatning.");
                            Console.ReadKey(true);
                            return;
                        }

                        string priceSql = @"
                             SELECT Clothes.*, Categories.Name AS CategoryName FROM Clothes
                             JOIN Categories ON Clothes.CategoryId = Categories.Id
                               WHERE Clothes.Price <= @maxPrice";

                        var cheapClothes = connection.Query<Clothes>(priceSql, new { maxPrice }).ToList();
                        foreach(var item in cheapClothes)
                        {
                            RitaUppEttPlagg.PlaggShowDrawer(item.Id, item.CategoryId);

                        }
                        break;//Max Pris
                        case 'm':

                        var popularSql = @"
                          SELECT c.* FROM Clothes c WHERE c.NumbersSold > 0 ORDER BY c.NumbersSold DESC";

                        var popularClothes = connection.Query<Models.Clothes>(popularSql).ToList();

                        foreach (var item in popularClothes)
                        {
                            string antalSålda = item.NumbersSold > 0 ? item.NumbersSold.ToString() : "Inga sålda än";
                            RitaUppEttPlagg.PlaggShowDrawer(item.Id, item.CategoryId);

                            Console.Write("Antal Sålda = " + antalSålda);
                        }                        
                        break;//Mest Sålda
                        case 'd':                              
                            var companySql = @"SELECT DISTINCT Company FROM Clothes";
                            
                            var companyClothes = connection.Query<string>(companySql).ToList();  //string list
                            foreach (var company in companyClothes)
                            {
                            Console.WriteLine(company);
                            }
                            Console.WriteLine("Skriv Märket du vill söka efter:");
                            string brandSearch = Console.ReadLine();

                            var companyClothesSql = @"SELECT * FROM Clothes WHERE Company LIKE @brand ";
                            var brandedClothes = connection.Query<Clothes>(companyClothesSql, new { brand = "%" + brandSearch + "%" }).ToList();
                             foreach (var item in brandedClothes)
                             {
                                RitaUppEttPlagg.PlaggShowDrawer(item.Id, item.CategoryId);
                             }
                           
                           
                        break;//Märke
                        case 'b':
                        Console.Clear();
                        EasySearch();
                        break;//Backa
                }
                while (true)     //Varukorg loop
                {
                    Console.WriteLine();
                    Console.WriteLine("Köpa = K     ,     Backa = B");
                    char userKeyBuyOrBack = char.ToLower(Console.ReadKey(true).KeyChar);
                    switch (userKeyBuyOrBack)
                    {
                        case 'k':
                            Console.WriteLine("Vilket plagg vill du ha i varukorgen?");
                            int buy = int.Parse(Console.ReadLine());
                            string buySql = @"
                             SELECT Id,Name  FROM Clothes WHERE Clothes.Id = @buy";
                            var selectedCloth = connection.QueryFirstOrDefault<Clothes>(buySql, new { buy }); //object builder

                            if (selectedCloth == null)
                            {
                                Console.WriteLine("Plagget hittades inte.");
                                Console.ReadKey(true);
                                break;
                            }
                            Kundkorg.ShoppingCart(selectedCloth);
                            Console.WriteLine(selectedCloth.Name + " Finns nu i varukorgen :)");
                            break;//Buy
                        case 'b':
                            Console.Clear();
                            EasySearch();
                            break;//Back
                    }
                }
               


          }


        }



        public static void SenasteKategorier()
        {
            using (var db = new Webbshopp2.Models.MyDbContext())
            {
                var senasteKategorier = db.Categories.Include(c => c.Clothes).Where(c => c.Id > 3).ToList();  // alla senare kategorier

                Console.WriteLine("Senaste Kategorier:");
                foreach (var kategori in senasteKategorier)
                {
                    Console.WriteLine($"- {kategori.Name} (ID: {kategori.Id})");
                    foreach(var plagg in kategori.Clothes)
                    {
                        RitaUppEttPlagg.PlaggShowDrawer(plagg.Id, kategori.Id);
                    }
                }
            }
            Console.ReadKey(true);
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
        }


    }
}
