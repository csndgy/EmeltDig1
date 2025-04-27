namespace autokmozgasa
{
    internal class Jeladas
    {
        string rendszam;
        int ora, perc, sebesseg;

        public Jeladas(string sor)
        {
            var adatok = sor.Split('\t');
            rendszam = adatok[0];
            ora = int.Parse(adatok[1]);
            perc = int.Parse(adatok[2]);
            sebesseg = int.Parse(adatok[3]);
        }


        public string Rendszam { get => rendszam; set => rendszam = value; }
        public int Ora { get => ora; set => ora = value; }
        public int Perc { get => perc; set => perc = value; }
        public int Sebesseg { get => sebesseg; set => sebesseg = value; }

        public string Idopont()
        {
            return $"{ora}:{perc:D2}";
        }
    }
}
