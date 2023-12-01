namespace Year2023
{
    internal class Puzzle02 : IPuzzle<int>
    {
        private readonly Dictionary<string, int> _alphabeticNumbers = new (){
            { "one", 1},
            { "two", 2},
            { "three", 3},
            { "four", 4},
            { "five", 5},
            { "six", 6},
            { "seven", 7},
            { "eight", 8},
            { "nine", 9}
        };
        private readonly string _inputFileName = string.Empty;

        public Puzzle02(string inputFileName)
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

                line = SwapAlphabeticNumbersToDigits(line);

                char firstNumericChar = '0';
                bool foundFirst = false;

                char lastNumericChar = '0';
                bool foundLast = false;

                for (int i = 0; i < line.Length; i++)
                {
                    if (!foundFirst && char.IsDigit(line[i]))
                    {
                        firstNumericChar = line[i];
                        foundFirst = true;
                    }

                    if (!foundLast && char.IsDigit(line[line.Length - i - 1]))
                    {
                        lastNumericChar = line[line.Length - i - 1];
                        foundLast = true;
                    }

                    if (foundFirst && foundLast)
                    {
                        result += int.Parse(string.Concat(firstNumericChar, lastNumericChar));
                        break;
                    }
                }
            }

            return result;
        }

        private string SwapAlphabeticNumbersToDigits(string line)
        {
            var foundAt = new List<Tuple<string, int>>();

            do
            {
                foundAt.Clear();

                foreach (string s in _alphabeticNumbers.Keys)
                {
                    var i = line.IndexOf(s);

                    if (i != -1)
                    {
                        foundAt.Add(new Tuple<string, int>(s, i));
                    }
                }

                if (foundAt.Count > 0)
                {
                    var firstWord = foundAt.OrderBy(x => x.Item2).First();
                    var alphabeticNumber = firstWord.Item1;
                    var index = firstWord.Item2;
                    var number = _alphabeticNumbers[alphabeticNumber];

                    line = line[..(index + 1)] + number + line[(index - 1 + alphabeticNumber.Length)..];
                }
            }
            while (foundAt.Count > 0);

            return line;
        }
    }
}
