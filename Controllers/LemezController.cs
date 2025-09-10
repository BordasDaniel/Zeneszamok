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
    public class LemezController
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
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hiba történt az adatbázishoz való csatlakozás során: {ex}");
            }
        }

        public int Modosit(int id, string cim, int kiadasEve, string kiado)
        {
            int sorokSzama = 0;
            try
            {
                SqlConnection.Open();
                string sql = "UPDATE `lemez` SET `Cim`= @cim,`KiadasEve`= @kiadasEve,`Kiado`= @kiado WHERE Id = @id";
                MySqlCommand cmd = new(sql, SqlConnection);
                cmd.Parameters.AddWithValue("@cim", cim);
                cmd.Parameters.AddWithValue("@kiadasEve", kiadasEve);
                cmd.Parameters.AddWithValue("@kiado", kiado);
                cmd.Parameters.AddWithValue("@id", id);
                sorokSzama = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hiba történt a módosítás során: {0}", ex);
            }
            finally
            {
                SqlConnection.Close();
            }
            return sorokSzama;

        }

        public int Torles(int id)
        {
            int sorokSzama = 0;

            try
            {
                SqlConnection.Open();
                string sql = "DELETE FROM `lemez` WHERE Id = @id";
                MySqlCommand cmd = new(sql, SqlConnection);
                cmd.Parameters.AddWithValue("@id", id);
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

        public int Felvesz(Lemez rogzitendo)
        {
            int sorokSzama = 0;
            try
            {
                SqlConnection.Open();
                string sql = "INSERT INTO `lemez`(`Cim`, `KiadasEve`, `Kiado`) VALUES (@cim,@kiadasEve, @Kiado)";
                MySqlCommand cmd = new(sql, SqlConnection);
                cmd.Parameters.AddWithValue("@cim", rogzitendo.Cim);
                cmd.Parameters.AddWithValue("@kiadasEve", rogzitendo.KiadasEve);
                cmd.Parameters.AddWithValue("@kiado", rogzitendo.Kiado);
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

        public List<Lemez> Feltolt()
        {
            BuildConnection();
            List<Lemez> lemezLocal = [];
            try
            {
                SqlConnection.Open();
                string sql = "SELECT * FROM lemez";
                MySqlCommand cmd = new(sql, SqlConnection);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Lemez lemez = new();
                    lemez.Id = reader.GetInt32("Id");
                    lemez.Cim = reader.GetString("Cim");
                    lemez.KiadasEve = reader.GetInt32("KiadasEve");
                    
                    if (!reader.IsDBNull(3))
                    {
                        lemez.Kiado = reader.GetString("Kiado");
                    }
                    else
                    {
                        lemez.Kiado = null;
                    }
                    lemezLocal.Add(lemez);

                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Adatbázis hiba: {ex}");
            }
            finally
            {
                SqlConnection.Close();
            }
            return lemezLocal;
        }


    }




}

