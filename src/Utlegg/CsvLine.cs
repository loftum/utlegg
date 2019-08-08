using System;
using System.Globalization;

namespace Utlegg
{
    class CsvLine
    {
        public string Date { get; private set; }
        public string Type { get; private set; }
        public string Description { get; private set; }
        public decimal Amount { get; private set; }

        public static CsvLine Parse(string input)
        {
            try
            {
                var parts = input.Split('\t');
                if (parts.Length < 4)
                {
                    throw new Exception($"Invalid row: '{input}'");
                }
                return new CsvLine
                {
                    Date = parts[0],
                    Type = parts[1],
                    Description = parts[2],
                    Amount = decimal.Parse(parts[3], System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture)
                };
            }
            catch
            {
                Console.WriteLine($"Failed parsing {input}");
                throw;
            }
        }

        public override string ToString()
        {
            return $"{Date} {Type} {Description} {Amount}";
        }
    }
}
