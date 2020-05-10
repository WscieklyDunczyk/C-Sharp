using System;
using System.Diagnostics;

namespace Projekt_1
{
    class Program
    {
        static int[] TestVector;
        static int counter;
        const int liczba_testow = 100;
        static int[] GenerateSortedIntTable(int size)
        {
            int[] r = new int[size];
            for (int i = 0; i < r.Length; i++)
            {
                r[i] = i;
            }

            return r;
        }
        static int szukaj_bin_instrumentacje(int[] tab, int szukany_n)
        {
            int prawy = tab.Length - 1;
            int lewy = 0;
            while (lewy <= prawy)
            {
                counter++;
                int sr = (prawy + lewy) / 2;
                if (tab[sr] == szukany_n)
                {
                    counter++;
                    return sr;
                }
                else if (tab[sr] > szukany_n)
                {
                    counter++;
                    prawy = sr - 1;
                }
                else
                {
                    counter++;
                    lewy = sr + 1;
                }
            }
            return -1;
        }
        static int szukaj_bin_czas(int[] tab, int szukany_n)
        {
            int prawy = tab.Length - 1;
            int lewy = 0;
            while (lewy <= prawy)
            {
                int sr = (prawy + lewy) / 2;
                if (tab[sr] == szukany_n) return sr;
                else if (tab[sr] > szukany_n) prawy = sr - 1;
                else lewy = sr + 1;
            }
            return -1;
        }
        static bool szukaj_liniowo_czas(int[] tab, int szukany_n)
        {
            for (int i = 0; i < tab.Length; i++)
            {
                if (tab[i] == szukany_n)
                {
                    return true;
                }
            }
            return false;
        }
        static bool szukaj_liniowo_instr(int[] tab, int szukany_n)
        {
            for (int i = 0; i < tab.Length; i++)
            {
                counter++;
                if (tab[i] == szukany_n) return true;
            }
            return false;
        }
        static void Bin_pesymistyczny_czas()
        {
            long ticks, total_ticks = 0;
            for (int i = 0; i < TestVector.Length; i++)
            {
                Stopwatch st = new Stopwatch();
                st.Reset();
                st.Start();
                szukaj_bin_czas(TestVector, TestVector.Length);
                st.Stop();
                ticks = st.ElapsedTicks;
                total_ticks += ticks;
            }
            Console.WriteLine(total_ticks);
        }
        static void Bin_pesymistyczny_instr()
        {
            counter = 0;
            szukaj_bin_instrumentacje(TestVector, TestVector.Length - 1);
            Console.WriteLine(counter);
        }
        static void Liniowy_pesymistyczny_czas()
        {
            Stopwatch st = new Stopwatch();
            st.Reset();
            st.Start();
            szukaj_liniowo_czas(TestVector, TestVector.Length);
            st.Stop();
            long ticks = st.ElapsedTicks;
            Console.WriteLine(ticks);
        }
        static void Liniowy_pesymistyczny_instr()
        {
            counter = 0;
            szukaj_liniowo_instr(TestVector, TestVector.Length);
            Console.WriteLine(counter);
        }
        static void Liniowy_sredni_instr()
        {
            counter = 0;
            long counter_pierwszy = 0;
            long counter_ostatni = 0;

            szukaj_liniowo_instr(TestVector, 0);
            counter_pierwszy = counter;

            counter = 0;

            szukaj_liniowo_instr(TestVector, TestVector.Length);
            counter_ostatni = counter;

            Console.WriteLine($"{(counter_pierwszy + counter_ostatni) / 2}");
        }
        static void Liniowy_sredni_czas()
        {
            Stopwatch st = new Stopwatch();
            st.Reset();
            st.Start();
            szukaj_liniowo_czas(TestVector, 0);
            st.Stop();
            long ticks_poczatek = st.ElapsedTicks;

            st.Reset();
            st.Start();
            szukaj_liniowo_czas(TestVector, TestVector.Length - 1);
            st.Stop();
            long ticks_koniec = st.ElapsedTicks;

            Console.WriteLine($" {(ticks_poczatek + ticks_koniec) / 2}");
        }
        static void Bin_sredni_czas()
        {
            /* long ticks, total_ticks = 0, avg_ticks = 0;
             for (int i = 0; i < liczba_testow; i++)
             {
                 Stopwatch st = new Stopwatch();
                 st.Reset();
                 st.Start();
                 szukaj_bin_czas(TestVector, TestVector.Length - 1);
                 st.Stop();
                 ticks = st.ElapsedTicks;
                 total_ticks += ticks;
             }
             avg_ticks = total_ticks / liczba_testow;
             Console.WriteLine(avg_ticks);
             */
            Stopwatch st = new Stopwatch();
            TestVector = GenerateSortedIntTable((int)Math.Pow(2, 28));
            st.Reset();
            st.Start();
            foreach (var t in TestVector)
            {
                szukaj_bin_czas(TestVector, t);
            }

            st.Stop();
            long totalTime = st.ElapsedTicks;
            Console.WriteLine($"Total exec time: {totalTime}, average search time: {totalTime / TestVector.Length}");
        }
        static void Bin_sredni_instr()
        {
            int total_count = 0;
            for (int i = 0; i < liczba_testow; i++)
            {
                counter = 0;
                szukaj_bin_instrumentacje(TestVector, i);
                total_count += counter;
            }
            Console.WriteLine($"{total_count / liczba_testow}");
        }
        static void Main(string[] args)
        {
            for (int i = 2000000; i <= Math.Pow(2, 28); i += 7000000)
            {
                Console.Write($"{i}, ");
                TestVector = new int[i];
                for (int x = 0; x < TestVector.Length; ++x)
                    TestVector[x] = x;
                Liniowy_pesymistyczny_czas();
                //Liniowy_pesymistyczny_instr();
                //Bin_pesymistyczny_czas();
                //Bin_pesymistyczny_instr();
                //Liniowy_sredni_instr();
                // Liniowy_sredni_czas();
                Bin_sredni_czas();
                //Bin_sredni_instr();
            }

            Console.WriteLine("koniec");
            Console.ReadKey();
        }
    }
}
