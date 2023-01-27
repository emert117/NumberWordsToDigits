using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NumberWordsToDigits.Business
{
    public class NumbersToWordsConverter : INumbersToWordsConverter
    {
        public string ConvertNumberWordsToDigits(string text)
        {
            ArgumentNullException.ThrowIfNull(text);
            text = text.Replace("billions", "billion")
                .Replace("millions", "million")
                .Replace("thousands", "thousand")
                .Replace("hundreds", "hundred")
                .Trim();

            string pattern = @"\b(zero|one|two|three|four|five|six|seven|eight|nine|ten|eleven|twelve|thirteen|fourteen|fifteen|sixteen|seventeen|eighteen|nineteen|twenty|thirty|forty|fifty|sixty|seventy|eighty|ninety|hundred|thousand|million|billion)\b";
            MatchCollection matches = Regex.Matches(text, pattern, RegexOptions.IgnoreCase);

            List<string> groups = new List<string>();
            for(int i=0; i<matches.Count; i++)
            {
                // check the next match(es) if the number continues
                int matchStart = i;
                bool matchContinues = false;
                int matchEnd = 0;
                for (int j = i; j < matches.Count-1; j++)
                {
                    if (matches[j].Index + matches[j].Length + 1 == matches[j + 1].Index)
                    {
                        matchContinues = true;
                        matchEnd = j + 1;
                        i = matchEnd;
                    }
                    else
                    {
                        break;
                    }
                }

                if (matchContinues)
                {
                    string group= string.Empty;
                    for (int k = matchStart; k <= matchEnd; k++)
                    {
                        group += matches[k].Value + " ";
                    }
                    groups.Add(group);
                }
                else
                {
                    groups.Add(matches[i].Value);
                }
            }

            foreach (var group in groups)
            {
                string word = WordsToNumbers(group).ToString();
                text = text.Replace(group.Trim(), word);
            }

            return text;
        }

        // https://www.programmingalgorithms.com/algorithm/words-to-numbers/
        public static ulong WordsToNumbers(string words)
        {
            if (string.IsNullOrEmpty(words)) return 0;

            words = words.Trim();
            words += ' ';

            ulong number = 0;
            string[] singles = new string[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
            string[] teens = new string[] { "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
            string[] tens = new string[] { "", "", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };
            string[] powers = new string[] { "", "thousand", "million", "billion", "trillion", "quadrillion", "quintillion" };

            for (int i = powers.Length - 1; i >= 0; i--)
            {
                if (!string.IsNullOrEmpty(powers[i]))
                {
                    int index = words.IndexOf(powers[i]);

                    if (index >= 0 && words[index + powers[i].Length] == ' ')
                    {
                        ulong count = WordsToNumbers(words.Substring(0, index));
                        number += count * (ulong)Math.Pow(1000, i);
                        words = words.Remove(0, index);
                    }
                }
            }

            {
                int index = words.IndexOf("hundred");

                if (index >= 0 && words[index + "hundred".Length] == ' ')
                {
                    ulong count = WordsToNumbers(words.Substring(0, index));
                    number += count * 100;
                    words = words.Remove(0, index);
                }
            }

            for (int i = tens.Length - 1; i >= 0; i--)
            {
                if (!string.IsNullOrEmpty(tens[i]))
                {
                    int index = words.IndexOf(tens[i]);

                    if (index >= 0 && words[index + tens[i].Length] == ' ')
                    {
                        number += (uint)(i * 10);
                        words = words.Remove(0, index);
                    }
                }
            }

            for (int i = teens.Length - 1; i >= 0; i--)
            {
                if (!string.IsNullOrEmpty(teens[i]))
                {
                    int index = words.IndexOf(teens[i]);

                    if (index >= 0 && words[index + teens[i].Length] == ' ')
                    {
                        number += (uint)(i + 10);
                        words = words.Remove(0, index);
                    }
                }
            }

            for (int i = singles.Length - 1; i >= 0; i--)
            {
                if (!string.IsNullOrEmpty(singles[i]))
                {
                    int index = words.IndexOf(singles[i] + ' ');

                    if (index >= 0 && words[index + singles[i].Length] == ' ')
                    {
                        number += (uint)(i);
                        words = words.Remove(0, index);
                    }
                }
            }

            return number;
        }
    }
}
