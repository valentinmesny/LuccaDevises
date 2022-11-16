using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace LuccaDevises
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CurrencyPath path = new CurrencyPath();

            if (File.Exists(args[0]))
            {
                // Lecture du fichier
                List<string> lines = File.ReadAllLines(args[0]).ToList();
                // Récupération des données
                Tuple<string, int, string> firstLine = Helper.ReadFirstLine(lines[0]);
                int numberOfRate = Helper.ReadSecondLine(lines[1]);
                List<string> currencyTab = new List<string>(lines);
                currencyTab.RemoveRange(0, 2);

                if (firstLine != null)
                {
                    if (currencyTab.Count == numberOfRate)
                    {
                        List<Tuple<string, string, decimal>> rateLines = Helper.ReadOtherLines(currencyTab);
                        if(rateLines != null)
                        {
                            path = CurrencyPath.GetAllCurrencyPath(rateLines, path);

                            string shortestPath = path.GetShortestPath(firstLine.Item1, firstLine.Item3);

                            if (shortestPath.Count() > 0)
                            {
                                decimal currency = firstLine.Item2;
                                List<string> shortestPathPairs = CurrencyPath.GetPairFromPath(shortestPath);

                                foreach (var pair in shortestPathPairs)
                                {
                                    decimal rate = path.Rate[pair];
                                    currency = currency * rate;
                                }
                                Console.WriteLine((int)decimal.Round(currency));
                            }
                            else
                            {
                                Console.WriteLine("Ah ! Pas assez de taux de change pour convertir !");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Diantre ! Le tableau de change semble incorrect !");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Mince ! On m'indique " + numberOfRate + " taux de change !");
                    }
                }
                else
                {
                    Console.WriteLine("Oh ! La première ligne semble défectueuse !");
                }
            }
            else
            {
                Console.WriteLine("Le fichier est introuvable");
            }
            Console.ReadLine();
        }
    }
}