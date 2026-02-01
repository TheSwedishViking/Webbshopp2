using System;
using System.Collections.Generic;
using System.Text;
using Webbshopp2.Models;

namespace Webbshopp2.Vy
{
    internal class ÄndraKundUppgifter
    {

        public static void UpdateUser()
        {
            using (var db = new Webbshopp2.Models.MyDbContext())
            {
                Console.Clear();

                var user = db.Users.Find(Användare.KundId);

                if (user == null)
                {
                    Console.WriteLine("Ingen användare inloggad.");
                    Console.ReadKey(true);
                    Startsida.DrawStartPage();
                    return;
                }

                while (true)
                {
                    Console.Clear();

                    Console.WriteLine("Dina nuvarande uppgifter är: Namn: " + user.Namn + "  Ålder: " + user.Ålder);
                    Console.WriteLine("Email:" + user.Email + " Telefonnummer: " + user.Telefonnummer + "  Adress: " + user.Adress);

                    Console.WriteLine("Ändra dina uppgifter?  A = Ändra   B = Backa");

                    char userKey = char.ToLower(Console.ReadKey(true).KeyChar);

                    switch (userKey)
                    {
                        case 'a':

                            Console.Write("Ange nytt namn: ");
                            user.Namn = Console.ReadLine();

                            Console.Write("Ange ny ålder: ");
                            int.TryParse(Console.ReadLine(), out int nyÅlder);
                            user.Ålder = nyÅlder;

                            Console.Write("Ange ny email: ");
                            user.Email = Console.ReadLine();

                            Console.Write("Ange nytt telefonnummer: ");
                            int.TryParse(Console.ReadLine(), out int nyttTelefonnummer);
                            user.Telefonnummer = nyttTelefonnummer;

                            Console.Write("Ange ny adress: ");
                            user.Adress = Console.ReadLine();

                            db.SaveChanges();

                            Console.WriteLine("\nDina uppgifter har uppdaterats!");
                            Console.Clear();
                            Console.ReadKey(true);

                            if (Användare.KundId == 5)
                                StartsidaAdmin.DrawAdminStartPage();
                            else
                                Startsida.DrawStartPage();

                            return;

                        case 'b':

                            if (Användare.KundId == 5)
                            {
                                StartsidaAdmin.DrawAdminStartPage();
                            }
                            else
                            {                               
                                Startsida.DrawStartPage();
                            }

                            return;

                        default:
                            Console.WriteLine("Ogiltigt val.");
                            Console.ReadKey(true);
                            break;
                    }
                }
            }
        }



        public static void CreateUser()
        {
            using (var db = new Webbshopp2.Models.MyDbContext())
            {
                Console.Clear();

                Användare nyUser = new Användare();

                Console.WriteLine("Ny användare");

                Console.Write("Namn: ");
                nyUser.Namn = Console.ReadLine();

                Console.Write("Ålder: ");
                int.TryParse(Console.ReadLine(), out int ålder);
                nyUser.Ålder = ålder;

                Console.Write("Email:");
                nyUser.Email = Console.ReadLine();

                Console.Write("Telefonnummer:  ");
                int.TryParse(Console.ReadLine(), out int telefon);
                nyUser.Telefonnummer = telefon;

                Console.Write("Adress:   ");
                nyUser.Adress = Console.ReadLine();

                db.Users.Add(nyUser);
                db.SaveChanges();  

                Användare.KundId = nyUser.Id;

                Console.WriteLine("Användare skapad!");
                Console.WriteLine("Du är nu inloggad som: " + nyUser.Namn +"ID: " + nyUser.Id);
                
                Console.ReadKey(true);
                Console.Clear();
                Startsida.DrawStartPage();
            }
        }


    }
}
