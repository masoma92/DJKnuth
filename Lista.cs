using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace DjKnuth
{
    public class Lista<T> where T : ILejatszhato
    {
        
        class ListaElem
        {
            public T adat;
            public ListaElem kovetkezo;

            public ListaElem(T adat)
            {
                this.adat = adat;
            }
        }

        ListaElem fej;

        public void Beszur(T adat)   //stílusok szerint feltölti a listát
        {
            ListaElem uj = new ListaElem(adat);
            if (fej == null)
            {
                fej = uj;
            }
            else
            {
                if (fej.adat.Stilus.CompareTo(uj.adat.Stilus) >= 0)
                {
                    uj.kovetkezo = fej;
                    fej = uj;
                }
                else
                {
                    ListaElem aktualis = fej;
                    ListaElem elozo = null;
                    while (aktualis != null && aktualis.adat.Stilus.CompareTo(uj.adat.Stilus) <= 0)
                    {
                        elozo = aktualis;
                        aktualis = aktualis.kovetkezo;
                    }
                    if (aktualis == null)
                    {
                        elozo.kovetkezo = uj;
                    }
                    else
                    {
                        uj.kovetkezo = aktualis;
                        elozo.kovetkezo = uj;
                    }
                }
            }
        }

        public void Torles(string torlendoCim) //cím alapján töröl
        {
            ListaElem aktualis = fej;
            ListaElem elozo = null;
            while (aktualis != null && !(aktualis.adat.Cim == torlendoCim))
            {
                elozo = aktualis;
                aktualis = aktualis.kovetkezo;
            }
            if (aktualis != null)
            {
                if (elozo != null)
                    elozo.kovetkezo = aktualis.kovetkezo;
                else
                    fej = aktualis.kovetkezo;
            }
        }

        public void Megjelenit() //consolera kiírja az elemeket
        {
            ListaElem aktualis = fej;
            while (aktualis != null)
            {
                if (aktualis.adat is Film)
                {
                    Console.WriteLine("Film: {0}, {1}, {2}, {3}", aktualis.adat.Cim, aktualis.adat.SzerzoiJogdij, aktualis.adat.Hossz, aktualis.adat.Stilus);
                }
                else if (aktualis.adat is Zene)
                {
                    Console.WriteLine("Zene: {0}, {1}, {2}, {3}", aktualis.adat.Cim, aktualis.adat.SzerzoiJogdij, aktualis.adat.Hossz, aktualis.adat.Stilus);
                }
                else
                {
                    Console.WriteLine("TorrentZene: {0}, {1}, {2}, {3}", aktualis.adat.Cim, aktualis.adat.SzerzoiJogdij, aktualis.adat.Hossz, aktualis.adat.Stilus);
                }
                aktualis = aktualis.kovetkezo;
            }
        }

        public Lista<T> KulsoTablaLetrehozo() //külső tábla létrehozása, ami referenciákat tartalmaz az egyes stílusok első elemére
        {
            Lista<T> kimenet = new Lista<T>();
            ListaElem aktualis;
            if (fej != null && fej.kovetkezo != null) //van-e két elem a listában
            {
                ListaElem betoltendo = fej;
                kimenet.Beszur(betoltendo.adat);
                aktualis = fej.kovetkezo;
                while (aktualis != null)
                {
                    if (aktualis.adat.Stilus != betoltendo.adat.Stilus)
                    {
                        betoltendo = aktualis;
                        kimenet.Beszur(betoltendo.adat);
                    }
                    aktualis = aktualis.kovetkezo;
                }
            }
            else if (fej!=null)
            {
                kimenet.Beszur(fej.adat);
            }
            return kimenet;
        }

        public T ElsoElofordulas(string stilus) //megkeresi az első olyan elemet ami a stílusnak megfelel, fájlkezelő hívja
        {
            if (fej != null)
            {
                ListaElem aktualis = fej;
                while (aktualis != null)
                {
                    if (aktualis.adat.Stilus == stilus)
                    {
                        return aktualis.adat;
                    }
                    aktualis = aktualis.kovetkezo;
                }
            }
            return default(T);
        }

        public void StilusokBeszurasa(T elem, ref Lista<T> kimenetLista)
        {
            if (fej != null)
            {
                ListaElem aktualis = fej;
                while (aktualis.adat.Stilus != elem.Stilus)
                {
                    aktualis = aktualis.kovetkezo;
                }
                string szuksegesStilus = aktualis.adat.Stilus;
                aktualis = aktualis.kovetkezo;
                while (aktualis.adat.Stilus == szuksegesStilus)
                {
                    kimenetLista.Beszur(aktualis.adat);
                    aktualis = aktualis.kovetkezo;
                }
            }
        }

        public T ElsoElofordulasCimSzerint(string cim) //megkeresi az első olyan elemet ami a címnek megfelel
        {
            if (fej != null)
            {
                ListaElem aktualis = fej;
                while (aktualis != null)
                {
                    if (aktualis.adat.Cim == cim)
                    {
                        return aktualis.adat;
                    }
                    aktualis = aktualis.kovetkezo;
                }
            }
            return default(T);
        }

        public int ElemSzam()
        {
            int elemSzam = 0;
            ListaElem aktualis = fej;
            while (aktualis != null)
            {
                elemSzam++;
                aktualis = aktualis.kovetkezo;
            }
            return elemSzam;
        }

        public T this[int index]
        {
            get
            {
                ListaElem aktualis = fej;
                if (index < 0)
                {
                    return default(T);
                }
                while (aktualis != null && index != 0)
                {
                    index--;
                    aktualis = aktualis.kovetkezo;
                }
                if (aktualis == null)
                {
                    return default(T);
                }
                return aktualis.adat;
            }
        }

        public int OsszHossz()
        {
            int osszIdo = 0;
            ListaElem aktualis = fej;
            while (aktualis != null)
            {
                osszIdo += aktualis.adat.Hossz;
                aktualis = aktualis.kovetkezo;
            }
            return osszIdo;
        }

        public int OsszAr()
        {
            int osszAr = 0;
            ListaElem aktualis = fej;
            while (aktualis != null)
            {
                osszAr += aktualis.adat.SzerzoiJogdij;
                aktualis = aktualis.kovetkezo;
            }
            return osszAr;
        }

    }
}

