using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utemezes
{
    internal class Tabor
    {
        public int KezdHonap { get; set; }
        public int KezdNap { get; set; }
        public int VegHonap { get; set; }
        public int VegNap { get; set; }
        public string Diakok { get; set; }
        public string Tema { get; set; }

        public Tabor(string sor)
        {
            var adatok = sor.Split('\t');
            KezdHonap = int.Parse(adatok[0]);
            KezdNap = int.Parse(adatok[1]);
            VegHonap = int.Parse(adatok[2]);
            VegNap = int.Parse(adatok[3]);
            Diakok = adatok[4];
            Tema = adatok[5];
        }

    }
}
