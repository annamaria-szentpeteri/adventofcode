namespace Year2023.Solutions;

internal class PuzzleDay04_2 : IPuzzle
{
    private readonly string _inputFileName = string.Empty;

    public PuzzleDay04_2(string inputFileName)
    {
        _inputFileName = inputFileName;
    }

    public int SolvePuzzle()
    {
        var result = 0;
        var cardNumber = 0;

        var extraCopies = new Dictionary<int, int>();

        using var file = File.OpenText(_inputFileName);

        while (!file.EndOfStream)
        {
            var line = file.ReadLine();

            if (string.IsNullOrEmpty(line))
                continue;

            cardNumber++;

            var numberOfCards = 1;

            if (extraCopies.TryGetValue(cardNumber, out var numberOfCopies))
            {
                numberOfCards += numberOfCopies;
            }

            result += numberOfCards;

            var winningNumberCount = GetWinningNumbers(line).Count;

            if (winningNumberCount == 0)
                continue;

            for (int c = 1; c <= numberOfCards; c++)
            {
                for (int i = 1; i <= winningNumberCount; i++)
                {
                    var number = cardNumber + i;
                    if (extraCopies.TryGetValue(number, out var copyCount))
                    {
                        extraCopies[number] = copyCount + 1;
                    }
                    else
                    {
                        extraCopies.Add(number, 1);
                    }
                }
            }
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
