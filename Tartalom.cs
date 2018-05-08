using System;
namespace DjKnuth
{
    public abstract class Tartalom : ILejatszhato
    {
        string cim;
        int szerzoiJogdij;
        int hossz;
        string stilus;

        public string Cim { get => cim; }
        public int SzerzoiJogdij { get => szerzoiJogdij; set => szerzoiJogdij = value; }
        public int Hossz { get => hossz; }
        public string Stilus { get => stilus; }

        public Tartalom(string cim, int szerzoiJogdij, int hossz, string stilus)
        {
            this.cim = cim;
            this.szerzoiJogdij = szerzoiJogdij;
            this.hossz = hossz;
            this.stilus = stilus;
        }



    }
}
