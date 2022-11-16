using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuccaDevises
{
    public class CurrencyPath
    {
        public List<string> Path { get; set; }
        public Dictionary<string, decimal> Rate { get; set; }

        public CurrencyPath() 
        { 
            Path = new List<string>();
            Rate = new Dictionary<string, decimal>();
        }
        public string GetShortestPath(string firstCurrency, string lastCurrency)
        {
            var path = Path
                .Where(x => x.StartsWith(firstCurrency) && x.EndsWith(lastCurrency))
                .OrderBy(o => o.Length)
                .First();

            return path ?? string.Empty;
        }
        public static List<string> GetPairFromPath(string path)
        {
            List<string> result = new List<string>();
            List<string> pair = new List<string>();
            pair.AddRange(path.Split("to"));
            for (int i = 0; i < pair.Count - 1; i++)
            {
                result.Add(pair[i] + "to" + pair[i + 1]);
            }

            return result;
        }

        public static CurrencyPath GetAllCurrencyPath(List<Tuple<string, string, decimal>> lineCurrencies, CurrencyPath currencyPath)
        {
            foreach(var line in lineCurrencies)
            {
                string firstSymbolCurrency = line.Item1;
                string lastSymbolCurrency = line.Item2;
                decimal rateCurrency = line.Item3;

                if ((!currencyPath.Path.Contains(firstSymbolCurrency + "to" + lastSymbolCurrency)))
                {
                    List<string> newPaths = new List<string>();

                    if (currencyPath.Path.Count > 0)
                    {
                        newPaths.AddRange(currencyPath.Path.Where(x => x.StartsWith(firstSymbolCurrency)).Select(y => lastSymbolCurrency + "to" + y));
                        newPaths.AddRange(currencyPath.Path.Where(x => x.EndsWith(firstSymbolCurrency)).Select(y => y + "to" + lastSymbolCurrency));
                        newPaths.AddRange(currencyPath.Path.Where(x => x.StartsWith(lastSymbolCurrency)).Select(y => firstSymbolCurrency + "to" + y));
                        newPaths.AddRange(currencyPath.Path.Where(x => x.StartsWith(lastSymbolCurrency)).Select(y => y + "to" + firstSymbolCurrency));
                        currencyPath.Path.AddRange(newPaths);
                    }
                    currencyPath.Path.Add(firstSymbolCurrency + "to" + lastSymbolCurrency);
                    currencyPath.Path.Add(lastSymbolCurrency + "to" + firstSymbolCurrency);
                    currencyPath.Rate.Add(firstSymbolCurrency + "to" + lastSymbolCurrency, rateCurrency);
                    currencyPath.Rate.Add(lastSymbolCurrency + "to" + firstSymbolCurrency, decimal.Round(1 / rateCurrency, 4));
                }
            }
            return currencyPath;
        }
    }
}