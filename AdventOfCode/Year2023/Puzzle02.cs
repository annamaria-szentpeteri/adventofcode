namespace Year2023
{
    internal class Puzzle02 : IPuzzle<int>
    {
        record NumberPosition(string Number, int Position);

        private readonly Dictionary<string, char> _alphabeticDigits = new (){
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

                char firstNumericChar = '0';
                bool foundFirst = false;

                char lastNumericChar = '0';
                bool foundLast = false;

                for (int index = 0; index < line.Length; index++)
                {
                    var indexFromEnd = line.Length - index - 1;

                    if (!foundFirst && char.IsDigit(line[index]))
                    {
                        firstNumericChar = GetFirstDigit(line, index);
                        foundFirst = true;
                    }

                    if (!foundLast && char.IsDigit(line[indexFromEnd]))
                    {
                        lastNumericChar = GetLastDigit(line, indexFromEnd);
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

        private char GetFirstDigit(string line, int index)
        {
            var alphabeticDigit = FindFirstAlphabeticNumber(line);
            return IsAlphabeticFirst(alphabeticDigit, index) ? GetDigit(alphabeticDigit!) : line[index];
        }

        private char GetLastDigit(string line, int index)
        {
            var alphabeticDigit = FindLastAlphabeticNumber(line);
            return IsAlphabeticLast(alphabeticDigit, index) ? GetDigit(alphabeticDigit!) : line[index];
        }

        private char GetDigit(NumberPosition alphabeticDigit)
        {
            return _alphabeticDigits[alphabeticDigit!.Number];
        }

        private static bool IsAlphabeticFirst(NumberPosition? alphabeticDigit, int index)
        {
            return alphabeticDigit != null && alphabeticDigit.Position < index;
        }

        private static bool IsAlphabeticLast(NumberPosition? alphabeticDigit, int index)
        {
            return alphabeticDigit != null && alphabeticDigit.Position > index;
        }

        private NumberPosition? FindFirstAlphabeticNumber(string line)
        {
            var foundAt = new List<NumberPosition>();

            foreach (string s in _alphabeticDigits.Keys)
            {
                var i = line.IndexOf(s);

                if (i != -1)
                {
                    foundAt.Add(new NumberPosition(s, i));
                }
            }

            return foundAt.OrderBy(x => x.Position).FirstOrDefault();
        }

        private NumberPosition? FindLastAlphabeticNumber(string line)
        {
            var foundAt = new List<NumberPosition>();

            foreach (string s in _alphabeticDigits.Keys)
            {
                var i = line.LastIndexOf(s);

                if (i != -1)
                {
                    foundAt.Add(new NumberPosition(s, i));
                }
            }

            return foundAt.OrderBy(x => x.Position).LastOrDefault();
        }
    }
}
