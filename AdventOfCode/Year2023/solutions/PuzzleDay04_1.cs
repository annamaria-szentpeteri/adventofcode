namespace Year2023.Solutions;

internal class PuzzleDay04_1 : IPuzzle
{
    private readonly string _inputFileName = string.Empty;

    public PuzzleDay04_1(string inputFileName)
    {
        _inputFileName = inputFileName;
    }

    public int SolvePuzzle()
    {
        var result = 0;
        
        using var file = File.OpenText(_inputFileName);

        while (!file.EndOfStream)
        {
            var line = file.ReadLine();

            if (string.IsNullOrEmpty(line))
                continue;

            var winningNumbers = GetWinningNumbers(line);

            if (winningNumbers.Count != 0)
                result += (int)Math.Pow(2, winningNumbers.Count - 1);
        }

        return result;
    }

    private static List<string> GetWinningNumbers(string input)
    {
        var splitOptions = StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries;

        var cardNumbers = input.Split(':', splitOptions)[1].Split('|', splitOptions);

        var possibleWinningNumbers = cardNumbers[0].Split(' ', splitOptions);
        var scratchedNumbers = cardNumbers[1].Split(' ', splitOptions);

        return scratchedNumbers.Intersect(possibleWinningNumbers).ToList();
    }
}
