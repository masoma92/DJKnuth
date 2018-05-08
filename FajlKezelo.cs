using System;
using System.IO;
namespace DjKnuth
{
    public class FajlKezelo
    {
        
        Lista<ILejatszhato> lejatszhato;
        Lista<ILejatszhato> kulsoTabla;
        Lista<ILejatszhato> megadottStilusKigyujtve;
        int musorHossz;
        string[] stilusok = { "mulatós", "családi" };

        public Lista<ILejatszhato> Lejatszhato { get => lejatszhato; }
        public Lista<ILejatszhato> KulsoTabla { get => kulsoTabla; }
        public Lista<ILejatszhato> MegadottStilusKigyujtve { get => megadottStilusKigyujtve; }
        public string[] Stilusok { get => stilusok; }
        public int MusorHossz { get => musorHossz; set => musorHossz = value; }

        public FajlKezelo()
        {
            lejatszhato = new Lista<ILejatszhato>();
            Feltolt("tartalom.txt");
            kulsoTabla = lejatszhato.KulsoTablaLetrehozo();
            megadottStilusKigyujtve = StilusokKigyujtve(Stilusok);
        }

        private void Feltolt(string bemenet)
        {
            StreamReader sr = new StreamReader(bemenet, System.Text.Encoding.UTF8);
            string tartalom = "";
            while (!sr.EndOfStream)
            {
                tartalom += sr.ReadLine() + "$";
            }
            tartalom = tartalom.Remove(tartalom.Length - 1, 1);
            string[] adatok = tartalom.Split('$');
            for (int i = 0; i < adatok.Length; i++)
            {
                Vizsgalat(adatok[i]);
            }
            sr.Close();
        }

        private void Vizsgalat(string aktualisSor) //fájl beolvasása után feltölti a lejátszható listát
        {
            string[] sorTomb = aktualisSor.Split(';');
            if (double.Parse(sorTomb[2]) > 8)
            {
                lejatszhato.Beszur(new Film(sorTomb[0], int.Parse(sorTomb[1]), int.Parse(sorTomb[2]), sorTomb[3]));
            }
            else if (int.Parse(sorTomb[1]) > 0)
            {
                lejatszhato.Beszur(new Zene(sorTomb[0], int.Parse(sorTomb[1]), int.Parse(sorTomb[2]), sorTomb[3]));
            }
            else
            {
                lejatszhato.Beszur(new TorrentZene(sorTomb[0], int.Parse(sorTomb[1]), int.Parse(sorTomb[2]), sorTomb[3]));
            }
        }

        public Lista<ILejatszhato> StilusokKigyujtve(string[] stilusok)
        {
            Lista<ILejatszhato> kimenet = new Lista<ILejatszhato>();
            for (int i = 0; i < stilusok.Length; i++)
            {
                ILejatszhato elsoElofordulas = KulsoTabla.ElsoElofordulas(stilusok[i]);
                kimenet.Beszur(elsoElofordulas);
                lejatszhato.StilusokBeszurasa(elsoElofordulas, ref kimenet);
            }
            return kimenet;
        }

        public void StilusokKiir()
        {
            Console.Write("Megadott stílusok: ");
            for (int i = 0; i < stilusok.Length; i++)
            {
                Console.Write(stilusok[i] + ";");
            }
        }

    }
}
