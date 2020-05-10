using System;
using System.Numerics;
using System.Diagnostics;

namespace Projekt_2
{
    internal class Program
    {
        private static BigInteger[] tab = { 100913, 1009139, 10091401, 100914061, 1009140611, 10091406133, 100914061337, 1009140613399 };
        private static BigInteger licznik;

        private static bool IsPrime_Czas(BigInteger n)
        {
            if (n < 2) return false;
            else if (n < 4) return true;
            else if (n % 2 == 0) return false;
            else for (BigInteger u = 3; u < n / 2; u += 2)
                    if (n % u == 0) return false;
            return true;
        }

        private static bool IsPrimie_instrumetacja(BigInteger n)
        {
            if (n < 2) return false;
            else if (n < 4) { return true; }
            else if (n % 2 == 0) return false;
            else
            {
                licznik = 1;
                for (BigInteger u = 3; u < n / 2; u += 2)
                {
                    licznik++;
                    if (n % u == 0) return false;
                }
            }

            return true;
        }

        private static bool IsPrimie_instrumetacja_usprawniony(BigInteger n)
        {
            if (n < 2) return false;
            else if (n < 4) return true;
            else if (n % 2 == 0) return false;
            else
            {
                licznik = 1;
                for (BigInteger u = 3; u * u < n; u += 2)
                {
                    licznik++;
                    if (n % u == 0) return false;
                }
            }

            return true;
        }

        private static bool IsPrime_czas_usprawniony(BigInteger n)
        {
            if (n < 2) return false;
            else if (n < 4) return true;
            else if (n % 2 == 0) return false;
            else for (BigInteger u = 3; u * u < n / 2; u += 2)
                    if (n % u == 0) return false;
            return true;
        }

        private static void Pokaz_czas()
        {
            for (int i = 0; i < tab.Length; i++)
            {
                long ticks = 0;
                Stopwatch st = new Stopwatch();
                st.Reset();
                st.Start();
                IsPrime_Czas(tab[i]);
                st.Stop();
                ticks = st.ElapsedTicks;
                Console.WriteLine($"{ticks}, {tab[i]}");
            }
        }

        private static void Pokaz_instrumetacje()
        {
            licznik = 0;
            for (int i = 0; i < tab.Length; i++)
            {
                IsPrimie_instrumetacja(tab[i]);
                Console.WriteLine($"{licznik}, {tab[i]}");
            }
        }

        private static void Pokaz_instrumentacja_usprawniony()
        {
            licznik = 0;
            for (int i = 0; i < tab.Length; i++)
            {
                IsPrimie_instrumetacja_usprawniony(tab[i]);
                Console.WriteLine($"{licznik}, {tab[i]}");
            }
        }

        private static void Pokaz_czas_usprawniony()
        {
            long ticks = 0;
            for (int i = 0; i < tab.Length; i++)
            {
                Stopwatch st = new Stopwatch();
                st.Reset();
                st.Start();
                IsPrime_czas_usprawniony(tab[i]);
                st.Stop();
                ticks = st.ElapsedTicks;
                Console.WriteLine($"{ticks}, {tab[i]}");
            }
        }

        private static void Main(string[] args)
        {
            for (int i = 0; i < 10; i++)
            {
                Pokaz_czas_usprawniony();
            }
        }
    }
}