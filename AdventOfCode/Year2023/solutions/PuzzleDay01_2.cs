namespace Year2023.Solutions;

internal class PuzzleDay01_2 : IPuzzle
{
    private readonly Dictionary<string, char> _digitMapping = new(){
        { "one", '1'},
        { "two", '2'},
        { "three", '3'},
        { "four", '4'},
        { "five", '5'},
        { "six", '6'},
        { "seven", '7'},
        { "eight", '8'},
        { "nine", '9'},
        { "1", '1'},
        { "2", '2'},
        { "3", '3'},
        { "4", '4'},
        { "5", '5'},
        { "6", '6'},
        { "7", '7'},
        { "8", '8'},
        { "9", '9'}
    };

    private readonly string _inputFileName = string.Empty;

    public PuzzleDay01_2(string inputFileName)
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

            char firstDigit = FindFirstDigit(line);
            char lastDigit = FindLastDigit(line);

            result += GetNumberFromDigits(firstDigit, lastDigit);
        }

        return result;
    }

    private static int GetNumberFromDigits(char firstDigit, char secondDigit)
    {
        return int.Parse(string.Concat(firstDigit, secondDigit));
    }

    private char FindFirstDigit(string line)
    {
        int position = line.Length;
        string number = string.Empty;

        foreach (string key in _digitMapping.Keys)
        {
            var index = line.IndexOf(key);

            if (index != -1 && index < position)
            {
                position = index;
                number = key;
            }
        }

        return GetDigit(number);
    }

    private char FindLastDigit(string line)
    {
        int position = -1;
        string number = string.Empty;

        foreach (string key in _digitMapping.Keys)
        {
            var index = line.LastIndexOf(key);

            if (index != -1 && index > position)
            {
                position = index;
                number = key;
            }
        }

        return GetDigit(number);
    }

    private char GetDigit(string number)
    {
        return _digitMapping[number];
    }
}
