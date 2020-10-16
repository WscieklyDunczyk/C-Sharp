using System;
using System.Threading.Tasks.Sources;
using System.IO;
using System.Collections.Generic;
using System.Runtime;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace kolkokrzyzyk
{
    internal class Program
    {
        public static int[,] iPlansza = new int[3, 3];
        public static int iAktualnyGracz = 1;

        public static List<Player> list = new List<Player>();

        public static string dir = @"c:\temp";
        public static string pathFile = Path.Combine(dir, "statystyki.bin");

        public static int PodajInt(string Tekst, int iMin, int iMax)
        {
            while (true)
            {
                try
                {
                    Console.Write(Tekst);
                    int iValue = int.Parse(Console.ReadLine());
                    if (iValue < iMin || iValue > iMax)
                    {
                        throw new Exception("Wartość poza zakresem");
                    }
                    else return iValue;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        public static string PodajTekst(string Tekst)
        {
            while (true)
            {
                Console.Write(Tekst);
                string txt = Console.ReadLine();
                if (txt == "")
                {
                    Console.WriteLine("Wartość nie może być pusta");
                }
                else return txt;
            }
        }

        public static int[,] Czyszczenie_iPlansza(int[,] plansza)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    plansza[i, j] = 0;
                }
            }
            return plansza;
        }

        public static void Rysuj_iPlansza(int[,] plansza)
        {
            string wiersz;
            string znak;
            for (int i = 0; i < 3; i++)
            {
                wiersz = "";
                znak = "";
                for (int j = 0; j < 3; j++)
                {
                    if (plansza[i, j] == 0)
                    {
                        znak = " * ";
                    }
                    else if (plansza[i, j] == -1)
                    {
                        znak = " x ";
                    }
                    else if (plansza[i, j] == 1)
                    {
                        znak = " o ";
                    }

                    wiersz += znak;
                }
                Console.WriteLine(wiersz);
            }
        }

        public static int[,] Ruch_gracza(int[,] plansza, int iGracz)
        {
            while (true)
            {
                int iWiersz = PodajInt("wiersz: ", 1, 3);
                int iKolumna = PodajInt("kolumna: ", 1, 3);

                if (plansza[iWiersz - 1, iKolumna - 1] == 0)
                {
                    if (iGracz == 1)
                    {
                        plansza[iWiersz - 1, iKolumna - 1] = -1;
                        return plansza;
                    }
                    else if (iGracz == -1)
                    {
                        plansza[iWiersz - 1, iKolumna - 1] = 1;
                        return plansza;
                    }
                }
                else Console.WriteLine("Pole już zajęte!");
            }
        }

        public static void AktualnyGracz(int iAktualnyGracz, string gracz1, string znak1, string gracz2, string znak2)
        {
            if (iAktualnyGracz == 1)
            {
                Console.Write($"{gracz1}({znak1})");
            }
            else Console.Write($"{gracz2}({znak2})");
        }

        public static int Sprawdzenie(int iAktaulnyGracz, int iRuchGracza)
        {
            if (iRuchGracza > 9) return 0;
            for (int i = 0; i < 3; i++)
            {
                int iValue = 0;
                for (int j = 0; j < 3; j++)
                {
                    iValue += iPlansza[j, i];
                }
                if (iValue == 3 || iValue == -3)
                {
                    return 1;
                }
            }

            for (int j = 0; j < 3; j++)
            {
                int iValue = 0;
                for (int i = 0; i < 3; i++)
                {
                    iValue += iPlansza[j, i];
                }
                if (iValue == 3 || iValue == -3)
                {
                    return 1;
                }
            }

            if (iPlansza[0, 0] + iPlansza[1, 1] + iPlansza[2, 2] == iAktaulnyGracz * 3)
            {
                return 1;
            }
            if (iPlansza[2, 0] + iPlansza[1, 1] + iPlansza[0, 2] == iAktaulnyGracz * 3)
            {
                return 1;
            }
            return -1;
        }

        public static void Wczytywanie_pliku()
        {
            if (File.Exists(pathFile))
            {
                Console.WriteLine("Znaleziono plik");
                try
                {
                    using (Stream stream = File.Open(pathFile, FileMode.Open, FileAccess.Read))
                    {
                        var formatter = new BinaryFormatter();

                        list = (List<Player>)formatter.Deserialize(stream);
                        stream.Close();
                    }
                    Console.WriteLine("Wczytano plik");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine("Nie znaleziono pliku");
                Console.ReadKey();
            }
        }

        public static void Zapisywanie_pliku()
        {
            IFormatter formatter = new BinaryFormatter();

            if (!File.Exists(pathFile))
            {
                Stream stream = new FileStream(pathFile, FileMode.Create, FileAccess.Write);
                formatter.Serialize(stream, list);
                stream.Close();
            }
            else
            {
                Stream stream = new FileStream(pathFile, FileMode.Open, FileAccess.Write);
                formatter.Serialize(stream, list);
                stream.Close();
            }
        }

        public static void Gra()
        {
            Console.Clear();
            int iRuchGracza = 0;

            Player gracz1 = new Player(PodajTekst("Podaj nick dla gracza(x): "));
            gracz1.Znak = "x";
            Player gracz2 = new Player(PodajTekst("Podaj nick dla gracza(o): "));
            gracz2.Znak = "o";
            for (int i = 0; i < list.Count; i++)
            {
                Player gracz = list[i];
                if (gracz.Nazwa == gracz1.Nazwa)
                {
                    gracz1.Nazwa = gracz.Nazwa;
                    gracz1.IloscGier = gracz.IloscGier;
                    gracz1.IloscWygranych = gracz.IloscWygranych;
                    gracz1.IloscRemisow = gracz.IloscRemisow;
                    gracz1.Procent = gracz.Procent;
                    list.Remove(gracz);
                }
            }
            list.Add(gracz1);

            for (int i = 0; i < list.Count; i++)
            {
                Player gracz = list[i];
                if (gracz.Nazwa == gracz2.Nazwa)
                {
                    gracz2.Nazwa = gracz.Nazwa;
                    gracz2.IloscGier = gracz.IloscGier;
                    gracz2.IloscWygranych = gracz.IloscWygranych;
                    gracz2.IloscRemisow = gracz.IloscRemisow;
                    gracz2.Procent = gracz.Procent;
                    list.Remove(gracz);
                }
            }

            list.Add(gracz2);

            while (true)
            {
                Console.Clear();

                Console.Write($"Aktualny gracz ");
                AktualnyGracz(iAktualnyGracz, gracz1.Nazwa, gracz1.Znak, gracz2.Nazwa, gracz2.Znak);

                Console.WriteLine("");
                Rysuj_iPlansza(iPlansza);
                Console.WriteLine("");

                Ruch_gracza(iPlansza, iAktualnyGracz);
                iRuchGracza++;

                Console.Clear();
                if (Sprawdzenie(iAktualnyGracz, iRuchGracza) == 1)
                {
                    Rysuj_iPlansza(iPlansza);
                    Console.Write("Wygrywa ");
                    AktualnyGracz(iAktualnyGracz, gracz1.Nazwa, gracz1.Znak, gracz2.Nazwa, gracz2.Znak);

                    gracz1.DodajGry();
                    gracz2.DodajGry();

                    if (iAktualnyGracz == 1)
                    {
                        gracz1.DodajWygrane();
                    }
                    else gracz2.DodajWygrane();

                    gracz1.ProcentWygranych(gracz1.IloscWygranych, gracz1.IloscGier);
                    gracz2.ProcentWygranych(gracz2.IloscWygranych, gracz2.IloscGier);

                    Console.ReadKey();
                    break;
                }
                else if (iRuchGracza == 9)
                {
                    Rysuj_iPlansza(iPlansza);
                    Console.WriteLine($"Remis! {gracz1.Nazwa} i {gracz2.Nazwa}");
                    gracz1.DodajRemis();
                    gracz2.DodajRemis();
                    gracz1.DodajGry();
                    gracz2.DodajGry();
                    gracz1.ProcentWygranych(gracz1.IloscWygranych, gracz1.IloscGier);
                    gracz2.ProcentWygranych(gracz2.IloscWygranych, gracz2.IloscGier);
                    Console.ReadKey();
                    break;
                }

                Console.Clear();

                iAktualnyGracz *= -1;
            }
            Zapisywanie_pliku();
            Czyszczenie_iPlansza(iPlansza);
        }

        public static void Statystyki()
        {
            Console.Clear();
            Console.WriteLine("Lista graczy");
            var listCount = list.Count;

            foreach (var gracz in list)
            {
                Console.WriteLine($"- {gracz.Nazwa}");
            }
            string nazwa_gracza = PodajTekst("Podaj nazwe gracza: ");
            Console.Clear();
            for (int i = 0; i < list.Count; i++)
            {
                Player gracz = list[i];
                if (gracz.Nazwa == nazwa_gracza)
                {
                    Console.WriteLine($"Gracz: {gracz.Nazwa}\n " +
                        $"Ilość gier: {gracz.IloscGier}\n " +
                        $"Wygrane: {gracz.IloscWygranych}\n " +
                        $"Remisy: {gracz.IloscRemisow}\n " +
                        $"Procent wygranych: {gracz.Procent}%\n ");
                    break;
                }
                if (i + 1 == listCount)
                {
                    Console.WriteLine("Nie znaleziono gracza!");
                }
            }
            Console.ReadKey();
        }

        private static void Main(string[] args)
        {
            Wczytywanie_pliku();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("---Menu---");
                Console.WriteLine("1 - Nowa gra");
                Console.WriteLine("2 - Statystyki");
                Console.WriteLine("3 - Wyjście");

                switch (PodajInt("Wybierz opcje: ", 1, 3))
                {
                    case 1:
                        Gra();
                        break;

                    case 2:
                        Statystyki();
                        break;

                    case 3: return;
                }
            }
        }
    }
}