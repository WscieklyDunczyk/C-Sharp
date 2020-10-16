using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace kolkokrzyzyk
{
    [Serializable]
    public class Player
    {
        public string Nazwa;
        public string Znak;
        public int IloscGier;
        public int IloscWygranych;
        public int IloscRemisow;
        public int Procent;

        public Player(string Nazwa)
        {
            this.Nazwa = Nazwa;
        }

        public void ProcentWygranych(int wygrane, int gry) => Procent = wygrane / gry * 100;

        public void DodajGry()
        {
            IloscGier++;
        }

        public void DodajWygrane()
        {
            IloscWygranych++;
        }

        public void DodajRemis()
        {
            IloscRemisow++;
        }
    }
}