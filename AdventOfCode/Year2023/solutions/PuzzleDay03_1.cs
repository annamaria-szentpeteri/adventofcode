using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace Year2023.Solutions;

internal class PuzzleDay03_1 : IPuzzle
{
    private readonly string _inputFileName = string.Empty;

    public PuzzleDay03_1(string inputFileName)
    {
        _inputFileName = inputFileName;
    }

    public int SolvePuzzle()
    {
        var result = 0;

        using var file = File.OpenText(_inputFileName);

        var text = file.ReadLine()!;
        var dimension = text.Length;
        text += Environment.NewLine + file.ReadToEnd().Trim().ReplaceLineEndings();

        var numbers = Regex.Matches(text, @"[\d]+").Select(x => new Tuple<int, string>(x.Index, x.Value));
        
        foreach(var number in numbers)
        {
            result += IsPartNumber(number.Item1, number.Item2.Length, dimension + Environment.NewLine.Length, text) ? int.Parse(number.Item2) : 0; 
        }

        return result;
    }

    private bool IsPartNumber(int index, int length, int dimension, string text)
    {
        var startingIndexMiddleLeft = (index % dimension) == 0 ? index : index - 1;
        var endingIndexMiddleRight = ((index + length) % dimension) == 0 ? index + length : index + length + 1;

        var startingIndexTopLeft = (startingIndexMiddleLeft - dimension) < 0 ? startingIndexMiddleLeft : startingIndexMiddleLeft - dimension;
        var endingIndexTopRight = (endingIndexMiddleRight - dimension) < 0 ? endingIndexMiddleRight : endingIndexMiddleRight - dimension;

        var startingIndexBottomLeft = (startingIndexMiddleLeft + dimension) >= text.Length ? startingIndexMiddleLeft : startingIndexMiddleLeft + dimension;
        var endingIndexBottomRight = (endingIndexMiddleRight + dimension) > text.Length ? endingIndexMiddleRight : endingIndexMiddleRight + dimension;

        var neighbours = text[startingIndexTopLeft..endingIndexTopRight] + text[startingIndexMiddleLeft..endingIndexMiddleRight] + text[startingIndexBottomLeft..endingIndexBottomRight];

        return Regex.Match(neighbours, @"[^\d\.\n\r]").Success;
    }
}
