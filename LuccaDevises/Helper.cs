using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LuccaDevises
{
    public class Helper
    {

        public static Tuple<string, int, string> ReadFirstLine(string line)
        {
            List<string> elements = new List<string>();
            int currencyToInt;

            if (!string.IsNullOrEmpty(line))
            {
                elements.AddRange(line.Split(';'));
                if (elements.Count == 3)
                {
                    if (int.TryParse(elements[1], out currencyToInt))
                    {
                        return new Tuple<string, int, string>(elements[0], currencyToInt, elements[2]);
                    }
                }
            }
            return null;
        }

        public static int ReadSecondLine(string line)
        {
            int countRate;
            if (int.TryParse(line, out countRate))
            {
                return countRate;
            }
            return 0;
        }

        public static List<Tuple<string, string, decimal>> ReadOtherLines(List<string> lines)
        {
            List<Tuple<string, string, decimal>> result = new List<Tuple<string, string, decimal>>();

            foreach (var node in lines)
            {
                List<string> elements = new List<string>();

                if (!string.IsNullOrEmpty(node))
                {
                    elements.AddRange(node.Split(";"));
                    if (elements.Count == 3)
                    {
                        decimal valueConversion = 0;

                        if (elements[0].Length == 3 && elements[1].Length == 3)
                        {
                            if (decimal.TryParse(node.Split(";")[2].Replace('.', ','), out valueConversion))
                            {
                                Tuple<string, string, decimal> rateLine = new(elements[0], elements[1], valueConversion);
                                result.Add(rateLine);
                            }
                            else
                            {
                                return null;
                            }
                        }
                        else
                        {
                            return null;
                        }

                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            return result;
        }

    }
}
