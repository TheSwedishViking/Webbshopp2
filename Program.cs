using Webbshopp2.Models;
using Webbshopp2.Vy;

namespace Webbshopp2
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            
                Användare.KundId = 1; //default kund

                if (Användare.KundId == 5)
                {
                await StartsidaAdmin.DrawAdminStartPage();
                }
                else
                {
                await Startsida.DrawStartPage();

                }
            
           



        }
    }
}
