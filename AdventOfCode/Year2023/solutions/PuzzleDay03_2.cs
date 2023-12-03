using System.Text.RegularExpressions;

namespace Year2023.Solutions;

internal class PuzzleDay03_2 : IPuzzle
{
    private readonly string _inputFileName = string.Empty;

    public PuzzleDay03_2(string inputFileName)
    {
        _inputFileName = inputFileName;
    }

    public int SolvePuzzle()
    {
        var result = 0;

        using var file = File.OpenText(_inputFileName);

        var text = file.ReadLine()!;
        var dimension = text.Length + Environment.NewLine.Length;
        text += Environment.NewLine + file.ReadToEnd().Trim().ReplaceLineEndings();

        var numbers = Regex.Matches(text, @"[\d]+").Select(x => new Tuple<int, string>(x.Index, x.Value)).ToList();
        var gears = Regex.Matches(text, @"[\*]+").Select(x => x.Index).ToList();

        gears.ForEach(g =>
        {
            var neighbourNumbers = GetNeighbourNumbers(g, text, dimension, numbers);

            if (neighbourNumbers.Count == 2)
            {
                var ratio = 1;
                neighbourNumbers.ForEach(n => ratio *= int.Parse(n.Item2));
                result += ratio;
            }
        });

        return result;
    }

    private static List<Tuple<int, string>> GetNeighbourNumbers(int index, string text, int dimension, List<Tuple<int, string>> numbers)
    {
        var length = 1;

        var startingIndexMiddleLeft = (index % dimension) == 0 ? index : index - 1;
        var endingIndexMiddleRight = ((index + length) % dimension) == 0 ? index + length : index + length + 1;

        var startingIndexTopLeft = (startingIndexMiddleLeft - dimension) < 0 ? startingIndexMiddleLeft : startingIndexMiddleLeft - dimension;
        var endingIndexTopRight = (endingIndexMiddleRight - dimension) < 0 ? endingIndexMiddleRight : endingIndexMiddleRight - dimension;

        var startingIndexBottomLeft = (startingIndexMiddleLeft + dimension) >= text.Length ? startingIndexMiddleLeft : startingIndexMiddleLeft + dimension;
        var endingIndexBottomRight = (endingIndexMiddleRight + dimension) > text.Length ? endingIndexMiddleRight : endingIndexMiddleRight + dimension;

        return numbers.Where(n =>
        {
            var index = n.Item1;
            var lastIndex = n.Item1 + n.Item2.Length - 1;

            return (startingIndexMiddleLeft <= index && index < endingIndexMiddleRight)
                || (startingIndexMiddleLeft <= lastIndex && lastIndex < endingIndexMiddleRight)
                || (startingIndexTopLeft <= index && index < endingIndexTopRight)
                || (startingIndexTopLeft <= lastIndex && lastIndex < endingIndexTopRight)
                || (startingIndexBottomLeft <= index && index < endingIndexBottomRight)
                || (startingIndexBottomLeft <= lastIndex && lastIndex < endingIndexBottomRight);
        })
        .ToList();
    }
}
