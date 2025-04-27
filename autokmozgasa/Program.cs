using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace autokmozgasa
{
    internal class Program
    {
        public static List<Jeladas> jeladasok { get; set; }

        static void Main(string[] args)
        {
            jeladasok = File.ReadAllLines("ido.txt")
                .Select(sor => new Jeladas(sor))
                .ToList();

            Feladat2();
            Feladat3();
            Feladat4();
            Feladat5();
            Feladat6();
            Feladat7();
        }

        public static void Feladat2()
        {
            Console.WriteLine("2. feladat:");
            var utolso = jeladasok.Last();
            Console.WriteLine($"Legutolso jeladas: {utolso.Idopont()}, rendszam: {utolso.Rendszam}");
        }

        public static void Feladat3()
        {
            Console.WriteLine("3. feladat:");
            var elsoRendszam = jeladasok.First().Rendszam;
            var idopontok = jeladasok
                .Where(j => j.Rendszam == elsoRendszam)
                .Select(j => j.Idopont());
            Console.WriteLine($"Elso jarmu rendszama: {elsoRendszam}");
            Console.WriteLine(string.Join(" ", idopontok));
        }

        public static void Feladat4()
        {
            Console.WriteLine("4. feladat:");
            Console.Write("Adja meg az ora erteket: ");
            int ora = int.Parse(Console.ReadLine());
            Console.Write("Adja meg a perc erteket: ");
            int perc = int.Parse(Console.ReadLine());

            int darab = jeladasok.Count(j => j.Ora == ora && j.Perc == perc);
            Console.WriteLine($"{darab} jeladas tortent ebben az idopontban.");
        }

        public static void Feladat5()
        {
            Console.WriteLine("5. feladat:");
            int maxSebesseg = jeladasok.Max(j => j.Sebesseg);
            var rendszamok = jeladasok
                .Where(j => j.Sebesseg == maxSebesseg)
                .Select(j => j.Rendszam);

            Console.WriteLine($"A legnagyobb sebesseg: {maxSebesseg} km/h");
            Console.WriteLine(string.Join(" ", rendszamok));
        }

        public static void Feladat6()
        {
            Console.WriteLine("6. feladat:");
            Console.Write("Adja meg a jarmu rendszamat: ");
            string keresettRendszam = Console.ReadLine().ToUpper();

            var jarmu = jeladasok.Where(j => j.Rendszam == keresettRendszam).OrderBy(j => j.Ora * 60 + j.Perc).ToList();

            if (jarmu.Count == 0)
            {
                Console.WriteLine("Ilyen rendszamu jarmu nem volt.");
                return;
            }

            double tavolsag = 0.0;
            Console.WriteLine("Idopont Tavolsag");
            Console.WriteLine($"{jarmu[0].Idopont()} 0,0 km");

            for (int i = 1; i < jarmu.Count; i++)
            {
                int elteltPerc = (jarmu[i].Ora * 60 + jarmu[i].Perc) - (jarmu[i - 1].Ora * 60 + jarmu[i - 1].Perc);
                double elteltOra = elteltPerc / 60.0;
                tavolsag += elteltOra * jarmu[i - 1].Sebesseg;
                Console.WriteLine($"{jarmu[i].Idopont()} {tavolsag:F1} km");
            }
        }

        public static void Feladat7()
        {
            Console.WriteLine("7. feladat:");
            var rendszamok = jeladasok.GroupBy(j => j.Rendszam);

            using (StreamWriter sw = new StreamWriter("ido2.txt"))
            {
                foreach (var csoport in rendszamok)
                {
                    var elso = csoport.First();
                    var utolso = csoport.Last();
                    sw.WriteLine($"{csoport.Key} {elso.Ora} {elso.Perc} {utolso.Ora} {utolso.Perc}");
                }
            }

            Console.WriteLine("Az ido2.txt fajl letrejott.");
        }
    }
}
