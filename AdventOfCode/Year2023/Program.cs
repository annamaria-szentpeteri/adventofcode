// See https://aka.ms/new-console-template for more information

using Year2023;

try
{
    var success = false;

    while (!success)
    {
        Console.WriteLine("Please provide which puzzle's result you want to see! (number of puzzle)");
        success = int.TryParse(Console.ReadLine(), out var puzzleNumber);

        if (!success)
        {
            Console.WriteLine("Invalid input.");
            continue;
        }

        switch (puzzleNumber)
        {
            case 1:
                {
                    var puzzle = new Puzzle01("input01.txt");
                    var solution = puzzle.SolvePuzzle();
                    Console.WriteLine($"Solution for puzzle #{puzzleNumber} is {solution}");
                    break;
                }
            case 2:
                {
                    var puzzle = new Puzzle02("input01.txt");
                    var solution = puzzle.SolvePuzzle();
                    Console.WriteLine($"Solution for puzzle #{puzzleNumber} is {solution}");
                    break;
                }
            default:
                {
                    Console.WriteLine("Invalid input. Number must be between 1-24.");
                    success = false;
                    break;
                }
        }
    }
}
catch (Exception e)
{
    Console.WriteLine("Unexpected error happened:");
    Console.WriteLine(e);
}
