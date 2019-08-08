using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Utlegg
{
    class Program
    {
        static int Main(string[] args)
        {
            switch (args.Length)
            {
                case 0:
                    PrintUsage();
                    return 0;
                default:
                    try
                    {
                        PrintFiles(args);
                        return 0;
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e);
                        return -1;
                    }
            }
        }

        private static void PrintFiles(string[] args)
        {
            var lines = ParseFiles(args);
            decimal sum = 0;
            foreach (var line in lines)
            {
                if (line.Amount >= 0)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }
                Console.WriteLine(line);
                Console.ResetColor();
                sum += line.Amount;
            }
            Console.WriteLine($"Total: {sum}");
        }

        private static IEnumerable<CsvLine> ParseFiles(string[] filenames)
        {
            return filenames
                .SelectMany(filename => File.ReadAllLines(filename)
                .Skip(1)
                .Select(CsvLine.Parse));
        }

        private static void PrintUsage()
        {
            Console.WriteLine("Usage:");
            Console.WriteLine("<file.csv> [file2.csv, ...]");
        }
    }
}