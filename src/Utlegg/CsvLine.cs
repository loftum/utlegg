using System;
using System.Globalization;

namespace Utlegg;

class CsvLine
{
    public bool IsEmpty { get; private init; }
    public string Date { get; private init; }
    public string Type { get; private init; }
    public string Description { get; private init; }
    public decimal Amount { get; private init; }

    public static CsvLine Parse(string input, int lineNumber)
    {
        try
        {
            var parts = input.Split(';');
            if (parts.Length < 8)
            {
                return Empty;
            }

            var minus = decimal.TryParse(parts[6].Replace(',', '.'), NumberStyles.Any, NumberFormatInfo.InvariantInfo, out var m) ? m : 0;
            var plus = decimal.TryParse(parts[7].Replace(',', '.'), NumberStyles.Any, NumberFormatInfo.InvariantInfo, out var p) ? p : 0;
            
            return new CsvLine
            {
                Date = parts[0].Trim('"'),
                Type = parts[4].Trim('"'),
                Description = parts[5].Trim('"'),
                Amount = plus + minus
            };
        }
        catch
        {
            Console.WriteLine($"Failed parsing line {lineNumber}. Input: '{input}'");
            throw;
        }
    }

    public static readonly CsvLine Empty = new CsvLine{ Description = "Empty", IsEmpty = true };

    public override string ToString()
    {
        return IsEmpty ? "--- EMPTY ---" : $"{Date} {Type} {Description} {Amount}";
    }
}