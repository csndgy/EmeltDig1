using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Utemezes;

class Program
{
    static List<Tabor> taborok;

    static void Main()
    {
        taborok = File.ReadAllLines("taborok.txt").Select(sor => new Tabor(sor)).ToList();
        Feladat2();
        Feladat3();
        Feladat4();
        Feladat6();
        Feladat7();
    }

    static void Feladat2()
    {
        Console.WriteLine("2. feladat:");
        Console.WriteLine($"Az adatsorok szama: {taborok.Count}");
        Console.WriteLine($"Az eloszor rogzitett tabor temaja: {taborok.First().Tema}");
        Console.WriteLine($"Az utoljara rogzitett tabor temaja: {taborok.Last().Tema}\n");
    }

    static void Feladat3()
    {
        Console.WriteLine("3. feladat:");
        var zeneiTaborok = taborok
            .Where(t => t.Tema == "zenei")
            .Select(t => $"{t.KezdHonap}. ho {t.KezdNap}. napjan")
            .ToList();

        if (zeneiTaborok.Any())
        {
            foreach (var datum in zeneiTaborok)
            {
                Console.WriteLine($"Zenei tabor kezdodik {datum}.\n");
            }
        }
        else
        {
            Console.WriteLine("Nem volt zenei tabor.\n");
        }
    }

    static void Feladat4()
    {
        Console.WriteLine("4. feladat:");
        int maxResztvevo = taborok.Max(t => t.Diakok.Length);

        var legnepszerubbTaborok = taborok
            .Where(t => t.Diakok.Length == maxResztvevo);

        Console.WriteLine("Legnepszerubbek:");
        foreach (var tabor in legnepszerubbTaborok)
        {
            Console.WriteLine($"{tabor.KezdHonap} {tabor.KezdNap} {tabor.Tema}\n");
        }
    }

    static void Feladat6()
    {
        Console.WriteLine("6. feladat:");
        Console.Write("Add meg a honapot: ");
        int honap = int.Parse(Console.ReadLine());
        Console.Write("Add meg a napot: ");
        int nap = int.Parse(Console.ReadLine());

        int bekertNapSorszam = Sorszam(honap, nap);

        int aktivTaborok = taborok.Count(t =>
            Sorszam(t.KezdHonap, t.KezdNap) <= bekertNapSorszam &&
            Sorszam(t.VegHonap, t.VegNap) >= bekertNapSorszam
        );

        Console.WriteLine($"Ekkor {aktivTaborok} tabor tart.");
    }

    static int Sorszam(int honap, int nap)
    {
        if (honap == 6) return nap - 15;
        if (honap == 7) return 30 + nap;
        if (honap == 8) return 30 + 31 + nap;
        return -1; // hibás hónap esetén
    }

    static void Feladat7()
    {
        Console.WriteLine("7. feladat:");
        Console.Write("Adja meg egy tanulo betujelét: ");
        char betujel = Console.ReadLine().Trim().ToUpper()[0];

        var tanuloTaborai = taborok
            .Where(t => t.Diakok.Contains(betujel))
            .OrderBy(t => Sorszam(t.KezdHonap, t.KezdNap))
            .ToList();

        using (StreamWriter sw = new StreamWriter("egytanulo.txt"))
        {
            foreach (var tabor in tanuloTaborai)
            {
                sw.WriteLine($"{tabor.KezdHonap}.{tabor.KezdNap}-{tabor.VegHonap}.{tabor.VegNap} {tabor.Tema}");
            }
        }
        bool vanAtfedes = false;
        for (int i = 0; i < tanuloTaborai.Count - 1; i++)
        {
            int aktualisVege = Sorszam(tanuloTaborai[i].VegHonap, tanuloTaborai[i].VegNap);
            int kovetkezoKezdete = Sorszam(tanuloTaborai[i + 1].KezdHonap, tanuloTaborai[i + 1].KezdNap);

            if (aktualisVege >= kovetkezoKezdete)
            {
                vanAtfedes = true;
                break;
            }
        }

        if (vanAtfedes)
        {
            Console.WriteLine("Nem mehet el mindegyik taborba.");
        }
        else
        {
            Console.WriteLine("Elmehet mindegyik taborba.");
        }
    }
}
