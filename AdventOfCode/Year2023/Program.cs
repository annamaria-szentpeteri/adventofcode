using System.Text;
using Year2023.Exceptions;
using Year2023.Solutions;

namespace Year2023;

public class Program
{
    private static void Main()
    {
        try
        {
            while (true)
            {
                var dayNumber = GetDayNumber();
                var puzzleNumber = GetPuzzleNumber();
                bool useExampleInput = GetUseExampleInput();

                try
                {
                    SolvePuzzle(dayNumber, puzzleNumber, useExampleInput);
                    return;
                }
                catch (TypeWasNotFoundException)
                {
                    Console.WriteLine("Puzzle was not found. Day number must be between 01-24. Puzzle number must be 1 or 2.");
                }
                catch (InstanceCouldNotBeCreatedException)
                {
                    Console.WriteLine($"Could not create puzzle instance.");
                }
                catch (InstanceIsNotIPuzzleCreatedException)
                {
                    Console.WriteLine($"Puzzle instance does not implement IPuzzle interface.");
                }

                Console.WriteLine("------------------------------------------");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Unexpected error happened:");
            Console.WriteLine(e);
        }
    }

    private static bool GetUseExampleInput()
    {
        Console.WriteLine("Would you like to solve the puzzle againts the example input? (y/n)");
        return Console.ReadLine() == "y";
    }

    private static string? GetPuzzleNumber()
    {
        Console.WriteLine("Please provide if the 1st or the 2nd puzzle's result you want to see! (1 or 2)");
        return Console.ReadLine();
    }

    private static string? GetDayNumber()
    {
        Console.WriteLine("Please provide which day's puzzle's result you want to see! (number of day with 2 digits)");
        return Console.ReadLine();
    }

    private static void SolvePuzzle(string? dayNumber, string? puzzleNumber, bool useExampleInput)
    {
        var puzzle = CreatePuzzle(dayNumber, puzzleNumber, useExampleInput);

        var result = puzzle.SolvePuzzle();
        Console.WriteLine($"Solution for day {dayNumber} puzzle {puzzleNumber} is {result}");
    }

    private static IPuzzle CreatePuzzle(string? dayNumber, string? puzzleNumber, bool useExampleInput)
    {
        var inputFile = GetInputFilePath(dayNumber, useExampleInput);
        object? instance = GetPuzzleInstance(dayNumber, puzzleNumber, inputFile);

        if (instance == null) throw new InstanceCouldNotBeCreatedException();
        if (instance is not IPuzzle) throw new InstanceIsNotIPuzzleCreatedException();

        return (IPuzzle)instance;
    }

    private static object? GetPuzzleInstance(string? dayNumber, string? puzzleNumber, string inputFile)
    {
        var t = Type.GetType($"{typeof(IPuzzle).Namespace}.PuzzleDay{dayNumber}_{puzzleNumber}");
        return t == null 
            ? throw new TypeWasNotFoundException()
            : Activator.CreateInstance(t, new object[] { inputFile });
    }

    private static string GetInputFilePath(string? dayNumber, bool useExampleInput)
    {
        var stringBuilder = new StringBuilder();

        stringBuilder.Append(@"files\");

        if (useExampleInput)
            stringBuilder.Append(@"example_");

        stringBuilder.Append(@"inputs\day_");
        stringBuilder.Append(dayNumber);
        stringBuilder.Append(@".txt");

        return stringBuilder.ToString();
    }
}
