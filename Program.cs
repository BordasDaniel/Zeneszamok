using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Zeneszamok.Models;
using Zeneszamok.Controllers;
using System.Collections.Concurrent;

namespace Zeneszamok
{
    class Program
    {
        
        static List<Eloado> eloadok;
        static List<Lemez> lemezek;

        #region Eloado

        static void Modosit()
        {
            static void Lehetosegek()
            {
                Console.Clear();
                Console.WriteLine("A lemezek listája:\n");
                for (int i = 0; i < eloadok.Count; i++)
                {
                    Console.WriteLine($"{eloadok[i]}");
                }
                Console.Write("Írd be az előadó számát amelyet módosítani szeretnél (0 az vissza): ");
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
                    EloadoOpcioValaszto();
                }
                Lehetosegek();
            }
            Console.WriteLine(szam);

            if (szam == 0)
            {
                Console.Clear();
                EloadoOpcioValaszto();
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
                EloadoOpcioValaszto();
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
                    EloadoOpcioValaszto();
                }
                Lehetosegek();
            }
            Console.WriteLine(szam);

            if (szam == 0)
            {
                Console.Clear();
                EloadoOpcioValaszto();
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
                EloadoOpcioValaszto();
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
            EloadoOpcioValaszto();
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
                EloadoOpcioValaszto();
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

        static void EloadoOpcioValaszto()
        {
            eloadok = new EloadoController().Feltolt();
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
                       "5. Vissza a táblákhoz\n" +
                       "6. Kilépés");
                Console.Write("\nVálasztott menüpont: ");
            }

            static void Valasztas(int menu)
            {
                switch (menu)
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
                        TablaValaszto();
                        break;
                    case 6:
                        Console.Clear();
                        Console.WriteLine("Viszlát!");
                        Console.ReadKey();
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("Hibás számsort adtál meg!");
                        Console.ReadKey();
                        Console.Clear();
                        EloadoOpcioValaszto();
                        break;
                }
            }

            while (!int.TryParse(Console.ReadLine(), out MenuSzam))
            {
                Console.WriteLine("Hibás formátumban adtad meg!");
                Console.ReadKey();
                Console.Clear();
                Lehetosegek();
            }

            Valasztas(MenuSzam);
        }









        #endregion

        #region Lemez

        static void LemezOpcioValaszto()
        {
            lemezek = new LemezController().Feltolt();
            int MenuSzam;

            Lehetosegek();

            static void Lehetosegek()
            {
                Console.WriteLine("Válassz a lehetőségek közül:");
                Console.WriteLine(
                       "1. Lemez kiírása\n" +
                       "2. Lemez módosítása\n" +
                       "3. Lemez felvétele\n" +
                       "4. Lemez törlése\n" +
                       "5. Vissza a táblákoz\n" +
                       "6. Kilépés");
                Console.Write("\nVálasztott menüpont: ");
            }

            static void Valasztas(int menu)
            {
                switch (menu)
                {
                    case 1:
                        LemezKiir();
                        break;
                    case 2:
                        LemezModosit();
                        break;
                    case 3:
                        LemezFelvesz();
                        break;
                    case 4:
                        LemezTorol();
                        break;
                    case 5:
                        Console.Clear();
                        TablaValaszto();
                        break;
                    case 6:
                        Console.Clear();
                        Console.WriteLine("Viszlát!");
                        Console.ReadKey();
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("Hibás számsort adtál meg!");
                        Console.ReadKey();
                        Console.Clear();
                        LemezOpcioValaszto();
                        break;
                }
            }

            while (!int.TryParse(Console.ReadLine(), out MenuSzam))
            {
                Console.WriteLine("Hibás formátumban adtad meg!");
                Console.ReadKey();
                Console.Clear();
                Lehetosegek();
            }

            Valasztas(MenuSzam);
        }



        static void LemezModosit()
        {
            static void Lehetosegek()
            {
                Console.Clear();
                Console.WriteLine("A lemezek listája:\n");
                for (int i = 0; i < lemezek.Count; i++)
                {
                    Console.WriteLine($"{lemezek[i]}");
                }
                Console.Write("Írd be a lemez számát amelyet módosítani szeretnél (0 az vissza): ");
            }


            int szam = -1;
            int[] szamok = new int[lemezek.Count];
            for (int i = 0; i < szamok.Length; i++)
            {
                szamok[i] = lemezek[i].Id;
            }


            Lehetosegek();
            while (!int.TryParse(Console.ReadLine(), out szam) || !szamok.Contains(szam))
            {
                if (szam == 0)
                {
                    Console.Clear();
                    LemezOpcioValaszto();
                }
                Lehetosegek();
            }
            Console.WriteLine(szam);

            if (szam == 0)
            {
                Console.Clear();
                LemezOpcioValaszto();
            }

            int sorokSzama = 0;
            try
            {
                Console.WriteLine("----------------------");
                Console.Write("Add meg az új címet: ");
                string cim = Console.ReadLine();
                Console.Write("Add meg az új kiadási évet: ");
                int kiadasEve = int.Parse(Console.ReadLine());
                Console.Write("Add meg az új kiadó nevét: ");
                string kiado = Console.ReadLine();

                sorokSzama = new LemezController().Modosit(szam, cim, kiadasEve, kiado);

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
                LemezOpcioValaszto();
            }
        }



        static void LemezTorol()
        {
            static void Lehetosegek()
            {
                Console.Clear();
                Console.WriteLine("A lemezek listája:\n");
                for (int i = 0; i < lemezek.Count; i++)
                {
                    Console.WriteLine($"{lemezek[i]}");
                }
                Console.Write("Írd be a lemez számát amelyet törölni szeretnél (0 az vissza): ");
            }

            int szam = -1;
            int[] szamok = new int[lemezek.Count];
            for (int i = 0; i < szamok.Length; i++)
            {
                szamok[i] = lemezek[i].Id;
                Console.WriteLine(szamok[i]);
            }


            Lehetosegek();
            while (!int.TryParse(Console.ReadLine(), out szam) || !szamok.Contains(szam))
            {
                if (szam == 0)
                {
                    Console.Clear();
                    LemezOpcioValaszto();
                }
                Lehetosegek();
            }


            int sorokSzama = 0;
            try
            {
                sorokSzama = new LemezController().Torles(szam);
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
                LemezOpcioValaszto();
            }

        }



        static void LemezKiir()
        {
            Console.Clear();
            Console.WriteLine("A lemezek listája:\n");
            foreach (var lemez in lemezek)
            {
                Console.WriteLine(lemez.ToString());
            }
            Console.WriteLine("\n(Nyomj egy gombot a folytatáshoz.)");
            Console.ReadKey();
            Console.Clear();
            LemezOpcioValaszto();
        }

        static void LemezFelvesz()
        {

            int sorokSzama = 0;
            try
            {
                sorokSzama = new LemezController().Felvesz(LemezAdatFelvetel());
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
                Console.Clear();
                LemezOpcioValaszto();
            }


        }

        private static Lemez LemezAdatFelvetel()
        {
            Console.Write("Add meg a lemez cimét: ");
            string cim = Console.ReadLine();
            Console.Write("Add meg a lemez kiadási évét: ");
            int kiadasEve = int.Parse(Console.ReadLine());
            Console.Write("Add meg a kiadó nevét: ");
            string kiado = Console.ReadLine();
            return new Lemez(cim, kiadasEve, kiado);
        }



        #endregion



        static void TablaValaszto()
        {
            static void TablakKiirasa()
            {
                Console.WriteLine("Válassz a táblák közül:\n" +
                                  "1. Előadó\n" +
                                  "2. Lemez\n" +
                                  "3. Kilépés");
            }

            static void Valaszto(int MenuSzam)
            {
                switch(MenuSzam)
                {
                    case 1:
                        EloadoTabla();
                        break;
                    case 2:
                        LemezTabla();
                        break;
                    case 3:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Hibás értéket adtál meg!");
                        Console.ReadKey();
                        Console.Clear();
                        TablaValaszto();
                        break;
                }
            }

            int MenuSzam;
            TablakKiirasa();
            while (!int.TryParse(Console.ReadLine(), out MenuSzam))
            {
                Console.WriteLine("Hibás formátumban adtad meg!");
                Console.ReadKey();
                Console.Clear();
                TablakKiirasa();
            }
            Valaszto(MenuSzam);
        }

        static void EloadoTabla()
        {
            Console.Clear();
            eloadok = new EloadoController().Feltolt();
            EloadoOpcioValaszto();

        }

        static void LemezTabla()
        {
            Console.Clear();
            lemezek = new LemezController().Feltolt();
            LemezOpcioValaszto();
        }

       
        static void Main(string[] args)
        {
            TablaValaszto();
            
            


            Console.ReadKey();
        }

      

    }
}
