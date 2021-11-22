using CurrencyService.Extensions;
using System;
using System.Text;
using System.Text.RegularExpressions;

namespace CurrencyService.Api
{
    public static class CurrencyConverter
    {
        private static readonly string[] ones = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen", };
        private static readonly string[] tens = { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };
        private static readonly string[] thous = { "hundred", "thousand", "million" };
        private static string cents;
        private static string dollars;

        public static string ConvertNumbersIntoWords(string currency)
        {
            try
            {
                var regex = new Regex(@"^([\d]{1,3} ([\d]{3} ){0,1}[\d]{3}|[\d]{1,3})(,[\d]{1,2})?");
                var result = regex.Match(currency);

                if (result.Value != currency)
                {
                    return "Wrong input";
                }

                var stringBuilder = new StringBuilder();
                (dollars, cents) = currency.Split(',');
                var dollarsArray = dollars.Split(' ');
                Array.Reverse(dollarsArray);

                if (cents.Length == 1) cents += '0';

                for (int i = 0; i < dollarsArray.Length; i++)
                {
                    stringBuilder.Insert(0, GetProperString(dollarsArray[i], i));
                }

                stringBuilder.Append(GetProperString(cents, -1));

                return stringBuilder.ToString();
            }
            catch (Exception)
            {
                //log ex
                return string.Empty;
            }
        }

        public static string GetProperString(string stringValue, int iteration)
        {
            try
            {
                if (string.IsNullOrEmpty(stringValue)) return stringValue;

                var stringBuilder = new StringBuilder();
                var currentValue = int.Parse(stringValue);

                if (currentValue == 0 && (!string.IsNullOrEmpty(cents) || iteration > 0)) return string.Empty;
                if (currentValue == 0 && string.IsNullOrEmpty(cents)) stringBuilder.Append($"{ones[currentValue]} ");

                var thousValue = currentValue / 100;
                var rest = currentValue % 100;
                var tensValue = rest / 10;
                var onesValue = rest % 10;
                string spaceOrDash = onesValue > 0 ? "-" : " ";
                string addOnes = onesValue > 0 ? $"{ones[onesValue]} " : "";
                string addHundreds = thousValue > 0 ? $"{ones[thousValue]} {thous[0]} " : "";
                string addTens = tensValue > 0 ? $"{tens[tensValue]}{spaceOrDash}" : "";
                string addOnesOrTensAndOnes = rest > 0 && rest < 20  ? $"{ones[rest]} " : $"{addTens}{addOnes}";
                stringBuilder.Append($"{addHundreds}{addOnesOrTensAndOnes}");

                if (iteration > 0 && currentValue > 0)
                {
                    stringBuilder.Append($"{thous[iteration]} ");
                }
                else if (iteration == 0)
                {
                    stringBuilder.Append($"dolars");
                    if (currentValue == 1)
                    {
                        stringBuilder.Length--;
                    }
                }
                else if (iteration == -1)
                {
                    var dol = int.Parse(dollars.Replace(" ", string.Empty));
                    if (dol > 0)
                    {
                        stringBuilder.Insert(0, " and ");
                    }
                    stringBuilder.Append($"cents");
                    if (currentValue == 1)
                    {
                        stringBuilder.Length--;
                    }
                }

                return stringBuilder.ToString();
            }
            catch (Exception)
            {
                //log ex
                return string.Empty;
            }
        }
    }
}
