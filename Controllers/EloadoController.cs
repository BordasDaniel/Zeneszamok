using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zeneszamok.Models;

namespace Zeneszamok.Controllers
{
    public class EloadoController
    {
        static MySqlConnection SqlConnection = new();

        private static void BuildConnection()
        {
            try
            {
                var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false, true)
                .Build();

                string connectionString = config.GetConnectionString("Default");

                SqlConnection.ConnectionString = connectionString;
            } catch (Exception ex)
            {
                Console.WriteLine($"Hiba történt az adatbázishoz való csatlakozás során: {ex}");
            }
            
        }

        public int Modosit(int szam, string nev, string nemzetiseg, bool szolo)
        {
            int sorokSzama = 0;
            try
            {
                SqlConnection.Open();
                string sql = "UPDATE `eloado` SET `Nev`= @nev,`Nemzetiseg`= @nemzetiseg,`Szolo`= @szolo WHERE Id = @id";
                MySqlCommand cmd = new(sql, SqlConnection);
                cmd.Parameters.AddWithValue("@nev", nev);
                cmd.Parameters.AddWithValue("@nemzetiseg", nemzetiseg);
                cmd.Parameters.AddWithValue("@szolo", szolo);
                cmd.Parameters.AddWithValue("@id", szam);
                sorokSzama = cmd.ExecuteNonQuery();
            } catch (Exception ex)
            {
                Console.WriteLine("Hiba történt a módosítás során: {0}", ex);
            }
            finally
            {
                SqlConnection.Close();
            }
            return sorokSzama;
           
        }

        public int Torles(int szam)
        {
            int sorokSzama = 0;
            
            try
            {
                SqlConnection.Open();
                string sql = "DELETE FROM `eloado` WHERE Id = @id";
                MySqlCommand cmd = new(sql, SqlConnection);
                cmd.Parameters.AddWithValue("@id", szam);
                sorokSzama = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hiba történt a törlés során {0}", ex);
            }
            finally 
            {
                SqlConnection.Close(); 
            }
          
            return sorokSzama;
        }

        public int Felvesz(Eloado rogzitendo)
        {
            int sorokSzama = 0;
            try
            {
                SqlConnection.Open();
                string sql = "INSERT INTO `eloado`(`Nev`, `Nemzetiseg`, `Szolo`) VALUES (@nev,@nemzetiseg, @szolo)";
                MySqlCommand cmd = new(sql, SqlConnection);
                cmd.Parameters.AddWithValue("@nev", rogzitendo.Nev);
                cmd.Parameters.AddWithValue("@nemzetiseg", rogzitendo.Nemzetiseg);
                cmd.Parameters.AddWithValue("@szolo", rogzitendo.Szolo);
                sorokSzama = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hiba történt a felvétel során: {0}", ex);
            }
            finally
            {
                SqlConnection.Close();
            }
            
            return sorokSzama;
        }

        public List<Eloado> Feltolt()
        {
            BuildConnection();
            List<Eloado> eloadokLocal = [];
            try
            {
                SqlConnection.Open();
                string sql = "SELECT * FROM eloado";
                MySqlCommand cmd = new(sql, SqlConnection);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Eloado eloado = new();
                    eloado.Id = reader.GetInt32("Id");
                    eloado.Nev = reader.GetString("Nev");

                    if (!reader.IsDBNull(2))
                    {
                        eloado.Nemzetiseg = reader.GetString("Nemzetiseg");
                    }
                    else
                    {
                        eloado.Nemzetiseg = null;
                    }

                    eloado.Szolo = reader.GetBoolean("Szolo");

                    eloadokLocal.Add(eloado);

                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Adatbázis hiba: {ex}");
            }

            SqlConnection.Close();
            return eloadokLocal;
        }


    }


}
