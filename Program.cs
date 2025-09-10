using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Zeneszamok.Models;
using Zeneszamok.Controllers;

namespace Zeneszamok
{
    class Program
    {
        
        static List<Eloado> eloadok;
       

        static void Modosit()
        {
            static void Lehetosegek()
            {
                Console.Clear();
                Console.WriteLine("Az előadók listája:\n");
                for (int i = 0; i < eloadok.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {eloadok[i]}");
                }
                Console.Write("Írd be az előadó számát amelyet törölni szeretnél (0 az vissza): ");
            }

            int szam = -1;
            int[] szamok = new int[eloadok.Count];
            for (int i = 0; i < szamok.Length; i++)
            {
                szamok[i] = eloadok[i].Id;
            }


            Lehetosegek();
            while (!int.TryParse(Console.ReadLine(), out szam) || !szamok.Contains(szam))
            {
                if (szam == 0)
                {
                    Console.Clear();
                    Valaszto();
                }
                Lehetosegek();
            }
            Console.WriteLine(szam);

            if (szam == 0)
            {
                Console.Clear();
                Valaszto();
            }

            int sorokSzama = 0;
            try
            {
                Console.WriteLine("----------------------");
                Console.Write("Add meg az új nevet:");
                string nev = Console.ReadLine();
                Console.Write("Add meg az új nemzetiséget:");
                string nemzetiseg = Console.ReadLine();
                Console.Write("Szolo? (I/N)");
                bool szolo = Console.ReadLine().ToUpper() == "I" ? true : false;

                sorokSzama = new EloadoController().Modosit(szam, nev, nemzetiseg, szolo);

            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Adatbázis hiba: {ex}");
            }
            finally
            {
                Console.WriteLine(sorokSzama > 0 ? "Sikeres Módosítás!" : "Sikertelen művelet!");     
                Console.WriteLine("\n(Nyomj egy gombot a folytatáshoz.)");
                Console.ReadKey();
                Console.Clear();
                Valaszto();
            }
        }

       

        static void Torol()
        {
            static void Lehetosegek()
            {
                Console.Clear();
                Console.WriteLine("Az előadók listája:\n");
                for (int i = 0; i < eloadok.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {eloadok[i].ToString()}");
                }
                Console.Write("Írd be az előadó számát amelyet törölni szeretnél (0 az vissza): ");
            }

            int szam = -1;
            int[] szamok = new int[eloadok.Count];
            for (int i = 0; i < szamok.Length; i++)
            {
                szamok[i] = eloadok[i].Id;
            }


            Lehetosegek();
            while (!int.TryParse(Console.ReadLine(), out szam) || !szamok.Contains(szam))
            {
                if (szam == 0)
                {
                    Console.Clear();
                    Valaszto();
                }
                Lehetosegek();
            }
            Console.WriteLine(szam);

            if (szam == 0)
            {
                Console.Clear();
                Valaszto();
            }

            int sorokSzama = 0;
            try
            {
                sorokSzama = new EloadoController().Torles(szam);

            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Adatbázis hiba: {ex}");
            }
            finally
            {
                Console.WriteLine(sorokSzama > 0 ? "Sikeres törlés!" : "Sikertelen művelet!");
                Console.WriteLine("\n(Nyomj egy gombot a folytatáshoz.)");
                Console.ReadKey();
                Console.Clear();
                Valaszto();
            }

        }

       

        static void Kiir()
        {
            Console.Clear();
            Console.WriteLine("Az előadók listája:\n");
            foreach (var eloado in eloadok)
            {
                Console.WriteLine(eloado.ToString());
            }
            Console.WriteLine("\n(Nyomj egy gombot a folytatáshoz.)");
            Console.ReadKey();
            Console.Clear();
            Valaszto();
        }

        static void Felvesz()
        {

            int sorokSzama = 0;
            try
            {
                sorokSzama = new EloadoController().Felvesz(AdatFelvetel());
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Adatbázis hiba: {ex}");
            }
            finally
            {
                string uzenet = sorokSzama > 0 ? "Sikeres felvétel!" : "Sikertelen felvétel!";
                Console.WriteLine(uzenet);
                Console.WriteLine("\n(Nyomj egy gombot a folytatáshoz.)");
                Console.ReadKey();
                Valaszto();
            }

           
        }

        private static Eloado AdatFelvetel()
        {
            Console.Write("Add meg az előadó nevét: ");
            string nev = Console.ReadLine();
            Console.Write("Add meg az elődó nemzetiségét: ");
            string nemzetiseg = Console.ReadLine();
            Console.Write("Szolo? (I/N)");
            bool szolo = Console.ReadLine().ToUpper() == "I" ? true : false;
            return new Eloado(nev, nemzetiseg, szolo);
        }

        static void Valaszto()
        {

            int MenuSzam;

            Lehetosegek();

            static void Lehetosegek()
            {
                Console.WriteLine("Válassz a lehetőségek közül:");
                Console.WriteLine( 
                       "1. Előadók kiírása\n" +
                       "2. Előadó módosítása\n" +
                       "3. Előadó felvétele\n" +
                       "4. Előadó törlése\n" +
                       "5. Kilépés");
                Console.Write("\nVálasztott menüpont: ");
            }

            static void Valasztas(int menu)
            {
                switch(menu)
                {
                    case 1:
                        Kiir();
                        break;
                    case 2:
                        Modosit();
                        break;
                    case 3:
                        Felvesz();
                        break;
                    case 4:
                        Torol();
                        break;
                    case 5:
                        Console.Clear();
                        Console.WriteLine("Viszlát!");
                        Console.ReadKey();
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("Hibás számsort adtál meg!");
                        Console.ReadKey();
                        Console.Clear();
                        Valaszto();
                        break;
                }
            }

            while(!int.TryParse(Console.ReadLine(), out MenuSzam))
            {
                Console.WriteLine("Hibás formátumban adtad meg!");
                Console.ReadKey();
                Console.Clear();
                Lehetosegek();
            }

            Valasztas(MenuSzam);


        }

       
        static void Main(string[] args)
        {
            eloadok = new EloadoController().Feltolt();
            Valaszto();


            Console.ReadKey();
        }

      

    }
}
