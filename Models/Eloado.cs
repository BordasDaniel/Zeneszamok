using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zeneszamok.Models
{
    public class Eloado
    {
        public int Id { get; set; }
        public string Nev { get; set; }
        public string? Nemzetiseg { get; set; }

        public bool Szolo { get; set; }

        public Eloado() { }
        public Eloado(string nev, string? nemzetiseg, bool szolo, int id=0)
        {
            Id = id;
            Nev = nev;
            Nemzetiseg = nemzetiseg;
            Szolo = szolo;
        }

        public override string ToString()
        {
            return $"- {Id}. {Nev} ({Nemzetiseg})";
        }

    }
}
