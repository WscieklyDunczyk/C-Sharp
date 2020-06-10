using System;
using System.Diagnostics;
using System.Net;

namespace algorytmy_sort
{
    internal class Program
    {
        private static int[] tablica_posortowana_r(int size)
        {
            int[] tab = new int[size];
            for (int i = 0; i < size; i++)
            {
                tab[i] = i;
            }
            return tab;
        }

        private static int[] tablica_posortowana_m(int size)
        {
            int[] tab = new int[size];
            for (int i = 0; i < tab.Length; i++)
            {
                tab[i] = -1;
            }
            return tab;
        }

        private static int[] tablica_stala(int size)
        {
            int[] tab = new int[size];
            for (int i = 0; i < tab.Length; i++)
            {
                tab[i] = 1;
            }
            return tab;
        }

        private static int[] tablica_losowa(int size)
        {
            int[] tab = new int[size];
            Random random = new Random();

            for (int i = 0; i < tab.Length; i++)
            {
                tab[i] = random.Next(int.MaxValue);
            }
            return tab;
        }

        private static int[] pobierz_tablice_losowa(int[] random, int size)
        {
            int[] tab = new int[size];
            for (int i = 0; i < size; i++)
            {
                tab[i] = random[i];
            }
            return tab;
        }

        public static int[] tablica_v(int size)
        {
            int[] tab = new int[size];

            int x = size / 2;

            for (int i = 0; i < size; i++)
            {
                if (i < size / 2)
                    tab[i] = x--;
                else
                    tab[i] = x++;
            }

            return tab;
        }

        public static int[] tablica_a(int size)
        {
            int[] tab = new int[size];
            int srodek = size / 2;
            for (int i = 0; i < size; i++)
            {
                if (i < size / 2)
                    tab[i] = srodek++;
                else
                    tab[i] = srodek--;
            }

            return tab;
        }

        public static void SelectionSort(int[] a_oTab)
        {
            uint k;
            for (uint i = 0; i < (a_oTab.Length - 1); i++)
            {
                int temp = a_oTab[i];
                k = i;
                for (uint j = i + 1; j < a_oTab.Length; j++)
                    if (a_oTab[j] < temp)
                    {
                        k = j;
                        temp = a_oTab[j];
                    }

                a_oTab[k] = a_oTab[i];
                a_oTab[i] = temp;
            }
        }

        public static void InsertionSort(int[] tab)
        {
            for (uint i = 1; i < tab.Length; i++)
            {
                uint j = i;
                int temp = tab[j];

                while ((j > 0) && (tab[j - 1] > temp))
                {
                    tab[j] = tab[j - 1];
                    j--;
                }

                tab[j] = temp;
            }
        }

        public static void CocktailSort(int[] tab)
        {
            int left = 1, right = tab.Length - 1, k = tab.Length - 1;
            do
            {
                for (int j = right; j >= left; j--)
                    if (tab[j - 1] > tab[j])
                    {
                        int temp = tab[j - 1];
                        tab[j - 1] = tab[j];
                        tab[j] = temp;
                        k = j;
                    }

                left = k + 1;
                for (int j = left; j <= right; j++)
                    if (tab[j - 1] > tab[j])
                    {
                        int temp = tab[j - 1];
                        tab[j - 1] = tab[j];
                        tab[j] = temp;
                        k = j;
                    }

                right = k - 1;
            } while (left <= right);
        }

        public static void HeapSort(int[] tab)
        {
            int left = tab.Length / 2;
            int right = tab.Length - 1;
            while (left > 0)
            {
                left--;
                heapify(ref tab, left, right);
            }

            while (right > 0)
            {
                int temp = tab[left];
                tab[left] = tab[right];
                tab[right] = temp;
                right--;
                heapify(ref tab, left, right);
            }
        }

        public static void heapify(ref int[] tab, int left, int right)
        {
            int i = left,
                j = 2 * i + 1;
            int temp = tab[i];
            while (j <= right)
            {
                if (j < right)
                    if (tab[j] < tab[j + 1])
                        j++;
                if (temp >= tab[j]) break;
                tab[i] = tab[j];
                i = j;
                j = 2 * i + 1;
            }

            tab[i] = temp;
        }

        public static void QuickSort_i(int[] tab)
        {
            int i, j, l, r, sp;
            int[] stackLeft = new int[tab.Length];
            int[] stackRight = new int[tab.Length];
            Random random = new Random();
            sp = 0;
            stackLeft[sp] = 0;
            stackRight[sp] = tab.Length - 1;

            do
            {
                l = stackLeft[sp];
                r = stackRight[sp];
                sp--;

                do
                {
                    int x;
                    i = l;
                    j = r;
                    //x = tab[(l + r) / 2];
                    //x = tab[random.Next(l, r + 1)];
                    x = tab[r];
                    do
                    {
                        while (tab[i] < x) i++;
                        while (x < tab[j]) j--;
                        if (i <= j)
                        {
                            int buf = tab[i];
                            tab[i] = tab[j];
                            tab[j] = buf;
                            i++;
                            j--;
                        }
                    } while (i <= j);

                    if (i < r)
                    {
                        sp++;
                        stackLeft[sp] = i;
                        stackRight[sp] = r;
                    }

                    r = j;
                } while (l < r);
            } while (sp >= 0);
        }

        public static void QuickSort_r(int[] tab)
        {
            Sort(tab, 0, tab.Length - 1);
        }

        public static void Sort(int[] tab, int left, int right)
        {
            //Random random = new Random();
            int i, j, x;
            i = left;
            j = right;
            //x = tab[right];
            //x = tab[random.Next(left, right + 1)];
            x = tab[(left + right) / 2];
            do
            {
                while (tab[i] < x) i++;
                while (x < tab[j]) j--;
                if (i < j)
                {
                    int buf = tab[i];
                    tab[i] = tab[j];
                    tab[j] = buf;
                    i++;
                    j--;
                }
            } while (i < j);

            if (left < j) Sort(tab, left, j);
            if (i < right) Sort(tab, i, right);
        }

        public static void Main(string[] args)
        {
            //int[] baseTab = tablica_a(200_000);
            // int[] tab = pobierz_tablice_losowa(baseTab, 100);
            //int[] tab = tablica_a(200_000);

            Console.WriteLine("InsertionSort");
            int[] tab = tablica_posortowana_r(200_000);
            Stopwatch st = new Stopwatch();
            for (int i = 50_000; i <= 200_000; i += 5_000)
            {
                tab = tablica_posortowana_r(i);
                st.Reset();
                st.Start();
                InsertionSort(tab);
                st.Stop();
                long time = st.ElapsedTicks;
                Console.WriteLine($"{time}");
            }

            tab = tablica_posortowana_r(200_000);
            Console.WriteLine("malejaca");
            for (int i = 50_000; i <= 200_000; i += 5_000)
            {
                tab = tablica_posortowana_m(i);
                st.Reset();
                st.Start();
                InsertionSort(tab);
                st.Stop();
                long time = st.ElapsedTicks;
                Console.WriteLine($"{time}");
            }

            tab = tablica_posortowana_r(200_000);
            Console.WriteLine("stala");
            for (int i = 50_000; i <= 200_000; i += 5_000)
            {
                tab = tablica_stala(i);
                st.Reset();
                st.Start();
                InsertionSort(tab);
                st.Stop();
                long time = st.ElapsedTicks;
                Console.WriteLine($"{time}");
            }

            tab = tablica_posortowana_r(200_000);
            Console.WriteLine("losowa");
            for (int i = 50_000; i <= 200_000; i += 5_000)
            {
                tab = tablica_losowa(i);
                st.Reset();
                st.Start();
                InsertionSort(tab);
                st.Stop();
                long time = st.ElapsedTicks;
                Console.WriteLine($"{time}");
            }

            tab = tablica_posortowana_r(200_000);
            Console.WriteLine("v");
            for (int i = 50_000; i <= 200_000; i += 5_000)
            {
                tab = tablica_v(i);
                st.Reset();
                st.Start();
                InsertionSort(tab);
                st.Stop();
                long time = st.ElapsedTicks;
                Console.WriteLine($"{time}");
            }
            /* Console.WriteLine("QuickSort last element");
             for (int i = 50_000; i <= 200_000; i += 5_000)
             {
                 tab = tablica_a(i);
                 st.Reset();
                 st.Start();
                 partition(tab, 0, tab.Length - 1);
                 st.Stop();
                 long time = st.ElapsedTicks;
                 Console.WriteLine($"{i}, {time} ");
             }*/
            /*
            for (int i = 50_000; i <= 200_000; i += 5_000)
            {
                tab = pobierz_tablice_losowa(baseTab, i);
                st.Reset();
                st.Start();
                QuickSort_i(tab);
                st.Stop();
                long time = st.ElapsedTicks;
                Console.WriteLine($"{i}, {time} ");
            }

            tab = pobierz_tablice_losowa(baseTab, 100);
            Console.WriteLine("QuickSort r");
            for (int i = 50_000; i <= 200_000; i += 5_000)
            {
                tab = pobierz_tablice_losowa(baseTab, i);
                st.Reset();
                st.Start();
                QuickSort_r(tab);
                st.Stop();
                long time = st.ElapsedTicks;
                Console.WriteLine($"{i}, {time} ");
            }*/

            /* Console.WriteLine("SelectionSort");
             for (int i = 50_000; i <= 200_000; i += 5_000)
             {
                 tab = pobierz_tablice_losowa(baseTab, i);
                 st.Reset();
                 st.Start();
                 SelectionSort(tab);
                 st.Stop();
                 long time = st.ElapsedTicks;
                 Console.WriteLine($"{time} ");
             }
             tab = pobierz_tablice_losowa(baseTab, 200_000);
             Console.WriteLine("InsertionSort");
             for (int i = 50_000; i <= 200_000; i += 5_000)
             {
                 tab = pobierz_tablice_losowa(baseTab, i);
                 st.Reset();
                 st.Start();
                 InsertionSort(tab);
                 st.Stop();
                 long time = st.ElapsedTicks;
                 Console.WriteLine($"{time} ");
             }
             tab = pobierz_tablice_losowa(baseTab, 200_000);
             Console.WriteLine("InsertionSort");
             for (int i = 50_000; i <= 200_000; i += 5_000)
             {
                 tab = pobierz_tablice_losowa(baseTab, i);
                 st.Reset();
                 st.Start();
                 CocktailSort(tab);
                 st.Stop();
                 long time = st.ElapsedTicks;
                 Console.WriteLine($"{time} ");
             }
             tab = pobierz_tablice_losowa(baseTab, 200_000);
             Console.WriteLine("HeapSort");
             for (int i = 50_000; i <= 200_000; i += 5_000)
             {
                 tab = pobierz_tablice_losowa(baseTab, i);
                 st.Reset();
                 st.Start();
                 HeapSort(tab);
                 st.Stop();
                 long time = st.ElapsedTicks;
                 Console.WriteLine($"{time} ");
             }*/
        }
    }
}