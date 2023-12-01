namespace Year2023
{
    internal class Puzzle02 : IPuzzle<int>
    {
        private readonly Dictionary<string, char> _alphabeticNumbers = new (){
            { "one", '1'},
            { "two", '2'},
            { "three", '3'},
            { "four", '4'},
            { "five", '5'},
            { "six", '6'},
            { "seven", '7'},
            { "eight", '8'},
            { "nine", '9'}
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

                var firstAlphabeticDigit = FindFirstAlphabeticDigit(line);
                var lastAlphabeticDigit = FindLastAlphabeticDigit(line);

                char firstNumericChar = '0';
                bool foundFirst = false;

                char lastNumericChar = '0';
                bool foundLast = false;

                for (int i = 0; i < line.Length; i++)
                {
                    if (!foundFirst && char.IsDigit(line[i]))
                    {
                        if (firstAlphabeticDigit != null && firstAlphabeticDigit.Item2 < i)
                        {
                            firstNumericChar = _alphabeticNumbers[firstAlphabeticDigit.Item1];
                        }
                        else
                        {
                            firstNumericChar = line[i];
                        }
                        foundFirst = true;
                    }

                    var indexFromBack = line.Length - i - 1;
                    if (!foundLast && char.IsDigit(line[indexFromBack]))
                    {
                        if (lastAlphabeticDigit != null && lastAlphabeticDigit.Item2 > indexFromBack)
                        {
                            lastNumericChar = _alphabeticNumbers[lastAlphabeticDigit.Item1];
                        }
                        else
                        {
                            lastNumericChar = line[indexFromBack];
                        }
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

        private Tuple<string, int>? FindFirstAlphabeticDigit(string line)
        {
            var foundAt = new List<Tuple<string, int>>();

            foreach (string s in _alphabeticNumbers.Keys)
            {
                var i = line.IndexOf(s);

                if (i != -1)
                {
                    foundAt.Add(new Tuple<string, int>(s, i));
                }
            }

            return foundAt.OrderBy(x => x.Item2).FirstOrDefault();
        }

        private Tuple<string, int>? FindLastAlphabeticDigit(string line)
        {
            var foundAt = new List<Tuple<string, int>>();

            foreach (string s in _alphabeticNumbers.Keys)
            {
                var i = line.LastIndexOf(s);

                if (i != -1)
                {
                    foundAt.Add(new Tuple<string, int>(s, i));
                }
            }

            return foundAt.OrderBy(x => x.Item2).LastOrDefault();
        }
    }
}
