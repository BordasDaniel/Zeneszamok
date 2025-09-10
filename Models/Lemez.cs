using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Zeneszamok.Models
{
    public class Lemez
    {
        public int Id { get; set; }
        public string Cim { get; set; }
        public int KiadasEve { get; set; }
        public string Kiado { get; set; }

        public Lemez() { }

        public Lemez(string cim, int kiadasEve, string kiado, int id = 0)
        {
            Id = id;
            Cim = cim;
            KiadasEve = kiadasEve;
            Kiado = kiado;
        }

        public override string ToString()
        {
            return $"- {Id} - {Kiado} - {Cim} ({KiadasEve})";
        }


    }
}
