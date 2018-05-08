using System;
using System.Collections.Generic;

namespace DjKnuth
{
    class MainClass
    {
        static public void ArvaltozasLetrehoz(ref Lista<ILejatszhato> valtoztatandoLista)
        {
            Console.WriteLine("Mi a címe a dalnak?");
            string bekertCim = Console.ReadLine();
            ILejatszhato valtoztatandoElem = valtoztatandoLista.ElsoElofordulasCimSzerint(bekertCim);
            if (valtoztatandoElem is Zene)
            {
                valtoztatandoLista.Torles(bekertCim);
                Console.WriteLine("Mennyire szeretnéd változtatni az árat?");
                int ar = int.Parse(Console.ReadLine());
                valtoztatandoElem.SzerzoiJogdij = ar;
                valtoztatandoLista.Beszur(valtoztatandoElem);
            }
            else
            {
                Console.WriteLine("Nem zenecímet adtál meg!");
                ArvaltozasLetrehoz(ref valtoztatandoLista);
            }

        }

        static public void ValtozasKiir(Lista<ILejatszhato> alapLista, Lista<ILejatszhato> aktulisLista)
        {
            System.Threading.Thread.Sleep(100);
            Console.Clear();
            Console.WriteLine("Az eredeti lista: ");
            alapLista.Megjelenit();
            Console.WriteLine("\nA műsor: ");
			aktulisLista.Megjelenit();
        }
		
        public static void Main(string[] args)
        {
            FajlKezelo F = new FajlKezelo();

            Lista<ILejatszhato> lista = F.Lejatszhato;
            Lista<ILejatszhato> stilusokKigyujtve = F.KulsoTabla;
            Lista<ILejatszhato> megadottStilusKigyujtve = F.MegadottStilusKigyujtve;

            Lista<ILejatszhato> musorLista = new Lista<ILejatszhato>();
            Lista<ILejatszhato> megvaltozottLista = new Lista<ILejatszhato>();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Eredeti lista: ");
            Console.ResetColor();
            lista.Megjelenit();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nStílusok első elemei:");
            Console.ResetColor();
            stilusokKigyujtve.Megjelenit();

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            F.StilusokKiir();
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nMegadott stílusok szerinti lista:");
            Console.ResetColor();
            megadottStilusKigyujtve.Megjelenit();
            
            Optimalizacio O = new Optimalizacio();
            O.listaMegjelenito += ValtozasKiir;
            O.Kerdes();
            Lista<ILejatszhato> valogatas = O.Optimalizalas();

            O.arvaltozas += ArvaltozasLetrehoz;
            O.MegadottStilusKigyujtve = F.MegadottStilusKigyujtve;
            O.ArvaltozasKerdes(ref megadottStilusKigyujtve, valogatas);



        }
    }
}
