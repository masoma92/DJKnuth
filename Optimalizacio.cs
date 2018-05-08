using System;
namespace DjKnuth
{
    public class Optimalizacio
    {
        public delegate void ArvaltozasDelegate(ref Lista<ILejatszhato> bemenet);
        public delegate void ListaMegjelenitoDelegate(Lista<ILejatszhato> alapLista, Lista<ILejatszhato> aktulisMegoldas);
        public event ArvaltozasDelegate arvaltozas;
        public event ListaMegjelenitoDelegate listaMegjelenito;

        Lista<ILejatszhato> megadottStilusKigyujtve;
        int musorHossz;

        public Lista<ILejatszhato> MegadottStilusKigyujtve { get => megadottStilusKigyujtve; set => megadottStilusKigyujtve = value; }

        public Optimalizacio()
        {
            this.megadottStilusKigyujtve = new FajlKezelo().MegadottStilusKigyujtve;
        }

        public void Kerdes()
        {
            Console.WriteLine("\nMilyen hosszú műsort szeretnél?");
            musorHossz = int.Parse(Console.ReadLine());
        }
        
        public Lista<ILejatszhato> Optimalizalas()
        {
            Lista<ILejatszhato> kimenet = new Lista<ILejatszhato>();
            VisszalepesesKereses(0, ref kimenet);
            return kimenet;
        }
        private void VisszalepesesKereses(int szint, ref Lista<ILejatszhato> OPT)
        {
            int i = szint;
            Lista<ILejatszhato> E = new Lista<ILejatszhato>();
            while (i < megadottStilusKigyujtve.ElemSzam())
            {
                if (megadottStilusKigyujtve[i].Hossz <= musorHossz)
                {
                    if (E.OsszHossz() + megadottStilusKigyujtve[i].Hossz <= musorHossz)
                    {
                        E.Beszur(megadottStilusKigyujtve[i]);
                        if (szint != megadottStilusKigyujtve.ElemSzam() - 1)
                        {
                            VisszalepesesKereses(szint + 1, ref OPT);
                        }
                    }
                }
                i++;
            }
            if (E.OsszHossz() == OPT.OsszHossz() && E.OsszAr() <= OPT.OsszAr() || E.OsszHossz() > OPT.OsszHossz())
            {
                OPT = E;
                listaMegjelenito.Invoke(megadottStilusKigyujtve, OPT);
            }
        }

        public void ArvaltozasKerdes(ref Lista<ILejatszhato> valtoztatandoLista, Lista<ILejatszhato> valogatas)
        {
            Console.WriteLine("\nSzeretnél árat változtatni? Y/N");
            string YN = Console.ReadLine();
            if (YN == "Y")
            {
                arvaltozas.Invoke(ref valtoztatandoLista);
                valogatas = Optimalizalas();
            }
            else if(YN == "N")
            {
                Console.WriteLine("Nem változik semmi!");
            }
            else
            {
                Console.WriteLine("Rossz karaktert vittél be!");
                ArvaltozasKerdes(ref valtoztatandoLista, valogatas);
            }
        }
    }
}
